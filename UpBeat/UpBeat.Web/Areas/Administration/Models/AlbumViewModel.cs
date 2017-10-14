using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ErrorMessage = ErrorMessages.FormName)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2}$", ErrorMessage = ErrorMessages.FormDate)]
        public string ReleaseDate { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public IEnumerable<SelectListItem> ArtistSelectList { get; set; }
    }
}