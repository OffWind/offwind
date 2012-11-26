using System.Text;

namespace Offwind.Products.OpenFoam.Models
{
    public sealed class Dimensions
    {
        public decimal Mass { get; set; }
        public decimal Length { get; set; }
        public decimal Time { get; set; }
        public decimal Temperature { get; set; }
        public decimal Quantity { get; set; }
        public decimal Current { get; set; }
        public decimal LuminousIntensity { get; set; }

        public decimal[] GetOpenFormatted()
        {
            return new[] { Mass, Length, Time, Temperature, Quantity, Current, LuminousIntensity };
        }

        public string Formatted()
        {
            var sb = new StringBuilder();
            FormatPart(sb, "kg", Mass);
            FormatPart(sb, "m", Length);
            FormatPart(sb, "s", Time);
            FormatPart(sb, "K", Temperature);
            FormatPart(sb, "kgmol", Quantity);
            FormatPart(sb, "A", Current);
            FormatPart(sb, "cd", LuminousIntensity);
            if (sb.Length == 0) return "...";
            return sb.ToString();
        }

        private void FormatPart(StringBuilder sb, string unit, decimal val)
        {
            if (val == 0) return;
            if (sb.Length > 0) sb.Append(" ");
            sb.AppendFormat("{0}^{1}", unit, val);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.AppendFormat("{0} {1} {2} {3} {4} {5} {6}"
                , Mass
                , Length
                , Time
                , Temperature
                , Quantity
                , Current
                , LuminousIntensity
                );
            sb.Append("]");
            return sb.ToString();
        }
    }
}