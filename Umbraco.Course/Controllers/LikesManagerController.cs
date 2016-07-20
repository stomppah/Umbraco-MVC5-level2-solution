using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Umbraco.Course.Controllers
{
    public class LikesManagerController : UmbracoAuthorizedApiController
    {
        public IEnumerable<Umbraco.Core.Models.IRelation> GetAll(int id)
        {
            return Services.RelationService.GetByParentId(id);
        }

        public HttpResponseMessage PostDeleteById(int id)
        {
            var relation = Services.RelationService.GetById(id);
            if (relation == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            Services.RelationService.Delete(relation);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}