using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebWithAuthentication.Startup))]
namespace WebWithAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
