using Offwind.Common;
using Offwind.Products.OpenFoam.UI;
using Offwind.Projects;

namespace Offwind.Products.OpenFoam
{
    public sealed class OpenFoamDescriptor : ProjectDescriptor
    {
        public OpenFoamDescriptor()
        {
            Order = 1;
            var config = new OpenFoamConfiguration();

            ProductType = ProductType.CFD;
            Name = "OpenFOAM";
            Code = ProductCode.CFD_OpenFoam;
            Description = @"icoFoam solves the incompressible laminar Navier-Stokes equations using the PISO algorithm.<LineBreak/>The code is inherently transient, requiring an initial condition (such as zero velocity) and boundary conditions. The icoFOAM code can take mesh non-orthogonality into account with successive non-orthogonality iterations.<LineBreak/>The number of PISO corrections and non-orthogonality corrections are controlled through user input. ";
            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[OpenFoamItemType.Initial_p],
                config.ProjectItemsMap[OpenFoamItemType.Initial_U],
                config.ProjectItemsMap[OpenFoamItemType.Constant_TransportProperties],
                config.ProjectItemsMap[OpenFoamItemType.System_ControlDict],
                config.ProjectItemsMap[OpenFoamItemType.System_Schemes],
                config.ProjectItemsMap[OpenFoamItemType.System_Solution],
            });
        }
    }
}
