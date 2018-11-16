using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRentApp.Startup))]
namespace CarRentApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
