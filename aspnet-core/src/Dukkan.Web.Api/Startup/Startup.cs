using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Dukkan.Configuration;
using Dukkan.Identity;

namespace Dukkan.Web.Startup
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDukkanMvc();
            IdentityRegistrar.Register(services);
            services.AddDukkanAuthentication(_appConfiguration);
            services.AddDukkanCors(_appConfiguration, DefaultCorsPolicyName);
            services.AddDukkanSwagger();
            return services.AddAbp<DukkanWebHostModule>(options =>
                {
                    options.IocManager.IocContainer.AddFacility<LoggingFacility>(f => 
                        f.UseAbpLog4Net().WithConfig("log4net.config")
                    );
                }
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAbp(options =>
            {
                options.UseAbpRequestLocalization = false;
            });
            app.UseDukkanCors(DefaultCorsPolicyName);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAbpRequestLocalization();
            app.UseDukkanMvc();
            app.UseDukkanSwagger(_appConfiguration);
        }
    }
}
