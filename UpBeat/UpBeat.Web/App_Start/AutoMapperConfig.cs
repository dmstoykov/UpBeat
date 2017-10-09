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
        public void RegisterMappings()
        {
            var jsonModelAssembly = Assembly.Load(Assemblies.JsonModels);
            var viewModels = Assembly.GetExecutingAssembly();

            this.Execute(jsonModelAssembly);
            this.Execute(viewModels);
        }

        public static IMapperConfigurationExpression Configuration { get; private set; }

        public IMapperConfigurationExpression Execute(Assembly assembly)
        {
            Mapper.Initialize((config) =>
            {
                var types = assembly.GetExportedTypes();
                LoadStandardMappings(types, config);
                LoadCustomMappings(types, config);

                Configuration = config;
            });

            return Configuration;
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression config)
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

            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination);
                config.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression config)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMapping).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (ICustomMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(config);
            }
        }
    }
}