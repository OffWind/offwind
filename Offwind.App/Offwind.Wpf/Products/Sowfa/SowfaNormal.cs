using Offwind.Common;
using Offwind.Projects;

namespace Offwind.Products.Sowfa
{
    public sealed class SowfaNormal : ProjectDescriptor
    {
        public SowfaNormal()
        {
            SkipStandalone = true;
            Order = 41;
            var config = new SowfaProjectConfiguration();

            ProductType = ProductType.CFD;
            Name = "SOWFA";
            Code = ProductCode.CFD_SowfaNormal;
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

                //config.ProjectItemsMap[SowfaProjectItemType.Constant_Gravitation],
                //config.ProjectItemsMap[SowfaProjectItemType.Constant_Omega],
                //config.ProjectItemsMap[SowfaProjectItemType.Constant_TransportProperties],
                config.ProjectItemsMap[SowfaProjectItemType.Constant_TurbinesProperties],
                //config.ProjectItemsMap[SowfaProjectItemType.Constant_AblProperties],

                config.ProjectItemsMap[SowfaProjectItemType.WP_controlDict],
                config.ProjectItemsMap[SowfaProjectItemType.WP_changeDict],
                config.ProjectItemsMap[SowfaProjectItemType.WP_setFieldsAblDict],
                config.ProjectItemsMap[SowfaProjectItemType.System_Schemes],
                config.ProjectItemsMap[SowfaProjectItemType.System_Solution],

                config.ProjectItemsMap[SowfaProjectItemType.WP_0_pd],
                config.ProjectItemsMap[SowfaProjectItemType.WP_0_T],
                config.ProjectItemsMap[SowfaProjectItemType.WP_0_U],

                //config.ProjectItemsMap[SowfaProjectItemType.RunSimulation],
            });
        }


        public override object CreateProjectModel()
        {
            var m = new VSowfaNormal();
            m.RunSimulation.ParallelExecution = false;
            m.RunSimulation.ParallelProcessorsAmount = 0;
            return m;
        }
    }
}
