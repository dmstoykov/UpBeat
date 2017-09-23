using System.Collections.Generic;
using Newtonsoft.Json;

namespace UpBeat.Data.JsonModels
{
    public class AlbumPlaylist
    {
        [JsonProperty("items")]
        public IList<Album> Albums { get; set; }
    }
}
