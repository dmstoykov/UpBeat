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
    public class AlbumViewModel : IMapFrom<Album>
    {
        [Required]
        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid album name length!")]
        public string Name { get; set; }

        [Required]
        [StringLength(DataConstants.AlbumReleaseDateLength,
            MinimumLength = DataConstants.AlbumReleaseDateLength,
            ErrorMessage = "Invalid date format")]
        public string ReleaseDate { get; set; }

        //public IEnumerable<SelectListItem> Images { get; set; }

        //public IEnumerable<SelectListItem> Artists { get; set; }
    }
}