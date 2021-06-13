using Microsoft.Bot.Builder.AI.Luis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomBotComponents.Recognizers
{
    public class LuisTranslatingRecognizer : LuisAdaptiveRecognizer
    {
        [JsonProperty("$kind")]
        public new const string Kind = "Custom.LuisRecognizer";
    }
}
