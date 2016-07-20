﻿using System.Linq;
using System.Web.Mvc;
using Umbraco.Course.Models;
using Umbraco.Web.Mvc;

namespace Umbraco.Course.Controllers
{
    public class SearchController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Search(string query)
        {
            var model = new SearchModel()
            {
                Query = query
            };
            if (!string.IsNullOrEmpty(query))
            {
                model.SearchResults = Umbraco.TypedSearch(query);
            }
            return PartialView("~/Views/Partials/SearchPartial.cshtml" ,model);
        }
    }
}
