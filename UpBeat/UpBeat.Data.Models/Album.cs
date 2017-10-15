using System.Collections.Generic;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Album : BaseModel
    {
        public string Name { get; set; }

        public string ReleaseDate { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
