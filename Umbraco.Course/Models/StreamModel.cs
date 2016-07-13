using System.Linq;

namespace Umbraco.Course.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using Core.Models;
    using Web.Models;

    public class StreamModel : RenderModel
    {
        public StreamModel(IPublishedContent content, CultureInfo culture) : base(content, culture)
        {
        }

        public StreamModel(IPublishedContent content) : base(content)
        {
        }

        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public IEnumerable<IPublishedContent> StatusUpdates { get; set; }
        public int StatusUpdatesCount => StatusUpdates.Count();
    }
}