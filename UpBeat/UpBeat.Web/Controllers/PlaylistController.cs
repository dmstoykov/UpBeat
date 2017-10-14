using AutoMapper;
using Bytes2you.Validation;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBeat.Common.Constants;
using UpBeat.Services.Contracts;
using UpBeat.Web.Models;

namespace UpBeat.Web.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAlbumService albumService;

        public PlaylistController(IMapper mapper, IAlbumService albumService)
        {
            Guard.WhenArgument(mapper, "IMapper").IsNull().Throw();
            Guard.WhenArgument(albumService, "IAlbumService").IsNull().Throw();

            this.mapper = mapper;
            this.albumService = albumService;
        }

        // GET: Playlist
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All(int? pageNumber)
        {
            var pageNum = pageNumber ?? DataConstants.StartingPageNumber;

            var dbAlbums = this.albumService.GetAll()
                .Select(x => this.mapper.Map<AlbumViewModel>(x))
                .ToList()
                .ToPagedList(pageNum, DataConstants.PageSize);

            var playlistViewModel = new PlaylistViewModel() { Albums = dbAlbums };

            return View("Index", playlistViewModel);
        }

        public ActionResult Details(int albumId)
        {
            var currentAlbum = this.albumService.GetById(albumId);
            var albumViewModel = this.mapper.Map<AlbumViewModel>(currentAlbum);

            return View("AlbumDetails", albumViewModel);
        }
    }
}