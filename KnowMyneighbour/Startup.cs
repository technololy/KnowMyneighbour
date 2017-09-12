using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnowMyneighbour.Startup))]
namespace KnowMyneighbour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
