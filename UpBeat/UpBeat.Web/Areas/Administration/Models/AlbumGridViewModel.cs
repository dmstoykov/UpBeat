using System;
using System.ComponentModel.DataAnnotations;
using UpBeat.Common.Mappings;
using UpBeat.Common.Constants;
using UpBeat.Data.Models;
using System.ComponentModel;

namespace UpBeat.Web.Areas.Administration.Models
{
    public class AlbumGridViewModel : IMapFrom<Album>
    {
        [ReadOnly(true)]
        public int Id { get; set; }

        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid album name length!")]
        public string Name { get; set; }

        [StringLength(DataConstants.AlbumReleaseDateLength,
            MinimumLength = DataConstants.AlbumReleaseDateLength,
            ErrorMessage = "Invalid date format")]
        public string ReleaseDate { get; set; }
    }
}