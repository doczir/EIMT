using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EIMT.Startup))]
namespace EIMT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
