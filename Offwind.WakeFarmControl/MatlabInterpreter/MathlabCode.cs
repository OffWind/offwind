using System;
using ILNumerics;

namespace MatlabInterpreter
{
    public abstract class MatlabCode
    {
        protected class ArrayInitializer
        {
            public ILArray<double> this[char space]
            {
                get
                {
                    if (!(space == ' '))
                    {
                        throw new ArgumentException();
                    }
                    return ((ILArray<double>)(new double[] { })).T;
                }
            }

            public string this[string str]
            {
                get
                {
                    return str;
                }
            }

            public string this[string str1, string str2]
            {
                get
                {
                    return (str1 + str2);
                }
            }

            public string this[string str1, string str2, string str3, string str4]
            {
                get
                {
                    var stringBuilder = new System.Text.StringBuilder(str1);
                    stringBuilder.Append(str2);
                    stringBuilder.Append(str3);
                    stringBuilder.Append(str4);

                    return stringBuilder.ToString();
                }
            }

            public double[] this[double value1, double value2]
            {
                get
                {
                    return new double[] { value1, value2 };
                }
            }

            public double[] this[double value1, double value2, double value3]
            {
                get
                {
                    return new double[] { value1, value2, value3 };
                }
            }

            public double[] this[double value1, double value2, double value3, double value4, double value5]
            {
                get
                {
                    return new double[] { value1, value2, value3, value4, value5 };
                }
            }

            public ILArray<double> this[double value1, char semicolon, double value2]
            {
                get
                {
                    return ((ILArray<double>)(new double[] { value1, value2 })).T;
                }
            }

            public ILArray<double> this[double value11, double value12, char semicolon, double value21, double value22]
            {
                get
                {
                    return (ILArray<double>)(new double[,] { { value11, value21 }, { value12, value22 } });
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2]
            {
                get
                {
                    return (value1.Concat(value2, 1));
                }
            }

            public ILArray<double> this[ILArray<double> value1, char semicolon, ILArray<double> value2]
            {
                get
                {
                    return (value1.Concat(value2, 0));
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3]
            {
                get
                {
                    return (value1.Concat(value2, 1).Concat(value3, 1));
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3, ILArray<double> value4]
            {
                get
                {
                    return (value1.Concat(value2, 1).Concat(value3, 1).Concat(value4, 1));
                }
            }

            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3, ILArray<double> value4, ILArray<double> value5]
            {
                get
                {
                    return (value1.Concat(value2, 1).Concat(value3, 1).Concat(value4, 1).Concat(value5, 1));
                }
            }
        }

        protected static ArrayInitializer _ = new ArrayInitializer();

        protected static double pi = ILMath.pi;
        protected static ILNumerics.Misc.ILExpression end = ILMath.end;

        protected static void error(string message)
        {
            throw new ApplicationException(message);
        }

        protected static void warning(string message, ref System.Collections.Generic.List<string> warningMessage)
        {
            warningMessage.Add(message);
        }

        protected static void warning(string message)
        {
            System.Collections.Generic.List<string> warningMessage = new System.Collections.Generic.List<string>();
            warning(message, ref warningMessage);
        }

        protected static string num2str(double value)
        {
            return value.ToString();
        }

        #region "Array size functions"
        protected static int[] size(ILArray<double> ilArray)
        {
            return ilArray.Size.ToIntArray();
        }

        protected static int size(ILArray<double> ilArray, int dim)
        {
            return ilArray.Size[dim - 1];
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

        protected static double exp(double power)
        {
            return Math.Exp(power);
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

        protected static ILArray<int> min(ILArray<int> ilArray)
        {
            return ILMath.min(ilArray);
        }

        protected static int min_(ILArray<int> ilArray)
        {
            return (int)(min(ilArray));
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

        protected static ILArray<double> min(out int index, ILArray<double> ilArray)
        {
            ILArray<int> idx;
            var result = min(out idx, ilArray);
            index = (int)idx;

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

        protected static ILLogical all(ILLogical Logical)
        {
            return (ILLogical)(ILMath.all(Logical));
        }

        protected static ILArray<int> find(ILLogical Logical)
        {
            ILArray<int> idx = ILMath.empty<int>();
            idx = ILMath.find(Logical);
            for (var i = 0; i < idx.Length; i++)
            {
                idx[i] = idx[i] + 1;
            }
            return idx;
        }

        #region "Matrix operations functions"
        protected static ILArray<double> _m(ILArray<double> ilArray1, ILArray<double> ilArray2)
        {
            return ILMath.multiply(ilArray1, ilArray2);
        }

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

        protected static ILArray<double> _a(double start, double limit)
        {
            return _a(start, 1, limit);
        }

        protected static ILArray<double> zeros(int size1, int size2)
        {
            return ILMath.zeros(size1, size2);
        }

        protected static ILArray<T> zeros<T>(int size1, int size2)
        {
            return ILMath.zeros<T>(size1, size2);
        }

        protected static ILArray<int> zeros_(int size1, int size2)
        {
            return zeros<int>(size1, size2);
        }

        protected static ILArray<double> ones(int size1, int size2)
        {
            return ILMath.ones(size1, size2);
        }

        protected static ILArray<double> linspace(double start, double end, double length)
        {
            return ILMath.linspace(start, end, length);
        }

        protected static ILArray<double> randn(int dim1, int dim2)
        {
            return ILMath.randn(dim1, dim2);
        }
        #endregion

        protected static bool isempty<T>(ILArray<T> ilArray)
        {
            return ILMath.isempty(ilArray);
        }

        protected static bool isempty(string str)
        {
            return (str == string.Empty);
        }

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

        protected static double sum_(ILArray<double> ilArray)
        {
            return (double)(sum(ilArray));
        }

        protected static bool strncmpi(string str1, string str2, int n)
        {
            return (string.Compare(str1, 0, str2, 0, n, true) == 0);
        }
    }
}
