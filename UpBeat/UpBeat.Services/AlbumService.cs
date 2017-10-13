using System.Linq;
using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class AlbumService : DataService<Album>, IAlbumService
    {
        public AlbumService(IGenericRepository<Album> dataRepository)
            : base(dataRepository)
        {
        }

        public void Add(Album album)
        {
            var albumToAdd = this.Data.All.Where(x => x.Name == album.Name).FirstOrDefault();

            if (albumToAdd == null)
            {
                this.Data.Add(album);
            }
        }

        public void Remove(Album album)
        {
            var albumToRemove = this.Data.All.Where(x => x.Name == album.Name).FirstOrDefault();

            if (albumToRemove != null)
            {
                this.Data.Remove(album);
            }
        }

        public void Update(Album album)
        {
            var albumToUpdate = this.Data.All.Where(x => x.Name == album.Name).FirstOrDefault();

            if (albumToUpdate != null)
            {
                this.Data.Update(album);
            }
        }
    }
}
