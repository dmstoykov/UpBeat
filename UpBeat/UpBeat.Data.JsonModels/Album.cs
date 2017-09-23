using System.Collections.Generic;
using Newtonsoft.Json;

namespace UpBeat.Data.JsonModels
{
    public class Album
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public IList<Image> Images { get; set; }
    }
}
