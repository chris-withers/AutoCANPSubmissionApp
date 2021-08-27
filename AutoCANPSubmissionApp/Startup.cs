using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using AutoCANP.Api.ServiceInterfaces.Services.CANP.Report;
using Funq;
using ServiceStack.Validation;
using ServiceStack.Api.OpenApi;
using ServiceStack.Text;
using AutoCANP.Api;

namespace AutoCANPSubmissionApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }

        public class AppHost : AppHostBase
        {
            public AppHost() : base("AutoCANPSubmissionApp.App", typeof(CANPReportServices).Assembly) { }

            public override void Configure(Container container)
            {

                Bindings.Configure(container);
                Plugins.Add(new ValidationFeature());
                Plugins.Add(new OpenApiFeature());

                SetConfig(new HostConfig
                {
                    HandlerFactoryPath = "api",
                    //  DefaultRedirectPath = "api/metadata",
                    DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
                });

                Instance.Config.GlobalResponseHeaders.Remove("x-powered-by");

                JsConfig.Init(new Config
                {
                    DateHandler = DateHandler.ISO8601
                });

            //    container.RegisterValidators(typeof(UserServices).Assembly);
            }
        }
    }
}
