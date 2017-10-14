using System;
using System.Collections.Generic;
using AutoMapper;
using UpBeat.Common.Mappings;
using PagedList;

namespace UpBeat.Web.Models
{
    public class PlaylistViewModel: IMapFrom<IPagedList<AlbumViewModel>>, ICustomMapping
    {
        public IPagedList<AlbumViewModel> Albums { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<IPagedList<AlbumViewModel>, PlaylistViewModel>()
                .ForMember(x => x.Albums, cfg => cfg.MapFrom(albums => albums));
        }
    }
}