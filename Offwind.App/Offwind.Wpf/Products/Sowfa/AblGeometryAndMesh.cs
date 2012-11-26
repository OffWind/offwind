using Offwind.Common;
using Offwind.UI.Sowfa;

namespace Offwind.ProjectDescriptors.Sowfa
{
    public sealed class AblGeometryAndMesh : ProjectDescriptor
    {
        public AblGeometryAndMesh()
        {
            Order = 2;
            var config = new SowfaProjectConfiguration();

            Category = ProjectCategory.SOWFA;
            Name = "ABL Geometry & Mesh";
            Code = ProjectCode.SowfaGeometyAndMesh;
            Description =
                @"A convenient geometry and mesh generator for Atmospheric Boundary Layer (ABL) based on blockMesh utility.";

            DefaultItems.AddRange(new[]
            {
                config.ProjectItemsMap[SowfaProjectItemType.Preprocessor_AblGeometry],
                config.ProjectItemsMap[SowfaProjectItemType.Preprocessor_AblMesh]
            });
        }
    }
}
