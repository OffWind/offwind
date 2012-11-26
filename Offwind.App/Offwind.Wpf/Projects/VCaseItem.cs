using System;
using Offwind.Infrastructure.Models;

namespace Offwind.Projects
{
    public class VCaseItem : BaseViewModel
    {
        public Guid Id
        {
            get { return GetProperty<Guid>("Id"); }
            set { SetProperty("Id", value); }
        }


        public string DisplayName
        {
            get { return GetProperty<string>("DisplayName"); }
            set { SetProperty("DisplayName", value); }
        }


        public string RelativePath
        {
            get { return GetProperty<string>("RelativePath"); }
            set { SetProperty("RelativePath", value); }
        }


        public bool IsStartup
        {
            get { return GetProperty<bool>("IsStartup"); }
            set { SetProperty("IsStartup", value); }
        }


        public VCaseItem()
        {
        }

        public VCaseItem(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
