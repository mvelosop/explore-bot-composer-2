using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomBotComponents.Recognizers
{
    public class CustomLuisRecognizer : LuisAdaptiveRecognizer
    {
        public override Task<RecognizerResult> RecognizeAsync(DialogContext dialogContext, Activity activity, CancellationToken cancellationToken = default, Dictionary<string, string> telemetryProperties = null, Dictionary<string, double> telemetryMetrics = null)
        {
            var logger = dialogContext.Services.Get<ILoggerFactory>().CreateLogger<CustomLuisRecognizer>();

            logger.LogInformation("----- Recognizing (LUIS) '{Text}' [{locale}] ({@Activity})", activity.Text, activity.Locale, activity);

            var recognizerResult = base.RecognizeAsync(dialogContext, activity, cancellationToken, telemetryProperties, telemetryMetrics);

            logger.LogInformation("----- Recognized (LUIS) '{Text}' [{locale}] ({@RecognizerResult})", activity.Text, activity.Locale, recognizerResult);

            return recognizerResult;
        }
    }
}
