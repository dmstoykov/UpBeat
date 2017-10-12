using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using UpBeat.Common.Constants;
using UpBeat.Data.Models;

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
            this.SeedAdminUser(context);
            this.SeedSampleData(context);
        }

        private void SeedAdminUser(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.CreateRole(DataConstants.AdminRoleName, context);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = DataConstants.AdministratorUserName,
                    Email = DataConstants.AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, DataConstants.AdministratorPassword);
                userManager.AddToRole(user.Id, DataConstants.AdminRoleName);
            }
        }

        private void CreateRole(string roleName, MsSqlDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = roleName };

            roleManager.Create(role);
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            if (context.Albums.Any())
            {
                return;
            }

            var albums = JsonConvert.DeserializeObject<ICollection<JsonModels.Album>>(System.IO.File.ReadAllText(Resources.DbSeedPath));
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
