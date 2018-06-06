using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebProy.Startup))]
namespace WebProy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
