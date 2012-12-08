namespace Offwind.Products.OpenFoam.Models.Fields
{
    public enum PatchType
    {
        undefined,
        patch,
        wall,
        symmetry,
        empty,
        wedge,
        cyclic,
        processor,

        buoyantPressureMod,
        zeroGradient,
        fixedGradient,
        fixedValue,
        slip,
    }
}
