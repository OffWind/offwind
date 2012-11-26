using Offwind.Infrastructure.Models;

namespace Offwind.Products.OpenFoam.UI.TurbulenceProperties
{
    public sealed class VTurbulenceProperties : BaseViewModel
    {
        public SimulationType SimulationType
        {
            get { return GetProperty<SimulationType>("SimulationType"); }
            set { SetPropertyEnum("SimulationType", value); }
        }


    }
}
