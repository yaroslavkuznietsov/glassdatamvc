using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GlassData.Web.Startup))]
namespace GlassData.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
