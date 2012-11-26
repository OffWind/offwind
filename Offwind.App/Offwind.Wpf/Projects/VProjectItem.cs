using System;
using Offwind.Infrastructure.Models;

namespace Offwind.Projects
{
    public sealed class VProjectItem : BaseViewModel
    {
        public Guid Id { get; set; }

        public string DisplayName
        {
            get { return GetProperty<string>("DisplayName"); }
            set { SetProperty("DisplayName", value); }
        }

        public ProjectItemDescriptor Descriptor { get; set; }

        public VProjectItem()
        {
            Id = Guid.NewGuid();
        }

        public VProjectItem(ProjectItemDescriptor d, string displayName)
        {
            Id = Guid.NewGuid();
            Descriptor = d;
            DisplayName = displayName;
        }
    }
}
