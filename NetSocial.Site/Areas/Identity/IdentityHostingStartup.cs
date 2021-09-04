using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetSocial.Site.Areas.Identity.Data;
using NetSocial.Site.Data;

[assembly: HostingStartup(typeof(NetSocial.Site.Areas.Identity.IdentityHostingStartup))]
namespace NetSocial.Site.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserAccountsDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserAccountsDbContextConnection")));

                services.AddDefaultIdentity<UseModel>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<UserAccountsDbContext>();
            });
        }
    }
}