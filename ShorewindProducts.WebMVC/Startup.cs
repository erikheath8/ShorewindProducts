using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shorewind.WebMVC.Startup))]
namespace Shorewind.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
