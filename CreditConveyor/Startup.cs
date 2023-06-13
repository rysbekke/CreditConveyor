using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(СreditСonveyor.Startup))]
namespace СreditСonveyor
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

