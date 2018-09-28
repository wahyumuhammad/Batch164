using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniProject.Web.Startup))]
namespace MiniProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
