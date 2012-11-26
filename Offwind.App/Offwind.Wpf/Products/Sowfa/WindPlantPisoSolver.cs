using Offwind.Common;
using Offwind.Projects;

namespace Offwind.Products.Sowfa
{
    public sealed class WindPlantPisoSolver : ProjectDescriptor
    {
        public WindPlantPisoSolver()
        {
            SkipStandalone = true;
            Order = 42;
            var config = new SowfaProjectConfiguration();
            ProductType = ProductType.CFD;
            Name = "Wind Plant PISO Solver";
            Code = ProductCode.Sowfa_WindPlantPiso;
            Description = @"A specialized version of ABLPisoSolver for performing LES of wind plant flow. It includes the ability to include actuator line turbine models with local grid refinement around the turbine.";
            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[SowfaProjectItemType.Constant_Gravitation],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_Omega],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TransportProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TurbinesProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_AblProperties],

                config.ProjectItemsMap[SowfaProjectItemType.WP_controlDict],
                config.ProjectItemsMap[SowfaProjectItemType.WP_changeDict],
                config.ProjectItemsMap[SowfaProjectItemType.WP_setFieldsAblDict],
                config.ProjectItemsMap[SowfaProjectItemType.System_Schemes],
                config.ProjectItemsMap[SowfaProjectItemType.System_Solution],

                config.ProjectItemsMap[SowfaProjectItemType.WP_0_pd],
                config.ProjectItemsMap[SowfaProjectItemType.WP_0_T],
                config.ProjectItemsMap[SowfaProjectItemType.WP_0_U],
            });
        }
    }
}
