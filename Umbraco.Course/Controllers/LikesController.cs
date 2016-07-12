using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Umbraco.Web.WebApi;

namespace Umbraco.Course.Controllers
{
    [MemberAuthorize(AllowType="IntranetMember")]
    public class LikesController : UmbracoApiController
    {
        [System.Web.Http.HttpGet]
        public int LikeStatus(int id)
        {
            var contentService = Services.ContentService;
            var post = contentService.GetById(id);

            var numberOfLikes = post.GetValue<int>("likes");
            numberOfLikes++;

            post.SetValue("likes", numberOfLikes);
            contentService.PublishWithStatus(post);

            return numberOfLikes;
        }
    }
}