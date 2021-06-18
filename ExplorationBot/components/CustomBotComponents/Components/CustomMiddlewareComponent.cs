using Microsoft.Bot.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomBotComponents.Components
{
    public class CustomMiddlewareComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Application services
            services.AddSingleton<IMiddleware, RegisterClassMiddleware<ILoggerFactory>>(
                sp => new RegisterClassMiddleware<ILoggerFactory>(sp.GetRequiredService<ILoggerFactory>()));
        }
    }
}
