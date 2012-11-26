using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.Other
{
    public sealed class PowerGenerator : ProjectDescriptor
    {
        public PowerGenerator()
        {
            ProductType = ProductType.EngineeringTools;
            Name = "Power Generator";
            Code = ProductCode.Engineering_PowerGenerator;
            Description = @"Wind Turbine Power Generator Equation Formulas Design Calculator. Windmill - Renewable Energy - Clean Electricity - Green Home - Solar Power";

            new ProjectItemDescriptor()
                .SetDefaultName("Project Settings")
                //.SetForm(typeof(FPowerGeneratorCalc))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("Other_WindProject")
                .AddTo(DefaultItems);
        }
    }
}
