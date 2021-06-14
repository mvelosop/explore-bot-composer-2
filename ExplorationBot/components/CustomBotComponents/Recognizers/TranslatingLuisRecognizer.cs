using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomBotComponents.Recognizers
{
    public class TranslatingLuisRecognizer : LuisAdaptiveRecognizer
    {
        [JsonProperty("$kind")]
        public new const string Kind = "Custom.TranslatingLuisRecognizer";

        public override Task<RecognizerResult> RecognizeAsync(DialogContext dialogContext, Activity activity, CancellationToken cancellationToken = default, Dictionary<string, string> telemetryProperties = null, Dictionary<string, double> telemetryMetrics = null)
        {
            Log.Information("----- Recognizing (LUIS) '{Text}' [{locale}] ({@Activity})", activity.Text, activity.Locale, activity);

            return base.RecognizeAsync(dialogContext, activity, cancellationToken, telemetryProperties, telemetryMetrics);
        }
    }
}
