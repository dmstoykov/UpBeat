using System.Collections.Generic;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Artist : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
