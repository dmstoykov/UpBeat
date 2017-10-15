using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class UserService : DataService<User>, IUsersService
    {
        private readonly IAlbumService albumService;

        public UserService(IGenericRepository<User> userRepository, IAlbumService albumService) 
            : base(userRepository)
        {
            this.albumService = albumService;
        }

        public void AddFavouriteAlbum(int albumId)
        {
            Guard.WhenArgument(albumId, "AlbumId").IsEqual(0).Throw();

            var currentUserName = HttpContext.Current.User.Identity.Name;
            var loggedInUser = this.Data.All.Where(x => x.UserName == currentUserName).FirstOrDefault();
            var albumToAdd = this.albumService.GetById(albumId);

            if (!loggedInUser.FavouriteAlbums.Contains(albumToAdd))
            {
                loggedInUser.FavouriteAlbums.Add(albumToAdd);
                this.Data.Update(loggedInUser);
            }
        }
    }
}
