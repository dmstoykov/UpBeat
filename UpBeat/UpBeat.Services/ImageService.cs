using UpBeat.Data.Contracts;
using UpBeat.Data.Models;
using UpBeat.Services.Abstracts;
using UpBeat.Services.Contracts;

namespace UpBeat.Services
{
    public class ImageService : DataService<Image>, IImageService
    {
        public ImageService(IGenericRepository<Image> dataRepository)
            :base(dataRepository)
        {
        }
    }
}
