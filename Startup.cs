using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capstone6.Startup))]
namespace Capstone6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
