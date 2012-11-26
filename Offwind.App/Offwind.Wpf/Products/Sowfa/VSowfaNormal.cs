using Offwind.Infrastructure.Models;
using Offwind.Products.OpenFoam.UI.RunSimulation;

namespace Offwind.Products.Sowfa
{
    public class VSowfaNormal : BaseViewModel
    {
        public VRunSimulation RunSimulation { get; private set; }

        public VSowfaNormal()
        {
            RunSimulation = new VRunSimulation();
        }
    }
}
