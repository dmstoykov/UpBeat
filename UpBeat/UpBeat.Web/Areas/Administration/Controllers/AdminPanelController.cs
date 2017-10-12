using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBeat.Web.Areas.Administration.Controllers.Abstracts;

namespace UpBeat.Web.Areas.Administration.Controllers
{
    public class AdminPanelController : AdminController
    {
        // GET: Administration/AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAlbum()
        {

            return View();
        }
    }
}