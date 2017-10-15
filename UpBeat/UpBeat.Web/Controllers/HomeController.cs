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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [OutputCache(Duration = DataConstants.LongCacheTime)]
        [ChildActionOnly]
        public ActionResult AboutContent()
        {
            return this.PartialView(Views.AboutContentPartial);
        }
    }
}