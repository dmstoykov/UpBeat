using System.Collections.Generic;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Track : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Artist> Artists { get; set; }

        public long? Duration { get; set; }

        public string PreviewUrl { get; set; }
    }
}
