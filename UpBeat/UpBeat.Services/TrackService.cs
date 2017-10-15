using System.Linq;
using Bytes2you.Validation;
using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class TrackService : DataService<Track>, ITrackService
    {
        private readonly IGenericRepository<Album> albumRepository;

        public TrackService(IGenericRepository<Track> trackRepository, IGenericRepository<Album> albumRepository)
            :base(trackRepository)
        {
            Guard.WhenArgument(albumRepository, "AlbumRepository").IsNull().Throw();
            this.albumRepository = albumRepository;
        }

        public void Add(Track track, string albumName)
        {
            Guard.WhenArgument(track, "TrackToAdd").IsNull().Throw();
            Guard.WhenArgument(albumName, "TrackAlbumName").IsNullOrEmpty().Throw();

            var trackExists = this.Data.All.Any(x => x.Name == track.Name);
            Guard.WhenArgument(trackExists, "Existing track").IsTrue().Throw();

            var trackAlbum = this.albumRepository.All.Where(x => x.Name == albumName).FirstOrDefault();
            Guard.WhenArgument(trackAlbum, "Track album").IsNull().Throw();

            track.Album = trackAlbum;

            this.Data.Add(track);
        }

        public void Remove(Track track)
        {
            Guard.WhenArgument(track, "TrackToRemove").IsNull().Throw();

            var trackExists = this.Data.All.Any(x => x.Id == track.Id);
            Guard.WhenArgument(trackExists, "TrackToRemove").IsFalse().Throw();

            this.Data.Remove(track);
        }

        public void Update(Track track)
        {
            Guard.WhenArgument(track, "TrackToUpdate").IsNull().Throw();

            var trackExists = this.Data.All.Any(x => x.Id == track.Id);
            Guard.WhenArgument(trackExists, "TrackToUpdate").IsFalse().Throw();

            this.Data.Update(track);
        }
    }
}
