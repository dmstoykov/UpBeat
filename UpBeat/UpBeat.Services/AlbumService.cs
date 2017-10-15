using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using UpBeat.Common.Constants;
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
            Guard.WhenArgument(artistRepository, "ArtistRepository").IsNull().Throw();

            this.artistRepository = artistRepository;
        }

        public Album GetById(int id)
        {
            var result = this.Data.All.Where(x => x.Id == id).FirstOrDefault();
            Guard.WhenArgument(result, "ResultAlbum").IsNull().Throw();

            if (result.Tracks != null)
            {
                var tracks = result.Tracks.Where(x => !x.IsDeleted).ToList();
                result.Tracks = tracks;
            }

            return result;
        }

        public void Add(Album album, string artistName)
        {
            Guard.WhenArgument(album, "Album").IsNull().Throw();
            Guard.WhenArgument(artistName, "ArtistName").IsNullOrEmpty().Throw();

            var albumExists = this.Data.All.Any(x => x.Name == album.Name);
            Guard.WhenArgument(albumExists, "AlbumToAdd").IsTrue().Throw();

            var albumArtist = this.artistRepository.All.Where(x => x.Name == artistName).FirstOrDefault();
            Guard.WhenArgument(albumArtist, "AlbumArtist").IsNull().Throw();

            album.Artists = new List<Artist>() { albumArtist };
            album.Images = new List<Image>()
            {
                new Image()
                {
                    Width = DataConstants.DefaultAlbumImageWidth,
                    Height = DataConstants.DefaultAlbumImageWidth,
                    Url = Resources.DefaultAlbumImageUrl
                }
            };

            this.Data.Add(album);
        }

        public void Remove(Album album)
        {
            Guard.WhenArgument(album, "AlbumToRemove").IsNull().Throw();

            var albumToRemove = this.Data.All.Any(x => x.Id == album.Id);
            Guard.WhenArgument(albumToRemove, "AlbumToRemove").IsFalse().Throw();

            this.Data.Remove(album);
        }

        public void Update(Album album)
        {
            Guard.WhenArgument(album, "AlbumToUpdate").IsNull().Throw();

            var albumToUpdate = this.Data.All.Any(x => x.Id == album.Id);
            Guard.WhenArgument(albumToUpdate, "AlbumToUpdate").IsFalse().Throw();

            this.Data.Update(album);
        }
    }
}
