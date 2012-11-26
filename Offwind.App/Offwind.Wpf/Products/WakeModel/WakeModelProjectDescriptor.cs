using DevExpress.Xpf.Ribbon.Customization;
using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Projects;

namespace Offwind.Products.WakeModel
{
    public sealed class WakeModelProjectDescriptor : ProjectDescriptor
    {
        public WakeModelProjectDescriptor()
        {
            Order = 30;
            ProductType = ProductType.CFD;
            Name = "Wake Model";
            Code = ProductCode.CFD_WakeModel;
            Description = @"Wake Model";

            new ProjectItemDescriptor()
                .SetDefaultName("General Properties")
                .SetForm(typeof (CGeneralInputProperties))
                .SetHandler(typeof (StubFileHandler))
                .SetCode("WakeModel_GeneralProperties")
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Turbines Properties")
                .SetForm(typeof (CTurbinesInputProperties))
                .SetHandler(typeof (StubFileHandler))
                .SetCode("WakeModel_TurbineProperties")
                .SetNoScroll(true)
                .AddTo(DefaultItems);

            new ProjectItemDescriptor()
                .SetDefaultName("Solver")
                .SetForm(typeof (CSolver))
                .SetHandler(typeof (StubFileHandler))
                .SetCode("WakeModel_Solver")
                .SetNoScroll(true)
                .AddTo(DefaultItems);
        }

        public override object CreateProjectModel()
        {
            var m = new VWakeModel();
            m.GridPointsX = 1000;
            m.GridPointsY = 1000;
            m.TurbineDiameter = 50;
            m.TurbineHeight = 70;
            m.HubThrust = 0.5m;
            m.WakeDecay = 0.02m;
            m.VelocityAtHub = 9m;
            m.AirDensity = 1.225m;
            m.UnknownProperty = 0.2m;
            m.RotationAngle = -48.4m;

            m.Turbines.AddRange(new []
                             {
                                 new VTurbine(3396.91m, 2696.66m),
                                 new VTurbine(3132.82m, 2393.34m),
                                 new VTurbine(2870.71m, 2090.02m),
                                 new VTurbine(2605.37m, 1796.49m),
                                 new VTurbine(2343.25m, 1493.17m),
                                 new VTurbine(2077.91m, 1199.63m),
                                 new VTurbine(1806.09m, 896.31m),
                                 new VTurbine(3132.82m, 2843.43m),
                                 new VTurbine(2867.48m, 2553.16m),
                                 new VTurbine(2605.37m, 2249.84m),
                                 new VTurbine(2343.25m, 1943.26m),
                                 new VTurbine(2077.91m, 1649.72m),
                                 new VTurbine(1802.85m, 1346.40m),
                                 new VTurbine(1543.98m, 1052.86m),
                                 new VTurbine(1278.63m, 750m),
                                 new VTurbine(2870.71m, 3003.24m),
                                 new VTurbine(2605.37m, 2699.93m),
                                 new VTurbine(2346.49m, 2393.34m),
                                 new VTurbine(2081.14m, 2103.07m),
                                 new VTurbine(1802.85m, 1796.49m),
                                 new VTurbine(1540.74m, 1506.21m),
                                 new VTurbine(1275.39m, 1199.63m),
                                 new VTurbine(1016.52m, 906.10m),
                                 new VTurbine(2605.37m, 3150.01m),
                                 new VTurbine(2343.25m, 2846.69m),
                                 new VTurbine(2081.14m, 2553.16m),
                                 new VTurbine(1806.09m, 2246.58m),
                                 new VTurbine(1278.63m, 1649.72m),
                                 new VTurbine(1016.52m, 1359.45m),
                                 new VTurbine(750.0m, 1052.86m),
                                 new VTurbine(2330.31m, 3280.47m),
                                 new VTurbine(2081.14m, 3003.24m),
                                 new VTurbine(1806.09m, 2699.93m),
                                 new VTurbine(1540.74m, 2406.39m),
                                 new VTurbine(1016.52m, 1809.53m),
                                 new VTurbine(750.0m, 1506.21m),
                                 new VTurbine(1806.09m, 3150.01m),
                                 new VTurbine(1543.98m, 2859.74m),
                                 new VTurbine(1278.63m, 2553.16m),
                                 new VTurbine(1016.52m, 2262.88m),
                                 new VTurbine(750.0m, 1956.30m),
                                 new VTurbine(1540.74m, 3309.83m),
                                 new VTurbine(1278.63m, 3003.24m),
                                 new VTurbine(1016.52m, 2709.71m),
                                 new VTurbine(750.0m, 2406.39m),
                                 new VTurbine(1278.63m, 3455.94m),
                                 new VTurbine(1013.28m, 3166.32m),
                                 new VTurbine(750.0m, 2859.74m),
                             });
            return m;
        }
    }
}