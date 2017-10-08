using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using UpBeat.Common.Constants;
using UpBeat.Data.JsonModels;
using System.Linq;
using AutoMapper;
using Ninject;
using AutoMapper.QueryableExtensions;

namespace UpBeat.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<UpBeat.Data.MsSqlDbContext>
    {
        [Inject]
        public IMapper Mapper { get; set; }

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(UpBeat.Data.MsSqlDbContext context)
        {
            this.SeedSampleData(context);
        }

        [Inject]
        private void SeedSampleData(MsSqlDbContext context)
        {
            var albums = JsonConvert.DeserializeObject<ICollection<Album>>(System.IO.File.ReadAllText(Resources.DbSeedPath));
            var dbAlbums = albums.AsQueryable().ProjectTo<UpBeat.Data.Models.Album>().ToList();

            foreach (var album in dbAlbums)
            {
                var tracks = album.Tracks;

                foreach (var track in tracks)
                {
                    context.Artists.AddRange(track.Artists);
                }

                context.Tracks.AddRange(tracks);
                context.Images.AddRange(album.Images);
                context.Artists.AddRange(album.Artists);
            }

            context.Albums.AddRange(dbAlbums);

            context.SaveChanges();
        }
    }
}
