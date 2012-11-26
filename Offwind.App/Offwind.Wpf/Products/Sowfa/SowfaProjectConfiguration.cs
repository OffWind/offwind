using System;
using System.Collections.Generic;
using Offwind.Common;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.UI.ControlDict;
using Offwind.Products.OpenFoam.UI.TransportProperties;
using Offwind.Products.OpenFoam.UI.fvSchemes;
using Offwind.Products.Sowfa.Defaults;
using Offwind.Products.Sowfa.UI.AblGeneralSettings;
using Offwind.Products.Sowfa.UI.AblGeometry;
using Offwind.Products.Sowfa.UI.AblProperties;
using Offwind.Products.Sowfa.UI.SetFieldsAbl;
using Offwind.Products.Sowfa.UI.TurbinesSetup;
using Offwind.Projects;
using Offwind.Sowfa.Constant.AblProperties;
using Offwind.Sowfa.Constant.Gravitation;
using Offwind.Sowfa.Constant.Omega;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.Sowfa.System.ControlDict;
using Offwind.Sowfa.System.DecomposeParDict;
using Offwind.Sowfa.System.FvSchemes;
using Offwind.Sowfa.System.FvSolution;
using Offwind.Sowfa.System.SetFieldsAblDict;
using Offwind.Sowfa.Time.FieldData;

namespace Offwind.Products.Sowfa
{
    public sealed class SowfaProjectConfiguration : IProjectConfiguration
    {
        public Dictionary<SowfaProjectItemType, ProjectItemDescriptor> ProjectItemsMap { get; private set; }

