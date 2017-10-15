using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UpBeat.Common.Constants;

namespace UpBeat.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = DataConstants.LongCacheTime)]
        [ChildActionOnly]
        public ActionResult HomePage()
        {
            return this.PartialView(Views.HomePagePartial);
        }

        public ActionResult About()
        {
            return View();
        }

        [OutputCache(Duration = 3)]
        [ChildActionOnly]
        public ActionResult AboutContent()
        {
            ViewBag.Title = "About the creator";
            return this.PartialView(Views.AboutContentPartial);
        }
    }
}