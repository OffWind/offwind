using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class FvSchemesHandler : FoamFileHandler
    {
        public FvSchemesHandler()
            : base("fvSchemes", null, "system", FvSchemesRes.Default)
        {
        }

        public override object Read(string path)
        {
            var obj = new FvSchemesData();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            var grammar = new NumericalSchemeGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                var dict = rootEntryNode.GetDictContent();
                switch (identifier)
                {
                    case "ddtSchemes":
                        obj.ddtSchemes = dict.DictionaryWalk(NewTimeScheme);
                        break;
                    case "gradSchemes":
                        obj.gradSchemes = dict.DictionaryWalk(NewGradientScheme);
                        break;
                    case "divSchemes":
                        obj.divSchemes = dict.DictionaryWalk(NewDivergenceScheme);
                        break;
                    case "laplacianSchemes":
                        obj.laplacianSchemes = dict.DictionaryWalk(NewLaplacianScheme);
                        break;
                    case "interpolationSchemes":
                        obj.interpolationSchemes = dict.DictionaryWalk(NewInterpolationScheme);
                        break;
                    case "snGradSchemes":
                        obj.snGradSchemes = dict.DictionaryWalk(NewSNGradScheme);
                        break;
                    case "fluxRequired":
                        obj.fluxCalculation = dict.DictionaryWalk(NewFluxCalculation);
                        break;
                }
            }
            return obj;
        }

        private static TimeScheme NewTimeScheme(string[] y)
        {
            TimeScheme x = null;
            if (y.Length > 1)
            {
                x = new TimeScheme();
                x.type = y[1].ToEnum<TimeSchemeType>();
                if (y.Length > 2)
                {
                    x.psi = Convert.ToDecimal(y[2]);
                }
            }
            return x;
        }

        private static GradientScheme NewGradientScheme(string[] y)
        {
            GradientScheme x = null;
            if (y.Length < 2) return x;

            x = new GradientScheme();
            var i = 0;
            x.SetHeader(ref y[i++]);
            switch (y[i])
            {
                case "cellLimited":
                    x.limited = LimitedType.cellLimited;
                    i++;
                    break;
                case "faceLimited":
                    x.limited = LimitedType.faceLimited;
                    i++;
                    break;
            }
            x.discretisation = y[i++].ToEnum<DiscretisationType>();
            if (x.discretisation == DiscretisationType.Gauss)
            {
                x.interpolation = y[i++].ToEnum<InterpolationType>();
            }
            if (i < y.Length)
            {
                x.psi = Convert.ToDecimal(y[i]);
            }
            return x;
        }

        private static DivergenceScheme NewDivergenceScheme(string[] y)
        {
            DivergenceScheme x = null;
            if (y.Length > 1)
            {
                x = new DivergenceScheme();
                x.SetHeader(ref y[0]);
                var idx = 1;
                if (y[1] == "Gauss")
                {
                    idx++;
                }
                else
                {
                    x.discretisation = DiscretisationType.none;
                }
                x.interpolation = y[idx++].ToEnum<InterpolationType>();
                if (y.Length > idx)
                {
                    x.psi = Convert.ToDecimal(y[idx]);
                }
            }
            return x;
        }

        private static LaplacianScheme NewLaplacianScheme(string[] y)
        {
            LaplacianScheme x = null;
            if (y.Length > 1)
            {
                x = new LaplacianScheme();
                x.SetHeader(ref y[0]);
                x.discretisation = y[1].ToEnum<DiscretisationType>();
                if (x.discretisation != DiscretisationType.Gauss) return x;
                x.interpolation  = y[2].ToEnum<InterpolationType>();
                x.snGradScheme   = y[3].ToEnum<SurfaceNormalGradientType>();
                if ((y.Length > 4) && (x.snGradScheme == SurfaceNormalGradientType.limited))
                {
                    x.psi = Convert.ToDecimal(y[4]);
                }
            }
            return x;
        }

        private static InterpolationScheme NewInterpolationScheme(string[] y)
        {
            const string isValue = @"[-|\d|\.]+";
            const string ShortLimit = @"(limitedLinear|vanLeer|Gamma|limitedCubic|MUSCL)+(\d)(\d)";
            const string RangeLimit = @"(limited(vanLeer|Gamma|MUSCL))";

            InterpolationScheme x = null;
            if (y.Length > 1)
            {
                x = new InterpolationScheme();                
                x.SetHeader( ref y[0]);

                var shortLimMatch = Regex.Match(y[1], ShortLimit);
                if (shortLimMatch.Length == 0)
                {
                    var rangeLimMatch = Regex.Match(y[1], RangeLimit);
                    if ( rangeLimMatch.Length == 0)
                    {
                        x.interpolation = y[1].ToEnum<InterpolationType>();
                    }
                    else
                    {
                        x.interpolation = rangeLimMatch.Groups[2].Value.ToEnum<InterpolationType>();
                        //x.view = BoundView.Range;
                    }
                }
                else
                {
                    x.view = BoundView.Name;
                    x.interpolation = shortLimMatch.Groups[1].Value.ToEnum<InterpolationType>();
                    x.lower_limit = Convert.ToDecimal(shortLimMatch.Groups[2].Value);
                    x.upper_limit = Convert.ToDecimal(shortLimMatch.Groups[3].Value);
                }
                switch (y.Length)
                {
                    case 5:
                        x.lower_limit = Convert.ToDecimal(y[2]);
                        x.upper_limit = Convert.ToDecimal(y[3]);
                        x.flux = String.Copy(y[4]);
                        x.view = BoundView.Range;
                        break;
                    case 4:
                        if (Regex.IsMatch(isValue,y[3]))
                        {
                            x.lower_limit = Convert.ToDecimal(y[2]);
                            x.upper_limit = Convert.ToDecimal(y[3]);
                            x.view = BoundView.Range;
                        }
                        else
                        {
                            x.psi = Convert.ToDecimal(y[2]);
                            x.flux = String.Copy(y[3]);
                        }
                        break;
                    case 3:
                        if (Regex.IsMatch(isValue, y[2]))
                        {
                            x.psi = Convert.ToDecimal(y[2]);
                        }
                        else
                        {
                            x.flux = String.Copy(y[3]);
                        }
                        break;
                }
            }
            return x;
        }

        private static SurfaceNormalGradientScheme NewSNGradScheme(string[] y)
        {
            SurfaceNormalGradientScheme x = null;
            if (y.Length > 1)
            {
                x = new SurfaceNormalGradientScheme();
                x.SetHeader( ref y[0]);
                x.type = y[1].ToEnum<SurfaceNormalGradientType>();
                if ((x.type == SurfaceNormalGradientType.limited) && (y.Length > 2))
                {
                    x.psi = Convert.ToDecimal(y[2]);
                }
            }
            return x;
        }

        private static FluxCalculation NewFluxCalculation(string[] y)
        {
            var x = new FluxCalculation();
            x.flux = String.Copy(y[0]);
            if ((y.Length > 1) && (y[1] == "no"))
            {
                x.enable = false;
            }
            return x;
        }

        public override void Write(string path, object data)
        {
            var obj = (FvSchemesData) data;
            var str = new StringBuilder(FvSchemesRes.Template);
            var culture = CultureInfo.InvariantCulture;

            var body0 = new StringBuilder(null);
            foreach (TimeScheme x in obj.ddtSchemes)
            {
                body0.Append(String.Format("\tdefault {0} {1};{2}",
                                           x.type,
                                           (x.psi != 0) ? x.psi.ToString(culture) : "",
                                           Environment.NewLine));
            }
            str.Replace("({[[ddtSchemes]]})", body0.ToString());

            var body1 = new StringBuilder(null);
            foreach (DivergenceScheme x in obj.divSchemes)
            {
                if (x.discretisation == DiscretisationType.Gauss)
                {
                    body1.Append(String.Format("\t{0} Gauss {1} {2};{3}",
                                               x.GetHeader(),
                                               (x.interpolation != InterpolationType.none) ? x.interpolation.ToString() : "",
                                               (x.psi != 0) ? x.psi.ToString(culture) : "", Environment.NewLine));
                }
                else
                {
                    body1.Append(String.Format("\t{0} none;{1}",
                                               x.GetHeader(), Environment.NewLine));
                }
            }
            str.Replace("({[[divSchemes]]})", body1.ToString());

            var body2 = new StringBuilder(null);
            foreach (GradientScheme x in obj.gradSchemes)
            {
                body2.Append(String.Format("\t{0} {1} {2} {3} {4};{5}",
                                           x.GetHeader(),
                                           (x.limited != LimitedType.none) ? x.limited.ToString() : "",
                                           (x.discretisation != DiscretisationType.none) ? x.discretisation.ToString() : "",
                                           (x.interpolation != InterpolationType.none) ? x.interpolation.ToString() : "",
                                           (x.psi != 0) ? x.psi.ToString(culture) : "",
                                           Environment.NewLine));
            }
            str.Replace("({[[gradSchemes]]})", body2.ToString());

            var body3 = new StringBuilder(null);
            foreach (LaplacianScheme x in obj.laplacianSchemes)
            {
                if (x.discretisation != DiscretisationType.none)
                {
                    body3.Append(String.Format("\t{0} {1} {2} {3} {4};{5}",
                        x.GetHeader(),
                        x.discretisation,
                        x.interpolation,
                        (x.snGradScheme != SurfaceNormalGradientType.none) ? x.snGradScheme.ToString() : "",
                        (x.psi != 0) ? x.psi.ToString(culture) : "",
                        Environment.NewLine));                    
                }
                else
                {
                    body3.Append(String.Format("\t{0} {1} ;{2}",
                                               x.GetHeader(), x.discretisation, Environment.NewLine));
                }
            }
            str.Replace("({[[laplacianSchemes]]})", body3.ToString());

            var body4 = new StringBuilder(null);
            foreach (InterpolationScheme x in obj.interpolationSchemes)
            {

                bool dont_add_limit = (x.interpolation == InterpolationType.limitedLinear) ||
                                      (x.interpolation == InterpolationType.limitedCubic);
                body4.Append(String.Format("\t{0} {1}{2}",
                                           x.GetHeader(),
                                           (x.view == BoundView.Range && !dont_add_limit) ? "limited" : "",
                                           x.interpolation));
                if (x.view == BoundView.Name)
                {
                    body4.Append(String.Format("{0}{1} ", x.lower_limit.ToString(), x.upper_limit.ToString()));
                }
                else if (x.view == BoundView.Range)
                {
                    body4.Append(String.Format(" {0} {1}", x.lower_limit.ToString(culture),
                                               x.upper_limit.ToString(culture)));
                }
                if (x.psi != 0)
                {
                    body4.Append(String.Format(" {0}", x.psi.ToString(culture)));
                }
                if (x.flux != null)
                {
                    body4.Append(String.Format(" {0}", x.flux));
                }
                body4.Append(String.Format(";{0}", Environment.NewLine));
            }
            str.Replace("({[[interpolationSchemes]]})", body4.ToString());

            var body5 = new StringBuilder(null);
            foreach (SurfaceNormalGradientScheme x in obj.snGradSchemes)
            {
                body5.Append(String.Format("\t{0} {1} {2};{3}",
                    x.GetHeader(),
                    x.type,
                    (x.psi != 0) ? x.psi.ToString(culture): "",
                    Environment.NewLine));
            }
            str.Replace("({[[snGradSchemes]]})", body5.ToString());

            var body6 = new StringBuilder(null);
            foreach (FluxCalculation x in obj.fluxCalculation)
            {
                body6.Append(String.Format("\t{0} {1};{2}", x.flux, x.enable ? "yes" : "no",
                    Environment.NewLine));
            }
            str.Replace("({[[fluxRequired]]})", body6.ToString());

            WriteToFile(path, str.ToString());
        }
    }
}