        public SowfaProjectConfiguration()
        {
            ProjectItemsMap = new Dictionary<SowfaProjectItemType, ProjectItemDescriptor>();

            new ProjectItemDescriptor()
                .SetDefaultName("Geometry")
                //.SetForm(typeof(FAblGeometry))
                .SetHandler(typeof(AblGeometryHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_Geometry);

            new ProjectItemDescriptor()
                .SetDefaultName("General Settings")
                .SetForm(typeof(CAblGeneralSettings))
                .SetHandler(typeof(DecomposeParDictHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_GeneralSettings);

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
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_Gravitation);

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
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_Omega);

            new ProjectItemDescriptor()
                .SetDefaultName("transportProperties")
                .SetForm(typeof(CTransportProperties))
                .SetHandler(typeof(TransportPropertiesHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_TransportProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("ABL Properties")
                .SetForm(typeof(CABLProperties))
                .SetHandler(typeof(AblPropertiesHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_AblProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("LES Properties")
                //.SetForm(typeof(FLesProperties))
                .SetHandler(typeof(StubFileHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_LesProperties);
            /*
            new ProjectItemDescriptor()
                .SetEditorGroup(CaseItemType.SolverConstant)
                .SetDefaultName("Turbine Array Properties")
                .SetForm(typeof(CTurbineSetup))
                .SetHandler(typeof(TurbineArrayPropHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_TurbineArrayProperties);            
            */
            new ProjectItemDescriptor()
                .SetDefaultName("Turbines Properties")
                .SetForm(typeof(CTurbineSetup))
                .SetHandler(typeof(TurbineArrayPropHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_TurbinesProperties);
           

            new ProjectItemDescriptor()
                .SetDefaultName("Turbulence Properties")
                //.SetForm(typeof(FTurbulenceProperties))
                .SetHandler(typeof(StubFileHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.Constant_TurbulenceProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.System_ControlDict);

            new ProjectItemDescriptor()
                .SetDefaultName("Change Dictionary")
                .SetNodeInvisible(true)
                //.SetForm(typeof(TestMdiChild))
                .SetHandler(typeof(StubFileHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_changeDict);

            new ProjectItemDescriptor()
                .SetDefaultName("Numerical schemes")
                .SetForm(typeof(CFvScheme))
                .SetHandler(typeof(FvSchemesHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.System_Schemes);

            new ProjectItemDescriptor()
                .SetDefaultName("fvSolution")
                .SetNodeInvisible(true)
                //.SetForm(typeof(TestMdiChild))
                .SetHandler(typeof(FvSolutionHandler))
                .AddTo(ProjectItemsMap, SowfaProjectItemType.System_Solution);

            #region FAST

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = FastDefaultsRes.FAST_controlDict;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.FAST_controlDict);

            new ProjectItemDescriptor()
                .SetDefaultName("nuSgs")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "nuSgs";
                    h.DefaultData = FastDefaultsRes.FAST_nuSgs;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.FAST_0_nuSgs);

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
                .AddTo(ProjectItemsMap, SowfaProjectItemType.FAST_0_p);

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
                .AddTo(ProjectItemsMap, SowfaProjectItemType.FAST_0_U);

            #endregion

            #region ABL

            new ProjectItemDescriptor()
                .SetDefaultName("ABL Properties")
                .SetForm(typeof(CABLProperties))
                .SetHandler(typeof(AblPropertiesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = AblDefaultsRes.constant_ABLProperties;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_AblProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("transportProperties")
                .SetForm(typeof(CTransportProperties))
                .SetHandler(typeof(TransportPropertiesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = AblDefaultsRes.constant_transportProperties;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_TransportProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict (0; QE)")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.FileSuffix = "1";
                    h.DefaultData = AblDefaultsRes.system_controlDict_1;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_controlDict_1);

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict (QE; QE+T)")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.FileSuffix = "2";
                    h.DefaultData = AblDefaultsRes.system_controlDict_2;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_controlDict_2);

            new ProjectItemDescriptor()
                .SetDefaultName("setFieldsABLDict")
                .SetForm(typeof(FSetFieldsAbl))
                .SetHandler(typeof(SetFieldsAblDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = AblDefaultsRes.system_setFieldsABLDict;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_setFieldsAblDict);

            new ProjectItemDescriptor()
                .SetDefaultName("Numerical schemes")
                .SetNodeInvisible(false)
                .SetForm(typeof(CFvScheme))
                .SetHandler(typeof(FvSchemesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = AblDefaultsRes.system_fvSchemes;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_Schemes);

            new ProjectItemDescriptor()
                .SetDefaultName("pd")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler) handler;
                    h.FileName = "pd";
                    h.DefaultData = AblDefaultsRes.time_pd;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_0_pd);

            new ProjectItemDescriptor()
                .SetDefaultName("U")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "U";
                    h.DefaultData = AblDefaultsRes.time_U;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_0_U);

            new ProjectItemDescriptor()
                .SetDefaultName("T")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "T";
                    h.DefaultData = AblDefaultsRes.time_T;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.ABL_0_T);

            #endregion

            #region WindPlant

            new ProjectItemDescriptor()
                .SetDefaultName("ABL Properties")
                .SetForm(typeof(CABLProperties))
                .SetHandler(typeof(AblPropertiesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantDefaultsRes.constant_ABLProperties;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_AblProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("transportProperties")
                .SetForm(typeof(CTransportProperties))
                .SetHandler(typeof(TransportPropertiesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantDefaultsRes.constant_transportProperties;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_TransportProperties);

            new ProjectItemDescriptor()
                .SetDefaultName("Numerical schemes")
                .SetNodeInvisible(false)
                .SetForm(typeof(CFvScheme))
                .SetHandler(typeof(FvSchemesHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantDefaultsRes.system_fvSchemes;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_Schemes);

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantDefaultsRes.system_controlDict;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_controlDict);

            new ProjectItemDescriptor()
                .SetDefaultName("setFieldsABLDict")
                //.SetForm(typeof(FSetFieldsAbl))
                .SetHandler(typeof(SetFieldsAblDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantDefaultsRes.system_setFieldsABLDict;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_setFieldsAblDict);

            new ProjectItemDescriptor()
                .SetDefaultName("pd")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "pd";
                    h.DefaultData = WindPlantDefaultsRes.time_pd;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_0_pd);

            new ProjectItemDescriptor()
                .SetDefaultName("U")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "U";
                    h.DefaultData = WindPlantDefaultsRes.time_U;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_0_U);

            new ProjectItemDescriptor()
                .SetDefaultName("T")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "T";
                    h.DefaultData = WindPlantDefaultsRes.time_T;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_0_T);

            #endregion

            #region WindPlant FAST

            new ProjectItemDescriptor()
                .SetDefaultName("controlDict")
                .SetForm(typeof(CControlDict))
                .SetHandler(typeof(ControlDictHandler))
                .SetHandlerInitializer(h =>
                {
                    h.DefaultData = WindPlantFastDefaultsRes.WP_FAST_controlDict;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_FAST_controlDict);

            new ProjectItemDescriptor()
                .SetDefaultName("pd")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "pd";
                    h.DefaultData = WindPlantFastDefaultsRes.WP_FAST_pd;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_FAST_0_pd);

            new ProjectItemDescriptor()
                .SetDefaultName("U")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "U";
                    h.DefaultData = WindPlantFastDefaultsRes.WP_FAST_U;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_FAST_0_U);

            new ProjectItemDescriptor()
                .SetDefaultName("T")
                //.SetForm(typeof(FFieldData))
                .SetHandler(typeof(FieldDataHandler))
                .SetHandlerInitializer(handler =>
                {
                    var h = (FieldDataHandler)handler;
                    h.FileName = "T";
                    h.DefaultData = WindPlantFastDefaultsRes.WP_FAST_T;
                })
                .AddTo(ProjectItemsMap, SowfaProjectItemType.WP_FAST_0_T);

            #endregion

            //new ProjectItemDescriptor()
            //    .SetDefaultName("Run Simulation")
            //    .SetForm(typeof(CRunSimulation))
            //    .SetHandler(typeof(StubFileHandler))
            //    .AddTo(ProjectItemsMap, SowfaProjectItemType.RunSimulation);
        }

        public ProjectDescriptor GetDescriptor(string code)
        {
            switch (code)
            {
                case ProductCode.CFD_SowfaNormal:
                    return new SowfaProject();
                //case ProductCode.Sowfa_AblPiso:
                //    return new AblPisoSolver();
                //case ProductCode.Sowfa_WindPlantPiso:
                //    return new WindPlantPisoSolver();
                //case ProjectCode.SowfaPisoFast:
                //    return new FastPisoSolver();
                //case ProjectCode.SowfaWindPlantPisoFast:
                //    return new WindPlantPisoFastSolver();
                default:
                    return null;
            }
        }

        public ProjectItemDescriptor GetItemDescriptor(string code)
        {
            var editorType = (SowfaProjectItemType)Enum.Parse(typeof(SowfaProjectItemType), code);
            return ProjectItemsMap[editorType];
        }
    }
}
