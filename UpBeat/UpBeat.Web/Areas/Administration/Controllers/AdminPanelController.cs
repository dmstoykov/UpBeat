using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBeat.Common.Constants;
using UpBeat.Data.Models;
using UpBeat.Services.Contracts;
using UpBeat.Web.Areas.Administration.Controllers.Abstracts;
using UpBeat.Web.Areas.Administration.Models;
using System.Web.Mvc.Expressions;
using UpBeat.Web.Infrastructure.Attributes;
using Bytes2you.Validation;

namespace UpBeat.Web.Areas.Administration.Controllers
{
    [SaveChanges]
    public class AdminPanelController : AdminController
    {
        private readonly IMapper mapper;
        private readonly IAlbumService albumService;
        private readonly ITrackService trackService;

        public AdminPanelController(
            IMapper mapper,
            IAlbumService albumService,
            ITrackService trackService)
        {
            Guard.WhenArgument(mapper, mapper.GetType().Name).IsNull().Throw();
            Guard.WhenArgument(albumService, albumService.GetType().Name).IsNull().Throw();
            Guard.WhenArgument(trackService, trackService.GetType().Name).IsNull().Throw();

            this.mapper = mapper;
            this.albumService = albumService;
            this.trackService = trackService;
        }

        // GET: Administration/AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAlbum()
        {
            return this.PartialView(Views.AddAlbumPartial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAlbum(AlbumViewModel albumModel)
        {
            if (this.ModelState.IsValid)
            {
                var albumDbModel = this.mapper.Map<Album>(albumModel);
                albumDbModel.Images = new List<Image>()
                {
                    Resources.DefaultAlbumImage
                };

                this.albumService.Add(albumDbModel);

            }

            return this.RedirectToAction<AlbumGridController>(c => c.Index());
        }

        [HttpGet]
        public ActionResult AddTrack()
        {
            var avaliableAlbums = this.albumService.GetAll()
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Name }).ToList() ;

            var trackViewModel = new TrackViewModel()
            {
                AlbumSelectList = avaliableAlbums
            };

            return this.PartialView(Views.AddTrackPartial, trackViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrack(TrackViewModel trackModel)
        {
            if (this.ModelState.IsValid)
            {
                var trackDbModel = this.mapper.Map<Track>(trackModel);

                this.trackService.Add(trackDbModel, trackModel.AlbumName);
            }

            return this.RedirectToAction<AlbumGridController>(c => c.Index());
        }
    }
}