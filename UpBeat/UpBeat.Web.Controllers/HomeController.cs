using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using UpBeat.Data.JsonModels;
using UpBeat.Common.Constants;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace UpBeat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;

        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var albums = JsonConvert.DeserializeObject<ICollection<Album>>(System.IO.File.ReadAllText(Resources.DbSeedPath));

            var dbAlbums = albums.AsQueryable().ProjectTo<UpBeat.Data.Models.Album>().ToList();

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Current logged user: " + User.Identity.Name;

            return View();
        }
    }
}