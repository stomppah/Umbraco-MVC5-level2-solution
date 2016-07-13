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
            var memberService = Services.MemberService;
            var contentService = Services.ContentService;
            var relationService = Services.RelationService;

            var member = memberService.GetById(Members.GetCurrentMemberId());

            var post = contentService.GetById(id);

            if (!relationService.AreRelated(post, member, "likes"))
                relationService.Relate(post, member, "likes");

            var likes = relationService.GetByParent(post, "likes").Count();

            post.SetValue("likes", likes);

            contentService.PublishWithStatus(post);
            return likes;
        }
    }
}