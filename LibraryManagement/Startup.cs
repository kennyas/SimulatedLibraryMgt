using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimulatedLibraryMgt.Startup))]
namespace SimulatedLibraryMgt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
