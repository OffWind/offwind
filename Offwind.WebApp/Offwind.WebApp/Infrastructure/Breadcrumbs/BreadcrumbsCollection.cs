using System.Collections;
using System.Collections.Generic;

namespace Offwind.WebApp.Infrastructure.Breadcrumbs
{
    public sealed class BreadcrumbsCollection : IEnumerable<BreadcrumbsItem>
    {
        private readonly List<BreadcrumbsItem> _inner = new List<BreadcrumbsItem>();

        public BreadcrumbsCollection Add(string title, string url = "", bool isLast = false)
        {
            _inner.Add(new BreadcrumbsItem { Title = title, Url = url, IsLast = isLast });
            return this;
        }

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