using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.AblGeneralSettings
{
    public sealed class VGeneralSettings : BaseViewModel
    {
        public bool RunParallel
        {
            get { return GetProperty<bool>("RunParallel"); }
            set { SetProperty("RunParallel", value); }
        }

        public int ParallelProcessors
        {
            get { return GetProperty<int>("ParallelProcessors"); }
            set { SetProperty("ParallelProcessors", value); }
        }

        public bool RequireMeshRefinement
        {
            get { return GetProperty<bool>("RequireMeshRefinement"); }
            set { SetProperty("RequireMeshRefinement", value); }
        }


        public int MeshRefinementLevel
        {
            get { return GetProperty<int>("MeshRefinementLevel"); }
            set { SetProperty("MeshRefinementLevel", value); }
        }

    }
}
