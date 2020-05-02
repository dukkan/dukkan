using System.Reflection;
using Dukkan.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Dukkan.Web.Startup
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDukkanSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(configuration["App:SwaggerEndPoint"], "Dukkan API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Dukkan.Web.wwwroot.swagger.ui.index.html");
                options.InjectBaseUrl(configuration["App:ServerRootAddress"]);
            }); //URL: /swagger
        }

        public static void UseDukkanMvc(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void UseDukkanCors(this IApplicationBuilder app, string policyName)
        {
            app.UseCors(policyName); // Enable CORS!
        }
    }
}
