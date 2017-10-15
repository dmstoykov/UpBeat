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
        private readonly IUsersService userService;

        public PlaylistController(IMapper mapper, IAlbumService albumService, IUsersService userService)
        {
            Guard.WhenArgument(mapper, "IMapper").IsNull().Throw();
            Guard.WhenArgument(albumService, "IAlbumService").IsNull().Throw();
            Guard.WhenArgument(userService, "IUserService").IsNull().Throw();

            this.mapper = mapper;
            this.albumService = albumService;
            this.userService = userService;
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

        public ActionResult AddToFavourites(int albumId)
        {
            this.userService.AddFavouriteAlbum(albumId);

            return this.View("Details", albumId);
        }
    }
}