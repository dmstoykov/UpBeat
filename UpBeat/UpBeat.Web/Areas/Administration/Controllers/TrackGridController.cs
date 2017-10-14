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
    public class TrackGridController : AdminController
    {
        private readonly IMapper mapper;
        private readonly ITrackService trackService;

        public TrackGridController(IMapper mapper, ITrackService trackService)
        {
            Guard.WhenArgument(mapper, "IMapper").IsNull().Throw();
            Guard.WhenArgument(trackService, "ITrackService").IsNull().Throw();

            this.mapper = mapper;
            this.trackService = trackService;
        }

        // GET: Administration/TrackGrid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTracks(DataSourceRequest request)
        {
            var tracks = this.trackService
                .GetAll()
                .Select(x => this.mapper.Map<Track, TrackGridViewModel>(x))
                .ToDataSourceResult(request);

            return this.Json(tracks);
        }

        public ActionResult EditTrack(TrackGridViewModel trackViewModel)
        {
            if (trackViewModel != null)
            {
                var trackDbModel = this.mapper.Map<Track>(trackViewModel);
                this.trackService.Update(trackDbModel);
            }

            return this.Json(new { trackViewModel });
        }

        public ActionResult RemoveTrack(TrackGridViewModel trackViewModel)
        {
            if (trackViewModel != null)
            {
                var trackDbModel = this.mapper.Map<Track>(trackViewModel);
                this.trackService.Remove(trackDbModel);
            }

            return this.Json(new { trackViewModel });
        }
    }
}