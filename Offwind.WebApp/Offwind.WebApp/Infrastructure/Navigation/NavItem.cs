using System.Collections;
using System.Collections.Generic;

namespace Offwind.WebApp.Infrastructure.Navigation
{
    public class NavItem<TUrl> : IEnumerable<NavItem<TUrl>>
        where TUrl : class
    {
        protected readonly List<NavItem<TUrl>> _inner = new List<NavItem<TUrl>>();

        public string Title { get; set; }
        public TUrl Url { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Add new subgroup
        /// </summary>
        /// <param name="title">Group title</param>
        /// <param name="url">Group URL</param>
        /// <param name="isActive">[Optional, false] Is group active (visible, expanded etc.)</param>
        /// <param name="icon">[Optional, ""] Icon for this item</param>
        /// <returns>Newly created subgroup</returns>
        public NavItem<TUrl> AddGroup(string title, TUrl url = null, bool isActive = false, string icon = "")
        {
            var subGroup = new NavItem<TUrl> { Title = title, Url = url, IsActive = isActive };
            _inner.Add(subGroup);
            return subGroup;
        }

        /// <summary>
        /// Add new item.
        /// </summary>
        /// <param name="title">Item title</param>
        /// <param name="url">Item URL</param>
        /// <param name="isActive">[Optional, false] Is item active (visible, expanded etc.)</param>
        /// <param name="icon">[Optional, ""] Icon for this item</param>
        /// <returns>Returns this group so that you can continue adding more items</returns>
        public NavItem<TUrl> AddItem(string title, TUrl url = null, bool isActive = false, string icon = "")
        {
            _inner.Add(new NavItem<TUrl> { Title = title, Url = url });
            return this;
        }

        public IEnumerator<NavItem<TUrl>> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}