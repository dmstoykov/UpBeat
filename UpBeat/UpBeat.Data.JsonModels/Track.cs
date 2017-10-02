using System.Collections.Generic;
using Newtonsoft.Json;
using UpBeat.Common.Mappings;

namespace UpBeat.Data.JsonModels
{
    public class Track : IMapFrom<Data.Models.Track>
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("artists")]
        public IList<Artist> Artists { get; set; }

        [JsonProperty("duration_ms")]
        public long? Duration { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
    }
}
