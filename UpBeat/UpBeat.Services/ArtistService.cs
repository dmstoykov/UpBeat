using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class ArtistService : DataService<Artist>, IArtistService
    {
        public ArtistService(IGenericRepository<Artist> artistRepository)
            :base(artistRepository)
        {
        }
    }
}
