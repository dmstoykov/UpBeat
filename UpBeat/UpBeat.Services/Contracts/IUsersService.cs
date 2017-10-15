using System;
using UpBeat.Data.Models;

namespace UpBeat.Services.Contracts
{
    public interface IUsersService : IDataService<User>
    {
        User GetByUsername(string username);

        void AddFavouriteAlbum(int albumId);
    }
}
