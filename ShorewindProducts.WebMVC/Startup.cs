using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShorewindProducts.WebMVC.Startup))]
namespace ShorewindProducts.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
