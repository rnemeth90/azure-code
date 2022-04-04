using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapp_auth.Data;

[assembly: HostingStartup(typeof(webapp_auth.Areas.Identity.IdentityHostingStartup))]
namespace webapp_auth.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<webapp_authContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("webapp_authContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<webapp_authContext>();
            });
        }
    }
}