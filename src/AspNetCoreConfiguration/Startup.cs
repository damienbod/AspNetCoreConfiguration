using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AspNetCoreConfiguration.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreConfiguration
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {		
            var globalbuilder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                .AddJsonFile("globalconfig.json");
            var globalConfiguration = globalbuilder.Build();

            string stagingEnvironment = globalConfiguration["StagingEnvironment"];

            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{stagingEnvironment}.json", optional: true);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // int level = int.Parse(Configuration["ApplicationConfiguration:MinimumLevel"]);

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
