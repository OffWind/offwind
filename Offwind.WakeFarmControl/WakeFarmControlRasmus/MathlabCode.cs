using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public abstract class MatlabCode
    {
        protected class ArrayInitializer
        {
            public double[] this[double value1, double value2]
            {
                get
                {
                    return new double[] { value1, value2 };
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2]
            {
                get
                {
                    return (value1.Concat(value2, 1));
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3, ILArray<double> value4]
            {
                get
                {
                    return (value1.Concat(value2, 1).Concat(value3, 1).Concat(value4, 1));
                }
            }
        }

        protected static ArrayInitializer _ = new ArrayInitializer();

        protected static double pi = ILMath.pi;
        protected static ILNumerics.Misc.ILExpression end = ILMath.end;

        #region "Array size functions"
        protected static int[] size(ILArray<double> ilArray)
        {
            return ilArray.Size.ToIntArray();
        }

        protected static int length(ILArray<double> ilArray)
        {
            return ilArray.Size.Longest;
        }

        protected static int length(ILArray<int> ilArray)
        {
            return ilArray.Size.Longest;
        }
        #endregion

        #region "Rounding functions"
        protected static double ceil(double value)
        {
            return Math.Ceiling(value);
        }

        protected static int ceil_(double value)
        {
            return (int)(ceil(value));
        }

        protected static double floor(double value)
        {
            return Math.Floor(value);
        }

        protected static int floor_(double value)
        {
            return (int)(floor(value));
        }

        protected static double round(double value)
        {
            return (double)(ILMath.round(value));
        }
        #endregion

        protected static double abs(double value)
        {
            return Math.Abs(value);
        }

        protected static ILArray<double> abs(ILArray<double> ilArray)
        {
            return (ILMath.abs(ilArray));
        }

        #region "Trigonometrical functions"
        protected static double sin(double value)
        {
            return Math.Sin(value);
        }

        protected static double cos(double value)
        {
            return Math.Cos(value);
        }

        protected static double acos(double value)
        {
            return Math.Acos(value);
        }
        #endregion

        protected static double _p(double value, double power)
        {
            return Math.Pow(value, power);
        }

        protected static double sqrt(double value)
        {
            return Math.Sqrt(value);
        }

        #region "min & max functions"
        protected static int min(int value1, int value2)
        {
            return Math.Min(value1, value2);
        }

        protected static double min(double value1, double value2)
        {
            return Math.Min(value1, value2);
        }

        protected static double min(double[] array)
        {
            if (array.Length == 2)
            {
                return Math.Min(array[0], array[1]);
            }

            throw new NotImplementedException();
        }

        protected static ILArray<double> min(ILArray<double> ilArray)
        {
            return ILMath.min(ilArray);
        }

        protected static double min_(ILArray<double> ilArray)
        {
            return (double)(min(ilArray));
        }

        protected static ILArray<double> min(ILArray<double> ilArray, double value)
        {
            return ILMath.min(ilArray, value);
        }

        protected static ILArray<double> min(out ILArray<int> idx, ILArray<double> ilArray)
        {
            idx = ILMath.empty<int>();
            var result = ILMath.min(ilArray, idx);
            for (var i = 0; i < idx.Length; i++)
            {
                idx[i] = idx[i] + 1;
            }
            return result;
        }

        protected static int max(int value1, int value2)
        {
            return Math.Max(value1, value2);
        }

        protected static double max(double value1, double value2)
        {
            return Math.Max(value1, value2);
        }

        protected static double max(double[] array)
        {
            if (array.Length == 2)
            {
                return Math.Max(array[0], array[1]);
            }

            throw new NotImplementedException();
        }

        protected static double max(ILArray<double> ilArray)
        {
            return (double)(ILMath.max(ilArray));
        }

        protected static ILArray<double> max(ILArray<double> ilArray, double value)
        {
            return ILMath.max(ilArray, value);
        }

        protected static ILArray<double> max(out ILArray<int> idx, ILArray<double> ilArray)
        {
            idx = ILMath.empty<int>();
            var result = ILMath.max(ilArray, idx);
            for (var i = 0; i < idx.Length; i++)
            {
                idx[i] = idx[i] + 1;
            }
            return result;
        }
        #endregion

        #region "Matrix operations functions"
        protected static ILArray<double> transpose(ILArray<double> ilArray)
        {
            return ilArray.T;
        }

        protected static ILArray<double> sortrows(ILArray<double> ilArray, int sortedByColumnIndex)
        {
            ILArray<double> columnValues = ilArray[ILMath.full, sortedByColumnIndex - 1];
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
        #endregion

        #region "Matrix creation functions"
        protected static ILArray<double> _a(double start, double incrementation, double limit)
        {
            return ILMath.counter(start, incrementation, (limit - start) / incrementation + 1);
        }

        protected static ILArray<double> zeros(int size1, int size2)
        {
            return ILMath.zeros(size1, size2);
        }

        protected static ILArray<T> zeros<T>(int size1, int size2)
        {
            return ILMath.zeros<T>(size1, size2);
        }

        protected static ILArray<double> ones(int size1, int size2)
        {
            return ILMath.ones(size1, size2);
        }

        protected static ILArray<double> randn(int dim1, int dim2)
        {
            return ILMath.randn(dim1, dim2);
        }
        #endregion

        protected static double randn()
        {
            //get
            {
                return (double)(ILMath.randn());
            }
        }

        protected static int sign(double value)
        {
            return Math.Sign(value);
        }

        protected static ILArray<double> mod(double value1, double value2)
        {
            return (value1 % value2);//ILMath.mod(value1, value2);
        }

        protected static ILArray<double> sum(ILArray<double> ilArray)
        {
            return ILMath.sum(ilArray);
        }
    }
}
