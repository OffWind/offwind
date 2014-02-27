using System;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class DescriptionAttribute : Attribute
    {
        private readonly string _partition;
        private readonly int _index;

        public string Partition { get { return _partition; } }

        public int Index { get { return _index; } }

        public DescriptionAttribute(string partition,int index)
        {
            _partition = partition;
            _index = index;
        }
    }
}