using Offwind.Infrastructure.Models;
using Offwind.Projects;

namespace Offwind.NewCase
{
    public sealed class VNewCase : BaseViewModel
    {
        public ProjectDescriptor ProjectDescriptor { get; set; }

        public string CaseName
        {
            get { return GetProperty<string>("CaseName"); }
            set { SetProperty("CaseName", value); }
        }

        public string CaseLocation
        {
            get { return GetProperty<string>("CaseLocation"); }
            set { SetProperty("CaseLocation", value); }
        }

        public string CaseDir
        {
            get { return GetProperty<string>("CaseDir"); }
            set { SetProperty("CaseDir", value); }
        }
    }
}
