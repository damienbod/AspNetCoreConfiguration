using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using AspNet5Configuration.Configurations;

namespace AspNet5Configuration
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
            services.AddMvc();
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

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
