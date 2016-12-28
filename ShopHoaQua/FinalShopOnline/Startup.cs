using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalShopOnline.Startup))]
namespace FinalShopOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
