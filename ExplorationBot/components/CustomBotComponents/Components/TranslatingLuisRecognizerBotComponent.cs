using CustomBotComponents.Recognizers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomBotComponents.Components
{
    public class TranslatingLuisRecognizerBotComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMiddleware, RegisterClassMiddleware<ILoggerFactory>>(
                sp => new RegisterClassMiddleware<ILoggerFactory>(sp.GetRequiredService<ILoggerFactory>()));

            services.AddSingleton<DeclarativeType>(
                sp => new DeclarativeType<TranslatingLuisRecognizer>(LuisAdaptiveRecognizer.Kind));

            //services.AddSingleton<DeclarativeType>(
            //    sp => new DeclarativeType<TranslatingLuisRecognizer>(TranslatingLuisRecognizer.Kind));
        }
    }
}
