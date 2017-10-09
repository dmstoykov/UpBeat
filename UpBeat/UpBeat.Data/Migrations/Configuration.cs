using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using UpBeat.Common.Constants;
using UpBeat.Data.JsonModels;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace UpBeat.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<UpBeat.Data.MsSqlDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(UpBeat.Data.MsSqlDbContext context)
        {
            this.SeedSampleData(context);
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (context.Albums.Any())
            {
                return;
            }

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

                foreach (var artist in album.Artists)
                {
                    if (!context.Artists.Any(x => x.Name == artist.Name))
                    {
                        context.Artists.Add(artist);
                    }
                }
            }

            context.Albums.AddRange(dbAlbums);

            context.SaveChanges();
        }
    }
}
