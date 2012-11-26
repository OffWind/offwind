using Offwind.Common;
using Offwind.Projects;

namespace Offwind.Products.Sowfa
{
    public sealed class AblPisoSolver : ProjectDescriptor
    {
        public AblPisoSolver()
        {
            SkipStandalone = true;
            Order = 41;
            var config = new SowfaProjectConfiguration();

            ProductType = ProductType.CFD;
            Name = "ABL PISO Solver";
            Code = ProductCode.Sowfa_AblPiso;
            Description =
                @"A large-eddy simulation (LES) solver for computing atmospheric boundary layer turbulent flow with the ability to specify surface roughness, stability, and wind speed and direction. It must be used with  hexahedral meshes (like those created by OpenFOAM's blockMesh utility)";
            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[SowfaProjectItemType.ABL_Geometry],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_GeneralSettings],

                config.ProjectItemsMap[SowfaProjectItemType.Constant_Gravitation],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_Omega],
                config.ProjectItemsMap[SowfaProjectItemType.System_Solution],

                config.ProjectItemsMap[SowfaProjectItemType.ABL_TransportProperties],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_AblProperties],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_setFieldsAblDict],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_controlDict_1],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_controlDict_2],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_Schemes],

                config.ProjectItemsMap[SowfaProjectItemType.ABL_0_pd],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_0_T],
                config.ProjectItemsMap[SowfaProjectItemType.ABL_0_U],
            });
        }
    }
}
