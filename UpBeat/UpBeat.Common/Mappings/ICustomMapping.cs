using AutoMapper;

namespace UpBeat.Common.Mappings
{
    public interface ICustomMapping
    {
        void CreateMappings(IConfigurationProvider configuration);
    }
}
