using AdaptiveExpressions.Converters;
using CustomBotComponents.Recognizers;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.AI.LuisV3;
using Microsoft.Bot.Builder.Dialogs.Declarative;
using Microsoft.Bot.Builder.Dialogs.Declarative.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DynamicList = Microsoft.Bot.Builder.AI.Luis.DynamicList;

namespace CustomBotComponents.Components
{
    public class CustomLuisBotComponent : BotComponent
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Converters
            services.AddSingleton<JsonConverterFactory, JsonConverterFactory<ArrayExpressionConverter<DynamicList>>>();
            services.AddSingleton<JsonConverterFactory, JsonConverterFactory<ArrayExpressionConverter<ExternalEntity>>>();

            // Declarative types
            services.AddSingleton<DeclarativeType>(
                sp => new DeclarativeType<CustomLuisRecognizer>(LuisAdaptiveRecognizer.Kind));
        }
    }
}
