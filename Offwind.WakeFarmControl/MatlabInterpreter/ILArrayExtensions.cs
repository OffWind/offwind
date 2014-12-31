using System;
using System.Linq;
using ILNumerics;

namespace MatlabInterpreter
{
    public static class ILArrayExtensions
    {
        //public static T[] ToArray<T>(this ILArray<T> ilArray)
        //{
        //    if (ilArray.Size.ToIntArray().Length != 1)
        //    {
        //        throw new ArgumentException();
        //    }

        //    var array = new T[ilArray.Size[0]];
        //    for (int i = 0; i <= array.GetLength(0) - 1; i++)
        //    {
        //        array[i] = ilArray.GetValue(i);
        //    }

        //    return array;
        //}

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
        /// <summary>
        /// = (i)
        /// </summary>
        public static int _(this ILArray<int> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        /// <summary>
        /// (i) =
        /// </summary>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<int> ilArray, int index, char equalitySign, int value)
        {
            ilArray.SetValue(value, index - 1);
        }

        /// <summary>
        /// = (i)
        /// </summary>
        public static double _(this ILArray<double> ilArray, int index)
        {
            return ilArray.GetValue(index - 1);
        }

        /// <summary>
        /// (i) =
        /// </summary>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index, char equalitySign, double value)
        {
            ilArray.SetValue(value, index - 1);
        }

        /// <summary>
        /// (i) =
        /// </summary>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index, char equalitySign, ILArray<double> value)
        {
            ilArray.SetValue((double)value, index - 1);
        }
        #endregion

        #region "(end)"
        /// <summary>
        /// = (end)
        /// </summary>
        /// <param name="colon"></param>
        public static double _(this ILArray<double> ilArray, ILNumerics.Misc.ILExpression index)
        {
            return (double)(ilArray[index]);
        }
        #endregion

        #region "(i1,i2)"
        /// <summary>
        /// = (i1,i2)
        /// </summary>
        public static double _(this ILArray<double> ilArray, int index1, int index2)
        {
            return ilArray.GetValue(index1 - 1, index2 - 1);
        }

        /// <summary>
        /// (i1,i2) =
        /// </summary>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index1, int index2, char equalitySign, double value)
        {
            ilArray.SetValue(value, index1 - 1, index2 - 1);
        }
        #endregion

        /// <summary>
        /// = (i1, i2, ...)
        /// </summary>
        /// <param name="indexes">[ i1, i2, ... ]</param>
        public static double _(this ILArray<double> ilArray, ILArray<int> indexes)
        {
            int[] indexesArray = indexes.ToArray();
            for (var index = 0; index < indexesArray.GetLength(0); index++)
            {
                indexesArray[index]--;
            }
            return ilArray.GetValue(indexesArray);
        }

        #region "(:,i) & (i,:)"
        /// <summary>
        /// = (:,i)
        /// </summary>
        /// <param name="colon">':'</param>
        public static ILArray<double> _(this ILArray<double> ilArray, char colon, int index)
        {
            return ilArray[ILMath.full, index - 1];
        }

        /// <summary>
        /// (:,i) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, char colon, int index, char equalitySign, ILArray<double> value)
        {
            ilArray[ILMath.full, index - 1] = value;
        }

        /// <summary>
        /// (:,i) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, char colon, int index, char equalitySign, double value)
        {
            ilArray[ILMath.full, index - 1] = value;
        }

        /// <summary>
        /// = (i,:)
        /// </summary>
        /// <param name="colon">':'</param>
        public static ILArray<double> _(this ILArray<double> ilArray, int index, char colon)
        {
            return ilArray[index - 1, ILMath.full];
        }

        /// <summary>
        /// (i,:) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index, char colon, char equalitySign, ILArray<double> value)
        {
            ilArray[index - 1, ILMath.full] = value;
        }
        #endregion

        #region "(:,f:t) & (f:t,:)"
        /// <summary>
        /// = (:,f:t)
        /// </summary>
        /// <param name="colon">':'</param>
        public static ILArray<double> _(this ILArray<double> ilArray, char colon, int index1, int index2)
        {
            return ilArray[ILMath.full, ILMath.r(index1 - 1, index2 - 1)];
        }

        /// <summary>
        /// (:,f:t) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, char colon, int index1, int index2, char equalitySign, ILArray<double> value)
        {
            ilArray[ILMath.full, ILMath.r(index1 - 1, index2 - 1)] = value;
        }

        /// <summary>
        /// = (f:t,:)
        /// </summary>
        /// <param name="colon">':'</param>
        public static ILArray<double> _(this ILArray<double> ilArray, int index1, int index2, char colon)
        {
            return ilArray[ILMath.r(index1 - 1, index2 - 1), ILMath.full];
        }

        /// <summary>
        /// (f:t,:) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index1, int index2, char colon, char equalitySign, ILArray<double> value)
        {
            ilArray[ILMath.r(index1 - 1, index2 - 1), ILMath.full] = value;
        }
        #endregion

        #region "(:,f:end) & (f:end,:)"
        /// <summary>
        /// = (:,f:end)
        /// </summary>
        /// <param name="colon"></param>
        public static ILArray<double> _(this ILArray<double> ilArray, char colon, int index1, ILNumerics.Misc.ILExpression index2)
        {
            return ilArray[ILMath.full, ILMath.r(index1 - 1, index2)];
        }

        /// <summary>
        /// (:,f:end) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, char colon, int index1, ILNumerics.Misc.ILExpression index2, char equalitySign, ILArray<double> value)
        {
            ilArray[ILMath.full, ILMath.r(index1 - 1, index2)] = value;
        }

        /// <summary>
        /// = (f:end,:)
        /// </summary>
        /// <param name="colon">':'</param>
        public static ILArray<double> _(this ILArray<double> ilArray, int index1, ILNumerics.Misc.ILExpression index2, char colon)
        {
            return ilArray[ILMath.r(index1 - 1, index2), ILMath.full];
        }

        /// <summary>
        /// (f:end,:) =
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="equalitySign">'='</param>
        public static void _(this ILArray<double> ilArray, int index1, ILNumerics.Misc.ILExpression index2, ILArray<double> value, char colon, char equalitySign)
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
