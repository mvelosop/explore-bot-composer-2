using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;

namespace ExplorationBot
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddBotRuntime(Configuration);

            var declarativeTypes = services
                .Where(x => x.ServiceType == typeof(DeclarativeType))
                .ToList();

            var recognizers = declarativeTypes
                .Where(x => x.ImplementationFactory.Target.GetType().FullName.Contains("Recognizer"))
                .ToList();

            Log.Information("----- Registered recognizers: {@Recognizers}", recognizers.Select(x => x.ImplementationFactory.Target.GetType().FullName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();

            // Set up custom content types - associating file extension to MIME type.
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".lu"] = "application/vnd.microsoft.lu";
            provider.Mappings[".qna"] = "application/vnd.microsoft.qna";

            // Expose static files in manifests folder for skill scenarios.
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
