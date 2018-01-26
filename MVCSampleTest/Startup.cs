using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSampleTest.Startup))]
namespace MVCSampleTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
