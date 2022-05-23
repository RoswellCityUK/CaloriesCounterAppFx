using CaloriesCounterAppFx.Migrations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaloriesCounterAppFx.Startup))]
namespace CaloriesCounterAppFx
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
