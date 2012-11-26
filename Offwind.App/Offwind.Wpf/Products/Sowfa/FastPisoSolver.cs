using Offwind.Common;
using Offwind.Products.Sowfa;
using Offwind.Projects;

namespace Offwind.ProjectDescriptors.Sowfa
{
    public sealed class FastPisoSolver : ProjectDescriptor
    {
        public FastPisoSolver()
        {
            Order = 4;
            var config = new SowfaProjectConfiguration();

            ProductType = ProductType.CFD;
            Name = "FAST PISO Solver";
            Code = ProductCode.Sowfa_PisoFast;
            Description = @"An incompressible Navier-Stokes LES solver (no temperature equation) with the actuator line turbine model coupled to NREL's FAST aeroelastic and turbine system dynamics code.";
            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[SowfaProjectItemType.Constant_LesProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TransportProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TurbineArrayProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TurbulenceProperties],
                config.ProjectItemsMap[SowfaProjectItemType.FAST_controlDict],
                config.ProjectItemsMap[SowfaProjectItemType.System_Schemes],
                config.ProjectItemsMap[SowfaProjectItemType.System_Solution],
                config.ProjectItemsMap[SowfaProjectItemType.FAST_0_nuSgs],
                config.ProjectItemsMap[SowfaProjectItemType.FAST_0_p],
                config.ProjectItemsMap[SowfaProjectItemType.FAST_0_U],
            });
        }
    }
}
