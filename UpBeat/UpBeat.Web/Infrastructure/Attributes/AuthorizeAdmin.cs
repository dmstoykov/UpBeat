using System.Web.Mvc;
using UpBeat.Common.Constants;

namespace UpBeat.Web.Infrastructure.Attributes
{
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        public AuthorizeAdmin()
        {
            Roles = DataConstants.AdminRoleName;
        }
    }
}