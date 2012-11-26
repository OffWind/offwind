using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Projects.Persistence;

namespace Offwind.Projects
{
    public sealed class VCase : BaseViewModel
    {
        public string Name
        {
            get { return GetProperty<string>("Name"); }
            set { SetProperty("Name", value); }
        }


        public string CaseDir
        {
            get { return GetProperty<string>("CaseDir"); }
            set { SetProperty("CaseDir", value); }
        }


        public ObservableCollection<VCaseItem> Items { get; set; }

        public VProject FindByProjectItem(VProjectItem pItem)
        {
            foreach (var vCaseItem in Items)
            {
                if (vCaseItem is VProject)
                {
                    var p = (VProject) vCaseItem;
                    foreach (var vProjectItem in p.Items)
                    {
                        if (vProjectItem.Id == pItem.Id) return p;
                    }
                }
            }
            return null;
        }

        public VCase()
        {
            Items = new ObservableCollection<VCaseItem>();
        }
    }
}
