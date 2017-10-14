using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using UpBeat.Data.Models;
using UpBeat.Services.Contracts;
using UpBeat.Web.Areas.Administration.Controllers.Abstracts;
using UpBeat.Web.Areas.Administration.Models;
using UpBeat.Web.Infrastructure.Attributes;

namespace UpBeat.Web.Areas.Administration.Controllers
{
    [SaveChanges]
    public class AlbumGridController : AdminController
    {
        private readonly IMapper mapper;
        private readonly IAlbumService albumService;

        public AlbumGridController(IMapper mapper, IAlbumService albumService)
        {
            Guard.WhenArgument(mapper, "IMapper").IsNull().Throw();
            Guard.WhenArgument(albumService, "IAlbumService").IsNull().Throw();

            this.mapper = mapper;
            this.albumService = albumService;
        }

        // GET: Administration/AlbumGrid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAlbums(DataSourceRequest request)
        {
            var albums = this.albumService
                .GetAll()
                .Select(x => this.mapper.Map<Album, AlbumGridViewModel>(x))
                .ToDataSourceResult(request);

            return this.Json(albums);
        }

        public ActionResult EditAlbum(AlbumGridViewModel albumViewModel)
        {
            if (albumViewModel != null)
            {
                var albumDbModel = this.mapper.Map<Album>(albumViewModel);
                this.albumService.Update(albumDbModel);
            }

            return this.Json(new { albumViewModel });
        }

        public ActionResult RemoveAlbum(AlbumGridViewModel albumViewModel)
        {
            if (albumViewModel != null)
            {
                var albumDbModel = this.mapper.Map<Album>(albumViewModel);
                this.albumService.Remove(albumDbModel);
            }

            return this.Json(new { albumViewModel });
        }
    }
}