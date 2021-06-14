using CustomBotComponents.Recognizers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomBotComponents.Components
{
    public class TranslatingLuisRecognizerBotComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DeclarativeType>(
                sp => new DeclarativeType<TranslatingLuisRecognizer>(TranslatingLuisRecognizer.Kind));
        }
    }
}
