using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace Umbraco.Course.Level2.Events
{
    public class Level2ApplicationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication,ApplicationContext applicationContext)
        {
            ContentService.Publishing += ContentService_Publishing;
        }

        private void ContentService_Publishing(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            var badWords = new[] {"fudge", "shiza", "crap", "heck", "darn"};
            foreach (var content in e.PublishedEntities)
            {
                if (!content.ContentType.Alias.Equals("statusUpdate")) continue;
                var bodyText = content.GetValue<string>("bodyText").ToLower();
                if (!badWords.Any(bodyText.Contains)) continue;
                e.Cancel = true;
                return;
            }
        }
    }
}