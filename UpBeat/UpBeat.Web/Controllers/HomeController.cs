using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using UpBeat.Common.Constants;
using UpBeat.Data.Repositories;
using UpBeat.Data.Models;
using UpBeat.Data;

namespace UpBeat.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var repo = new GenericRepository<Album>(new MsSqlDbContext());

            ViewBag.Albums = repo.All.ToList();

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