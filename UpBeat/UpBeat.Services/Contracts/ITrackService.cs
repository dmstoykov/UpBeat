using UpBeat.Data.Models;

namespace UpBeat.Services.Contracts
{
    public interface ITrackService : IDataService<Track>
    {
        void Add(Track track, string albumName);

        void Update(Track track);

        void Remove(Track track);
    }
}
