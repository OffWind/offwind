using Offwind.Common;
using Offwind.Products.Sowfa;
using Offwind.Projects;

namespace Offwind.ProjectDescriptors.Sowfa
{
    public sealed class WindPlantPisoFastSolver : ProjectDescriptor
    {
        public WindPlantPisoFastSolver()
        {
            Order = 6;
            var config = new SowfaProjectConfiguration();
            ProductType = ProductType.CFD;
            Name = "Wind Plant PISO-FAST Solver";
            Code = ProductCode.Sowfa_WindPlantPisoFast;
            Description = @"Like windPlantPisoSolver with FAST coupled to the actuator line turbine model.";
            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[SowfaProjectItemType.Constant_Gravitation],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_Omega],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TransportProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_AblProperties],
                config.ProjectItemsMap[SowfaProjectItemType.WP_FAST_controlDict],
                config.ProjectItemsMap[SowfaProjectItemType.WP_changeDict],
                config.ProjectItemsMap[SowfaProjectItemType.System_Schemes],
                config.ProjectItemsMap[SowfaProjectItemType.System_Solution],
                config.ProjectItemsMap[SowfaProjectItemType.WP_FAST_0_pd],
                config.ProjectItemsMap[SowfaProjectItemType.WP_FAST_0_T],
                config.ProjectItemsMap[SowfaProjectItemType.WP_FAST_0_U],
            });
        }
    }
}
