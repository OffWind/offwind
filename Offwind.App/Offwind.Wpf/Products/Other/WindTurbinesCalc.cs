using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.Other
{
    public sealed class WindTurbinesCalc : ProjectDescriptor
    {
        public WindTurbinesCalc()
        {
            ProductType = ProductType.EngineeringTools;
            Name = "Wind Turbines Efficiency";
            Code = ProductCode.Engineering_WindTurbinesCalc;
            Description = @"Wind Turbines Efficiency Calculator & Comparison";

            new ProjectItemDescriptor()
                .SetDefaultName("Project Settings")
                //.SetForm(typeof(FWindTurbinesCalc))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("Other_WindTurbines")
                .AddTo(DefaultItems);
        }
    }
}
