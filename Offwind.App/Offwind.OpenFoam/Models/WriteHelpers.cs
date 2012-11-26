using System.Collections.Generic;
using System.Text;

namespace Offwind.Products.OpenFoam.Models
{
    public static class WriteHelpers
    {
        public static string WriteArray<T>(this IEnumerable<T> input)
        {
            var b = new StringBuilder();
            b.Append("(");
            var c = 0;
            foreach (T v in input)
            {
                if (c++ > 0) b.Append(" ");
                b.AppendFormat("{0}", v);
            }
            b.Append(")");
            return b.ToString();
        }

        public static string WriteArrayOrNumber<T>(this T[] input)
        {
            if (input.Length == 1)
                return input[0].ToString();
            return input.WriteArray();
        }

        public static string WriteVector(this Vertice v)
        {
            return WriteArray(new[] { v.X, v.Y, v.Z });
        }
    }
}
