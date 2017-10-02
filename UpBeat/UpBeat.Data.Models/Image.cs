using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBeat.Data.Models.Abstracts;

namespace UpBeat.Data.Models
{
    public class Image : BaseModel
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Url { get; set; }
    }
}
