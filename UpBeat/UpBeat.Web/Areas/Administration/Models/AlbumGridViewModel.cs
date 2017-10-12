using System;
using System.ComponentModel.DataAnnotations;
using UpBeat.Common.Mappings;
using UpBeat.Common.Constants;
using UpBeat.Data.Models;

namespace UpBeat.Web.Areas.Administration.Models
{
    public class AlbumGridViewModel : IMapFrom<Album>
    {
        public int Id { get; set; }

        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid album name length!")]
        public string Name { get; set; }

        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid date format")]
        public string ReleaseDate { get; set; }
    }
}