namespace Offwind.Products.OpenFoam
{
    // ReSharper disable InconsistentNaming
    public enum OpenFoamItemType
    {
        Constant_Gravitation,
        Constant_Omega,
        Constant_TransportProperties,

        System_ControlDict,
        System_Schemes,
        System_Solution,

        Initial_pd,
        Initial_p,
        Initial_T,
        Initial_U,

        RunSimulation,
    }
    // ReSharper restore InconsistentNaming
}
