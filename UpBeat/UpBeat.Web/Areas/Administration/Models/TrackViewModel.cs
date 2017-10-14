using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ErrorMessage = ErrorMessages.FormName)]
        public string Name { get; set; }

        public long? Duration { get; set; }

        [RegularExpression(DataConstants.UrlRegex, ErrorMessage = ErrorMessages.FormLink)]
        public string PreviewUrl { get; set; }

        [Required]
        public string AlbumName { get; set; }

        public IEnumerable<SelectListItem> AlbumSelectList { get; set; }
    }
}