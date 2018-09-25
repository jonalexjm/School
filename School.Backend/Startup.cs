using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School.Backend.Startup))]
namespace School.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
