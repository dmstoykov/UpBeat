using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBeat.Common.Constants;
using UpBeat.Common.Mappings;
using UpBeat.Data.Models;

namespace UpBeat.Web.Areas.Administration.Models
{
    public class TrackViewModel : IMapFrom<Track>
    {
        [Required]
        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid track name length!")]
        public string Name { get; set; }

        public long? Duration { get; set; }

        public string PreviewUrl { get; set; }

        [Required]
        public string AlbumName { get; set; }

        public IEnumerable<SelectListItem> AlbumSelectList { get; set; }
    }
}