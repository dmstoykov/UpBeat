using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UpBeat.Web.Startup))]
namespace UpBeat.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
