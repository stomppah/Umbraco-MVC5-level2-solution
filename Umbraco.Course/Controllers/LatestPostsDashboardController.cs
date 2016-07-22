using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Umbraco.Web.WebApi;

namespace Umbraco.Course.Controllers
{
    public class LatestPostsDashboardController : UmbracoAuthorizedApiController
    {
        public IEnumerable<StatusUpdate> GetLatestsPost()
        {
            var type = Services.ContentTypeService.GetContentType("statusUpdate");

            var statusUpdates = Services.ContentService.GetContentOfContentType(type.Id)
                .OrderByDescending(x => x.CreateDate).Take(10);

            var content = new List<StatusUpdate>();

            foreach (var statusUpdate in statusUpdates)
            {
                var memberId = statusUpdate.GetValue<int>("memberId");
                var memberName = memberId == 0
                    ? "Guest"
                    : Services.MemberService.GetById(memberId).Name;

                content.Add(new StatusUpdate()
                {
                    Id = statusUpdate.Id,
                    Text = statusUpdate.GetValue<string>("bodyText"),
                    CreateDate = statusUpdate.CreateDate.ToString(),
                    MemberName = memberName
                });
            }
            return content;
        }

    }

    public class StatusUpdate
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("memberName")]
        public string MemberName { get; set; }
        [JsonProperty("createDate")]
        public string CreateDate { get; set; }
    }
}