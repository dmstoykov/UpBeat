using System.Collections.Generic;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Track : BaseModel
    {
        public string Name { get; set; }

        public virtual Album Album { get; set; }

        public long? Duration { get; set; }

        public string PreviewUrl { get; set; }
    }
}
