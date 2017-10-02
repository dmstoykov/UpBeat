using System.Collections.Generic;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Album : BaseModel
    {
        public string Name { get; set; }

        public string ReleaseDate { get; set; }

        public ICollection<Artist> Artists { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
