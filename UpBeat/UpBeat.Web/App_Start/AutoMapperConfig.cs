using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using UpBeat.Common.Mappings;
using UpBeat.Common.Constants;

namespace UpBeat.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Put IMapFrom And ICustomMapping to the json models and convert them to database models then execute parse and seed database
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            var jsonModelTypes = Assembly.Load(Assemblies.JsonModels).GetExportedTypes();

            LoadStandardMappings(types);
            LoadStandardMappings(jsonModelTypes);
            
            LoadCustomMappings(types);
            LoadCustomMappings(jsonModelTypes);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            Mapper.Initialize((config) =>
            {
                foreach (var map in maps)
                {
                    config.CreateMap(map.Source, map.Destination);
                    config.CreateMap(map.Destination, map.Source);
                }
            });
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMapping).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (ICustomMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}