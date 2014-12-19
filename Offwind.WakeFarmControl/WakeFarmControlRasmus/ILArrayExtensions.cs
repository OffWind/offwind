using System;
using System.Linq;
using ILNumerics;

namespace WakeFarmControlR
{
    public static class ILArrayExtensions
    {
        public static int length(this ILArray<double> ilArray)
        {
            return ilArray.Size.Longest;
        }

        public static int length(this ILArray<int> ilArray)
        {
            return ilArray.Size.Longest;
        }

        public static double[][] ToDoubleArray(this ILArray<double> ilArray)
        {
            if (ilArray.Size.ToIntArray().Length != 2)
            {
                throw new ArgumentException();
            }

            var doubleArray = new double[ilArray.Size[0]][];
            for (int i = 0; i <= doubleArray.GetLength(0) - 1; i++)
            {
                doubleArray[i] = new double[ilArray.Size[1]];
                for (int j = 0; j <= doubleArray[i].GetLength(0) - 1; j++)
                {
                    doubleArray[i][j] = ilArray.GetValue(i, j);
                }
            }

            return doubleArray;
        }

        public static int Get(this ILArray<int> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        public static double Get(this ILArray<double> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        public static double Get(this ILArray<double> ilArray, int index1, int index2)
        {
            return ilArray.GetValue(index1 - 1, index2 - 1);
        }

        public static void Set(this ILArray<double> ilArray, int index, double value)
        {
            ilArray.SetValue(value, index - 1);
        }

        public static void Set(this ILArray<double> ilArray, int index1, int index2, double value)
        {
            ilArray.SetValue(value, index1 - 1, index2 - 1);
        }

        public static void SetRows(this ILArray<double> ilArray, ILArray<double> value, int fromRow, int toRow)
        {
            if (ilArray == null)
            {
                throw new ArgumentException();
            }

            if (value == null)
            {
                throw new ArgumentException();
            }

            if (toRow > ilArray.Size[0] - 1)
            {
                throw new ArgumentException();
            }

            if (value.Size[0] != (toRow - fromRow + 1))
            {
                throw new ArgumentException();
            }

            if (value.Size[1] != ilArray.Size[1])
            {
                throw new ArgumentException();
            }

            for (int index = 0; index <= toRow - fromRow; index++)
            {
                ilArray[fromRow + index, ILMath.full] = value[index, ILMath.full];
            }
        }

        public static ILArray<double> sortrows(this ILArray<double> ilArray, int column)
        {
            ILArray<double> columnValues = ilArray[ILMath.full, column];
            ILArray<int> rowsIndices = ILMath.empty<int>();
            ILArray<double> sortedColumnValues = ILMath.sort(columnValues, rowsIndices);

            //ILArray<double> sortedArray = ilArray[ILMath.full, 0][rowsIndices];
            ILArray<double> sortedArray = ILMath.empty(ilArray.Size[0], 0);
            for (var columnIndex = 0; columnIndex < ilArray.Size[1]; columnIndex++)
            {
                sortedArray = sortedArray.Concat(ilArray[ILMath.full, columnIndex][rowsIndices], 1);
            }

            return sortedArray;
        }
    }

}
