using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.WindWave
{
    public sealed class WindWaveProjectDescriptor : ProjectDescriptor
    {
        public WindWaveProjectDescriptor()
        {
            Order = 201;
            ProductType = ProductType.EngineeringTools;
            Name = "WindWave Interactions Toolkit";
            Code = ProductCode.Engineering_WindWave;
            Description = @"The purpose of this part of the software is to calculate simple wind-wave interactions by take into account the effect of the waves on the wind speed.";

            new ProjectItemDescriptor()
                .SetDefaultName("Calculator")
                .SetForm(typeof(CWindWave))
                .SetHandler(typeof(StubFileHandler))
                .SetCode("MainForm")
                .AddTo(DefaultItems);
        }

        public override object CreateProjectModel()
        {
            var m = new VWindWave();
            m.Ug = 7;
            m.Zg = 20;
            m.Zhub = 100;
            m.Td = 100;
            m.Ef = 35;
            m.Cw = 2;
            return m;
        }
    }
}
