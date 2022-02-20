using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Workshop.Web.Areas.Identity.IdentityHostingStartup))]

namespace Workshop.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
