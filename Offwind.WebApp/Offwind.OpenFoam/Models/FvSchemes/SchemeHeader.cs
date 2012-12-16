using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Offwind.Sowfa.System.FvSchemes
{
    public class SchemeHeader
    {
        public bool use_default { set; get; }
        public string scheme  { set; get; }
        public string function;

        public SchemeHeader()
        {
            use_default = true;
            scheme = null;
            function = null;
        }

        public void SetHeader( ref string value )
        {
            const string format = @"(laplacian|div|interpolate|grad|time)";
            use_default = true;
            if ( value != "default" )
            {
                use_default = false;
                var match = Regex.Match(value, format);
                if (match.Length > 0)
                {
                    scheme = String.Copy(match.Value);
                    function = value.Replace(scheme, "");
                }
            }
        }
        public string GetHeader()
        {
            if (use_default) return "default";
            return String.Concat(scheme, function);
        }
    }
}
