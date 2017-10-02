using Newtonsoft.Json;
using UpBeat.Common.Mappings;

namespace UpBeat.Data.JsonModels
{
    public class Image : IMapFrom<Data.Models.Image>
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
