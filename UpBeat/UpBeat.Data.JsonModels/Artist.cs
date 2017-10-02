using Newtonsoft.Json;
using UpBeat.Common.Mappings;

namespace UpBeat.Data.JsonModels
{
    public class Artist : IMapFrom<Data.Models.Artist>
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
