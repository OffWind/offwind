using System;
using System.Collections.Generic;
using Offwind.Common;
using Offwind.Products.OpenFoam.UI.ControlDict;
using Offwind.Products.OpenFoam.UI.TransportProperties;
using Offwind.Products.OpenFoam.UI.fvSchemes;
using Offwind.Products.Sowfa.Defaults;
using Offwind.Projects;
using Offwind.Sowfa.Constant.Gravitation;
using Offwind.Sowfa.Constant.Omega;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.System.ControlDict;
using Offwind.Sowfa.System.FvSchemes;
using Offwind.Sowfa.System.FvSolution;
using Offwind.Sowfa.Time.FieldData;

namespace Offwind.Products.OpenFoam
{
    public sealed class OpenFoamConfiguration : IProjectConfiguration
    {
        public Dictionary<OpenFoamItemType, ProjectItemDescriptor> ProjectItemsMap { get; private set; }

        public OpenFoamConfiguration()
        {
            ProjectItemsMap = new Dictionary<OpenFoamItemType, ProjectItemDescriptor>();

            new ProjectItemDescriptor()
                .SetDefaultName("g")
                .SetNodeInvisible(true)
                //.SetForm(typeof(FDimensionsAndValue))
                .SetFormInitializer(form =>
                {
                    //var f = (FDimensionsAndValue)form;
                    //f.InitValue(0, 0, -9.81m);
                    //f.InitDimText("m/s^2");
                    //f.InitDim(0, 1, -2, 0, 0, 0, 0);
                })
                .SetHandler(typeof(GravitationHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.Constant_Gravitation);

            new ProjectItemDescriptor()
                .SetDefaultName("Omega")
                .SetNodeInvisible(true)
                //.SetForm(typeof(FDimensionsAndValue))
                .SetFormInitializer(form =>
                {
                    //var f = (FDimensionsAndValue)form;
                    //f.InitValue(0, 0.5142226E-4m, 0.5142226E-4m);
                    //f.InitDimText("1/s");
                    //f.InitDim(0, 0, -1, 0, 0, 0, 0);
                })
                .SetHandler(typeof(OmegaHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.Constant_Omega);

            new ProjectItemDescriptor()
                .SetDefaultName("transportProperties")
                .SetForm(typeof(CTransportProperties))
                .SetHandler(typeof(TransportPropertiesHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.Constant_TransportProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.System_ControlDict);

            new ProjectItemDescriptor()
                .SetDefaultName("Numerical schemes")
                .SetForm(typeof(CFvScheme))
                .SetHandler(typeof(FvSchemesHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.System_Schemes);

            new ProjectItemDescriptor()
                .SetDefaultName("fvSolution")
                .SetNodeInvisible(true)
                //.SetForm(typeof(TestMdiChild))
                .SetHandler(typeof(FvSolutionHandler))
                .AddTo(ProjectItemsMap, OpenFoamItemType.System_Solution);

            new ProjectItemDescriptor()
                .SetDefaultName("p")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "p";
                    h.DefaultData = FastDefaultsRes.FAST_p;
                })
                .AddTo(ProjectItemsMap, OpenFoamItemType.Initial_p);

            new ProjectItemDescriptor()
                .SetDefaultName("U")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "U";
                    h.DefaultData = FastDefaultsRes.FAST_U;
                })
                .AddTo(ProjectItemsMap, OpenFoamItemType.Initial_U);
        }

        public ProjectDescriptor GetDescriptor(string code)
        {
            switch (code)
            {
                case ProductCode.CFD_OpenFoam:
                    return new OpenFoamDescriptor();
                default:
                    return null;
            }
        }

        public ProjectItemDescriptor GetItemDescriptor(string code)
        {
            var editorType = (OpenFoamItemType)Enum.Parse(typeof(OpenFoamItemType), code);
            return ProjectItemsMap[editorType];
        }
    }
}
