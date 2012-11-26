using System;
using System.Text;

namespace Offwind.Products.OpenFoam.Models
{
    public sealed class DimensionedValue : IComparable<DimensionedValue>
    {
        public string Name { get; set; }
        public decimal ScalarValue { get; set; }

        public int Mass { get; set; }
        public int Length { get; set; }
        public int Time { get; set; }
        public int Temperature { get; set; }
        public int Quantity { get; set; }
        public int Current { get; set; }
        public int LuminousIntensity { get; set; }

        public int[] GetOpenFormatted()
        {
            return new[] { Mass, Length, Time, Temperature, Quantity, Current, LuminousIntensity };
        }

        public int CompareTo(DimensionedValue other)
        {
            var result = 0;
            if (Name != other.Name) result++;
            if (ScalarValue != other.ScalarValue) result++;
            if (Mass != other.Mass) result++;
            if (Length != other.Length) result++;
            if (Time != other.Time) result++;
            if (Temperature != other.Temperature) result++;
            if (Quantity != other.Quantity) result++;
            if (Current != other.Current) result++;
            if (LuminousIntensity != other.LuminousIntensity) result++;
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(" [");
            sb.AppendFormat("{0} {1} {2} {3} {4} {5} {6}"
                , Mass
                , Length
                , Time
                , Temperature
                , Quantity
                , Current
                , LuminousIntensity
                );
            sb.Append("] ");
            sb.Append(ScalarValue);
            return sb.ToString();
        }
    }
}
