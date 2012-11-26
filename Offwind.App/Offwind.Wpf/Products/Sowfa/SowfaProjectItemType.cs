namespace Offwind.Products.Sowfa
{
    // ReSharper disable InconsistentNaming
    public enum SowfaProjectItemType
    {
        Constant_Gravitation,
        Constant_Omega,
        Constant_TransportProperties,
        Constant_AblProperties,
        Constant_LesProperties,
        Constant_TurbulenceProperties,
        Constant_TurbinesProperties,
        Constant_TurbineArrayProperties,

        System_ControlDict,
        System_Schemes,
        System_Solution,

        ABL_Geometry,
        ABL_GeneralSettings,
        ABL_Schemes,
        ABL_AblProperties,
        ABL_TransportProperties,
        ABL_setFieldsAblDict,
        ABL_controlDict_1,
        ABL_controlDict_2,
        ABL_0_pd,
        ABL_0_T,
        ABL_0_U,

        WP_Schemes,
        WP_AblProperties,
        WP_TransportProperties,
        WP_setFieldsAblDict,
        WP_controlDict,
        WP_changeDict,
        WP_0_pd,
        WP_0_T,
        WP_0_U,

        WP_FAST_controlDict,
        WP_FAST_0_pd,
        WP_FAST_0_T,
        WP_FAST_0_U,

        FAST_controlDict,
        FAST_0_p,
        FAST_0_nuSgs,
        FAST_0_U,

        RunSimulation,
    }
    // ReSharper restore InconsistentNaming
}
