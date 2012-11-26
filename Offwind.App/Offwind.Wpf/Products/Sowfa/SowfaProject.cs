using Offwind.Common;
using Offwind.Projects;

namespace Offwind.Products.Sowfa
{
    public sealed class SowfaProject : ProjectDescriptor
    {
        public SowfaProject()
        {
            Order = 40;
            CaseInitializer = new SowfaProjectCaseInitializer();
            ProductType = ProductType.CFD;
            Name = "SOWFA";
            Code = ProductCode.CFD_SowfaNormal;
            Description = @"Offshore Wind Plant Simulation";
        }
    }
}
