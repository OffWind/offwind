namespace Offwind.Products.OpenFoam.Models.Fields
{
    public class Patch
    {
        /// <summary>
        /// Standard OpenFOAM patch type. If set to custom, then <see cref="CustomType"/> must be defined.
        /// </summary>
        public PatchType PatchType { get; set; }

        /// <summary>
        /// This is only set when <see cref="PatchType"/> is set to "custom"
        /// </summary>
        public string CustomType { get; set; }

        public PatchValue Value { get; set; }
    }
}
