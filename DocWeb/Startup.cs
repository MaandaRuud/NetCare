using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocWeb.Startup))]
namespace DocWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
