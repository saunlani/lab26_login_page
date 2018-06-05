using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab26_login_page.Startup))]
namespace lab26_login_page
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
