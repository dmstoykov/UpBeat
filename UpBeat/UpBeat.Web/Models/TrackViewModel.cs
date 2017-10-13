using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UpBeat.Common.Mappings;
using UpBeat.Data.Models;

namespace UpBeat.Web.Models
{
    public class TrackViewModel : IMapFrom<Track>, ICustomMapping
    {
        public string Title { get; set; }

        public List<string> ArtistNames { get; set; }

        public long? Duration { get; set; }

        public string PreviewUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Track, TrackViewModel>()
                .ForMember(x => x.Title, cfg => cfg.MapFrom(track => track.Name));
        }
    }
}