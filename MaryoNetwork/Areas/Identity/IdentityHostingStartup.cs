using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MaryoNetwork.Areas.Identity.IdentityHostingStartup))]
namespace MaryoNetwork.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}