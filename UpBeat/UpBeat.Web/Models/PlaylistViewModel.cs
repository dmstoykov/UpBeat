using System;
using System.Collections.Generic;
using AutoMapper;
using UpBeat.Common.Mappings;

namespace UpBeat.Web.Models
{
    public class PlaylistViewModel: IMapFrom<ICollection<AlbumViewModel>>, ICustomMapping
    {
        public ICollection<AlbumViewModel> Albums { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ICollection<AlbumViewModel>, PlaylistViewModel>()
                .ForMember(x => x.Albums, cfg => cfg.MapFrom(albums => albums));
        }
    }
}