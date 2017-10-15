using System.Collections.Generic;
using UpBeat.Data.Models;

namespace UpBeat.Services.Contracts
{
    public interface IAlbumService : IDataService<Album>
    {
        Album GetById(int id);

        void Add(Album album, string artistName);

        void Update(Album album);

        void Remove(Album album);
    }
}
