using System.Collections;
using System.Collections.Generic;

namespace Offwind.Web.Core.Navigation
{
    public sealed class NavItemsList : IEnumerable<NavItem>
    {
        private readonly List<NavItem> _inner = new List<NavItem>();

        public NavItemsList Add(string title, string url = "", bool isActive = false)
        {
            _inner.Add(new NavItem { Title = title, Url = url, IsActive = isActive });
            return this;
        }

        public IEnumerator<NavItem> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}