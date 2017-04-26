using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeCur.Startup))]
namespace CodeCur
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
