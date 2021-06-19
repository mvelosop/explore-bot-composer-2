using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomBotComponents.Middlewares
{
    public class CustomMiddleware : IMiddleware
    {
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(ILogger<CustomMiddleware> logger)
        {
            this._logger = logger;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                LogUserActivity(turnContext.Activity);
            }

            turnContext.OnSendActivities(LogBotActiviesAsync);

            await next(cancellationToken);
        }

        private void LogUserActivity(Activity activity)
        {
            _logger.LogInformation("CONVERSATION-LOG {ConversationId} --> [{Locale}]: {Text} ({Source})",
                activity.Conversation.Id, activity.Locale, activity.Text, "user");
        }

        private async Task<ResourceResponse[]> LogBotActiviesAsync(ITurnContext turnContext, List<Activity> activities, Func<Task<ResourceResponse[]>> next)
        {
            foreach (var activity in activities.Where(x => x.Type == ActivityTypes.Message))
            {
                _logger.LogInformation("CONVERSATION-LOG {ConversationId} <-- [{Locale}]: {Text} ({Source})",
                    activity.Conversation.Id, activity.Locale, activity.Text, "bot");
            }

            return await next();
        }

    }
}
