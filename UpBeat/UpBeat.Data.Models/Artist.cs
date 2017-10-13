using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Artist : BaseModel
    {
        public string Name { get; set; }

        public virtual Album Album { get; set; }
    }
}
