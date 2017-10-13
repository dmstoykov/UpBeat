using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class AlbumService : DataService<Album>, IAlbumService
    {
        private readonly IGenericRepository<Artist> artistRepository;

        public AlbumService(IGenericRepository<Album> albumRepository, IGenericRepository<Artist> artistRepository)
            : base(albumRepository)
        {
            Guard.WhenArgument(artistRepository, artistRepository.GetType().Name).IsNull().Throw();

            this.artistRepository = artistRepository;
        }

        public void Add(Album album, string artistName)
        {
            var albumExists = this.Data.All.Any(x => x.Name == album.Name);
            Guard.WhenArgument(albumExists, "Album exists").IsTrue().Throw();

            var albumArtist = this.artistRepository.All.Where(x => x.Name == artistName).FirstOrDefault();
            Guard.WhenArgument(albumArtist, "Album artist").IsNull().Throw();

            album.Artists = new List<Artist>() { albumArtist };

            this.Data.Add(album);
        }

        public void Remove(Album album)
        {
            var albumToRemove = this.Data.All.Any(x => x.Name == album.Name);
            Guard.WhenArgument(albumToRemove, albumToRemove.GetType().Name).IsTrue().Throw();

            this.Data.Remove(album);
        }

        public void Update(Album album)
        {
            var albumToUpdate = this.Data.All.Any(x => x.Name == album.Name);
            Guard.WhenArgument(albumToUpdate, albumToUpdate.GetType().Name).IsTrue().Throw();

            this.Data.Update(album);
        }

        //private IEnumerable<Artist> ParseArtistNames(IEnumerable<string> artistNames)
        //{
        //    var result = new List<Artist>();

        //    foreach (var name in artistNames)
        //    {
        //        var currentArtist = this.artistRepository.All.Where(y => y.Name == name).FirstOrDefault();

        //        if (currentArtist != null)
        //        {
        //            result.Add(currentArtist);
        //        }
        //    }

        //    return result;
        //}
    }
}
