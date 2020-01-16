using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JalgrattaEksam.Startup))]
namespace JalgrattaEksam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
