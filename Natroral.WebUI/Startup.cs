using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Natroral.WebUI.Startup))]
namespace Natroral.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
