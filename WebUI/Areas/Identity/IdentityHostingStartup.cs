using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Data;

[assembly: HostingStartup(typeof(WebUI.Areas.Identity.IdentityHostingStartup))]
namespace WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => 
            {
                builder.ConfigureServices((context, services) => {
                    services.AddDbContext<ApplicationDbContext<IdentityUser>>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("ApplicationContextConnection")));

                    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<ApplicationDbContext<IdentityUser>>();
                });
            });
        }
    }
}