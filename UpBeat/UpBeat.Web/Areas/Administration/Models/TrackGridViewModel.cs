using System.ComponentModel.DataAnnotations;
using UpBeat.Common.Constants;
using UpBeat.Common.Mappings;
using UpBeat.Data.Models;

namespace UpBeat.Web.Areas.Administration.Models
{
    public class TrackGridViewModel : IMapFrom<Track>
    {
        public int Id { get; set; }

        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid album name length!")]
        public string Name { get; set; }

        public string PreviewUrl { get; set; }
    }
}