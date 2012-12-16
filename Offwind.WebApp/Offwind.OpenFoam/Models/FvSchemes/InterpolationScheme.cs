namespace Offwind.Sowfa.System.FvSchemes
{
    public enum BoundView
    {
        None, Range, Name
    }

    public sealed class InterpolationScheme : SchemeHeader
    {
        public InterpolationType interpolation { set; get; }
        public decimal psi { set; get; }
        public BoundView view { set; get; }
        public decimal lower_limit { set; get; }
        public decimal upper_limit { set; get; }
        public string flux { set; get; }

        public InterpolationScheme()
        {
            interpolation = InterpolationType.linear;
            psi = 0;
            flux = null;
            view = BoundView.None;
            upper_limit = lower_limit = 0;
        }

        public static bool StrictlyBoundedField(InterpolationType x)
        {
            if ((x == InterpolationType.limitedLinear)
                || (x == InterpolationType.vanLeer)
                || (x == InterpolationType.Gamma)
                || (x == InterpolationType.limitedCubic)
                || (x == InterpolationType.MUSCL)
                ) return true;
            return false;
        }

        public static bool AllowedLimitedPrefix(InterpolationType x)
        {
            if ((x == InterpolationType.limitedCubic) ||
                (x == InterpolationType.limitedLinear))
                return false;
            return true;
        }
    }
}
