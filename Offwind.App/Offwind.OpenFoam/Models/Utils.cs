using System.Text.RegularExpressions;

namespace Offwind.Products.OpenFoam.Models
{
    public static class Utils
    {
        public static string GetVtkSeries(string fileName)
        {
            var matches = Regex.Matches(fileName, @"([\w\s]+)_([0-9])+\.vtk", RegexOptions.IgnoreCase);
            return string.Format("{0}_..vtk", matches[0].Groups[1]);
        }
    }
}
