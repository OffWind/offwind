using System.Text.RegularExpressions;

namespace Offwind.Products.OpenFoam.Models
{
    public static class Validator
    {
        public static bool IsIdentifier(string txt)
        {
            if (txt == null) return false;
            var regex = new Regex(@"[a-z]+([a-z]|[0-9])*", RegexOptions.IgnoreCase);
            return regex.IsMatch(txt);
        }
    }
}
