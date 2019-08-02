using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_MeetingMS.Startup))]
namespace E_MeetingMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
