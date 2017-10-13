using System;
using System.ComponentModel.DataAnnotations;
using UpBeat.Common.Mappings;
using UpBeat.Common.Constants;
using UpBeat.Data.Models;
using System.ComponentModel;
using AutoMapper;
using System.Linq;

namespace UpBeat.Web.Areas.Administration.Models
{
    public class AlbumGridViewModel : IMapFrom<Album>, ICustomMapping
    {
        public int Id { get; set; }

        [ReadOnly(true)]
        public string Image { get; set; }

        [StringLength(DataConstants.MaxModelNameLength,
            MinimumLength = DataConstants.MinModelNameLength,
            ErrorMessage = "Invalid album name length!")]
        public string Name { get; set; }

        [StringLength(DataConstants.AlbumReleaseDateLength,
            MinimumLength = DataConstants.AlbumReleaseDateLength,
            ErrorMessage = "Invalid date format")]
        public string ReleaseDate { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumGridViewModel>()
                .ForMember(x => x.Image, cfg =>
                cfg.MapFrom(album => album.Images.Count > 1 ? album.Images.ElementAt(0).Url : album.Images.FirstOrDefault().Url));
        }
    }
}