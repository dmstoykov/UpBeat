using System.Web.Mvc;
using UpBeat.Web.Infrastructure.Attributes;

namespace UpBeat.Web.Areas.Administration.Controllers.Abstracts
{
    [AuthorizeAdmin]
    public abstract class AdminController : Controller
    {
    }
}