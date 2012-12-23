using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Offwind.Sowfa.System.FvSchemes
{
    public class SchemeHeader
    {
        public bool isDefault { set; get; }
        public string scheme  { set; get; }
        public string function { set; get; }
        public decimal psi { set; get; }


        public SchemeHeader()
        {
            isDefault = true;
            scheme = null;
            function = null;
            psi = 0;
        }

        public void SetHeader( ref string value )
        {
            const string format = @"(laplacian|div|interpolate|grad|time)";
            isDefault = true;
            if ( value != "default" )
            {
                isDefault = false;
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
            if (isDefault) return "default";
            return String.Concat(scheme, function);
        }
        public string GetFunction()
        {
            return (isDefault) ? "default" : function;
        }
    }
}
