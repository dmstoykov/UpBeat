using AutoMapper;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;

        public AlbumGridController(IAlbumService albumService, IMapper mapper)
        {
            Guard.WhenArgument(albumService, albumService.GetType().Name).IsNull().Throw();
            Guard.WhenArgument(mapper, mapper.GetType().Name).IsNull().Throw();

            this.albumService = albumService;
            this.mapper = mapper;
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