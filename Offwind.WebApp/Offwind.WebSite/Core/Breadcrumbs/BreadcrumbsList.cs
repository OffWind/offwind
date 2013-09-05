using System.Collections;
using System.Collections.Generic;

namespace Offwind.Web.Core.Breadcrumbs
{
    public sealed class BreadcrumbsList : IEnumerable<BreadcrumbsItem>
    {
        private readonly List<BreadcrumbsItem> _inner = new List<BreadcrumbsItem>();

        public BreadcrumbsList Add(string title, string url = "")
        {
            _inner.Add(new BreadcrumbsItem { Title = title, Url = url});
            return this;
        }

        public int Count { get { return _inner.Count; } }

        public IEnumerator<BreadcrumbsItem> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}