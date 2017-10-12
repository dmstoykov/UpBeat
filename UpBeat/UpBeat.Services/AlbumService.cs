using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class AlbumService : DataService<Album>, IAlbumService
    {
        public AlbumService(IGenericRepository<Album> dataRepository)
            :base(dataRepository)
        {
        }
    }
}
