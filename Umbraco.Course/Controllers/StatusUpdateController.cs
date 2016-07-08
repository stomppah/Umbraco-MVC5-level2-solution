using System.Configuration;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Course.Level2;
using Umbraco.Course.Models;

namespace Umbraco.Course.Controllers
{
    public class StatusUpdateController : SurfaceController
    {
        public ActionResult Create(StatusUpdateModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var currentPageId = CurrentPage.Id;

            // Get handles to services we need
            var contentService = Services.ContentService;
            var mediaService = Services.MediaService;

            // Create content
            var statusUpdateTitle = Umbraco.Truncate(Umbraco.StripHtml(model.BodyText), 50, false).ToString();
            var content = contentService.CreateContent(statusUpdateTitle, currentPageId, "statusUpdate");

            // Process media uploads - if any
            if (model.Files.HasFiles() && model.Files.ContainsImages())
            {
                var folder = mediaService.CreateMedia(statusUpdateTitle, -1, "Folder");
                mediaService.Save(folder);

                var relatedMediaIds = new CommaDelimitedStringCollection();
                foreach (var file in model.Files)
                {
                    if (!file.IsImage()) continue;
                    var mediaImage = mediaService.CreateMedia(file.FileName, folder.Id, "image");
                    mediaImage.SetValue("umbracoFile", file);
                    mediaService.Save(mediaImage);
                    relatedMediaIds.Add(mediaImage.Id.ToString());
                }
                content.SetValue("relatedMedia", relatedMediaIds.ToString());
            }

            // Set post properties
            content.SetValue("bodyText", model.BodyText);

            // Publish
            contentService.PublishWithStatus(content);
            
            return RedirectToCurrentUmbracoPage();
        }
    }
}