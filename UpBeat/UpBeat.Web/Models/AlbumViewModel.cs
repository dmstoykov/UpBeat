using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UpBeat.Common.Mappings;
using UpBeat.Data.Models;

namespace UpBeat.Web.Models
{
    public class AlbumViewModel : IMapFrom<Album>, ICustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public List<string> ArtistNames { get; set; }

        public List<Track> Tracks { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumViewModel>()
                .ForMember(x => x.ArtistNames, cfg => cfg.MapFrom(album => album.Artists.Select(y => y.Name).ToList()))
                .ForMember(x => x.ImageUrl, cfg => cfg.MapFrom(album => album.Images.ElementAt(1).Url));
        }
    }
}