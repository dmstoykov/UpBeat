using System.Web.Mvc;
using UpBeat.Data.Contracts;

namespace UpBeat.Web.Infrastructure.Filters
{
    public class SaveChangesFilter : IActionFilter
    {
        private readonly ISaveChanges contextSaveChanges;

        public SaveChangesFilter(ISaveChanges contextSaveChanges)
        {
            this.contextSaveChanges = contextSaveChanges;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.contextSaveChanges.Commit();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}