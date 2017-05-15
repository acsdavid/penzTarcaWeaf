using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(penztarca.Startup))]
namespace penztarca
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
