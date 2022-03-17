using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sqlapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Logging;
using Sqlapp.Util;
using Sqlapp.Interfaces;

namespace Sqlapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Ensure to add the services
            services.AddMvc();
            services.AddTransient<CourseService>(_ => new CourseService(Configuration.GetConnectionString("SQLConnection")));
            //services.AddScoped<ICourseService, CourseService>();
            services.AddApplicationInsightsTelemetry(Configuration.GetConnectionString("APPINSIGHTS_CONNECTIONSTRING"));
            services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module,o) => { module.EnableSqlCommandTextInstrumentation = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.CreateLogger<Startup>();
            }

            app.UseRouting();

            // Ensure to map the controllers accordingly
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Course}/{action=Index}/{id?}");
            });

        }
    }
}
