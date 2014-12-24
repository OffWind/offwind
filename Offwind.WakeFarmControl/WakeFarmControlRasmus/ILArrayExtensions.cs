using System;
using System.Linq;
using ILNumerics;

namespace WakeFarmControlR
{
    public static class ILArrayExtensions
    {
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

        #region "(i)"
        public static int _get(this ILArray<int> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        public static void _set(this ILArray<int> ilArray, int index, int value)
        {
            ilArray.SetValue(value, index - 1);
        }

        public static double _get(this ILArray<double> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        public static void _set(this ILArray<double> ilArray, int index, double value)
        {
            ilArray.SetValue(value, index - 1);
        }
        #endregion

        #region "(i1,i2)"
        public static double _get(this ILArray<double> ilArray, int index1, int index2)
        {
            return ilArray.GetValue(index1 - 1, index2 - 1);
        }

        public static void _set(this ILArray<double> ilArray, int index1, int index2, double value)
        {
            ilArray.SetValue(value, index1 - 1, index2 - 1);
        }
        #endregion

        public static double _get(this ILArray<double> ilArray, ILArray<int> indexes)
        {
            int[] indexesArray = indexes.ToArray();
            for (var index = 0; index < indexesArray.GetLength(0); index++)
            {
                indexesArray[index]--;
            }
            return ilArray.GetValue(indexesArray);
        }

        #region "(:,i) & (i,:)"
        public static ILArray<double> _get(this ILArray<double> ilArray, char c, int index)
        {
            return ilArray[ILMath.full, index - 1];
        }

        public static void _set(this ILArray<double> ilArray, char c, int index, ILArray<double> value)
        {
            ilArray[ILMath.full, index - 1] = value;
        }

        public static void _set(this ILArray<double> ilArray, char c, int index, double value)
        {
            ilArray[ILMath.full, index - 1] = value;
        }

        public static ILArray<double> _get(this ILArray<double> ilArray, int index, char c)
        {
            return ilArray[index - 1, ILMath.full];
        }

        public static void _set(this ILArray<double> ilArray, int index, char c, ILArray<double> value)
        {
            ilArray[index - 1, ILMath.full] = value;
        }
        #endregion

        #region "(:,f:t) & (f:t,:)"
        public static ILArray<double> _get(this ILArray<double> ilArray, char c, int index1, int index2)
        {
            return ilArray[ILMath.full, ILMath.r(index1 - 1, index2 - 1)];
        }

        public static void _set(this ILArray<double> ilArray, char c, int index1, int index2, ILArray<double> value)
        {
            ilArray[ILMath.full, ILMath.r(index1 - 1, index2 - 1)] = value;
        }

        public static ILArray<double> _get(this ILArray<double> ilArray, int index1, int index2, char c)
        {
            return ilArray[ILMath.r(index1 - 1, index2 - 1), ILMath.full];
        }

        public static void _set(this ILArray<double> ilArray, int index1, int index2, char c, ILArray<double> value)
        {
            ilArray[ILMath.r(index1 - 1, index2 - 1), ILMath.full] = value;
        }
        #endregion

        #region "(:,f:end) & (f:end,:)"
        public static ILArray<double> _get(this ILArray<double> ilArray, char c, int index1, ILNumerics.Misc.ILExpression index2)
        {
            return ilArray[ILMath.full, ILMath.r(index1 - 1, index2)];
        }

        public static void _set(this ILArray<double> ilArray, char c, int index1, ILNumerics.Misc.ILExpression index2, ILArray<double> value)
        {
            ilArray[ILMath.full, ILMath.r(index1 - 1, index2)] = value;
        }

        public static ILArray<double> _get(this ILArray<double> ilArray, int index1, ILNumerics.Misc.ILExpression index2, char c)
        {
            return ilArray[ILMath.r(index1 - 1, index2), ILMath.full];
        }

        public static void _set(this ILArray<double> ilArray, int index1, ILNumerics.Misc.ILExpression index2, ILArray<double> value, char c)
        {
            ilArray[ILMath.r(index1 - 1, index2), ILMath.full] = value;
        }
        #endregion


        //public static ILArray<double> GetRows(this ILArray<double> ilArray, int fromRowIndex, int toRowIndex)
        //{
        //    return ((ILArray<double>)(ilArray[ILMath.r(fromRowIndex - 1, toRowIndex - 1), ILMath.full]));
        //}

        //public static void SetRows(this ILArray<double> ilArray, int fromRowIndex, int toRowIndex, ILArray<double> value)
        //{
        //    if (ilArray == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    if (value == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    if (toRowIndex > ilArray.Size[0])
        //    {
        //        throw new ArgumentException();
        //    }

        //    if (value.Size[0] != (toRowIndex - fromRowIndex + 1))
        //    {
        //        throw new ArgumentException();
        //    }

        //    if (value.Size[1] != ilArray.Size[1])
        //    {
        //        throw new ArgumentException();
        //    }

        //    for (int index = 0; index <= toRowIndex - fromRowIndex; index++)
        //    {
        //        ilArray[fromRowIndex + index - 1, ILMath.full] = value[index, ILMath.full];
        //    }
        //}
    
    }

}
