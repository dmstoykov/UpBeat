using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBeat.Data.Models;

namespace UpBeat.Services.Contracts
{
    public interface IUsersService : IDataService<User>
    {
        void AddFavouriteAlbum(int albumId);
    }
}
