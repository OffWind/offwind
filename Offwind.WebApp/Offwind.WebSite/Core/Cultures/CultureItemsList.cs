using System.Collections;
using System.Collections.Generic;

namespace Offwind.Web.Core.Cultures
{
    public sealed class CultureItemsList : IEnumerable<CultureItem>
    {
        private readonly List<CultureItem> _inner = new List<CultureItem>();

        public CultureItemsList Add(string title, string url = "", bool isActive = false)
        {
            _inner.Add(new CultureItem { Title = title, Url = url, IsActive = isActive });
            return this;
        }

        public IEnumerator<CultureItem> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}