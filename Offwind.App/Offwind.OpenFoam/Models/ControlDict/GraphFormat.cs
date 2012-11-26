namespace Offwind.Products.OpenFoam.Models.ControlDict
{
    /// <summary>
    /// This is very sensitive as it goes directly into output files.
    /// Consider implement mapping if you want change it.
    /// </summary>
    public enum GraphFormat
    {
        raw,
        gnuplot,
        xmgr,
        jplot,
    }
}
