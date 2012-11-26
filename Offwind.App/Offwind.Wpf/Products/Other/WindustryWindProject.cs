using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.Other
{
    public sealed class WindustryWindProject : ProjectDescriptor
    {
        public WindustryWindProject()
        {
            ProductType = ProductType.EngineeringTools;
            Name = "Windustry Wind Project";
            Code = ProductCode.Engineering_WindustryWind;
            Description = @"Windustry Wind Project";

            new ProjectItemDescriptor()
                .SetDefaultName("Project Settings")
                //.SetForm(typeof(FWindustryWindPlant))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("Other_WindProject")
                .AddTo(DefaultItems);
        }
    }
}
