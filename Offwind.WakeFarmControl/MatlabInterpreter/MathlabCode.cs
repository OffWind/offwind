using System;
using ILNumerics;

namespace MatlabInterpreter
{
    public abstract class MatlabCode
    {
        protected class ArrayInitializer
        {
            /// <summary>
            /// [ ]
            /// </summary>
            /// <param name="space">' '</param>
            public ILArray<double> this[char space]
            {
                get
                {
                    if (!(space == ' '))
                    {
                        throw new ArgumentException();
                    }
                    return ILArray<double>.empty();
                }
            }

        //    public string this[string str]
        //    {
        //        get
        //        {
        //            return str;
        //        }
        //    }

            /// <summary>
            /// [  '', '' ]
            /// </summary>
            public string this[string str1, string str2]
            {
                get
                {
                    return (str1 + str2);
                }
            }

        //    public string this[string str1, string str2, string str3, string str4]
        //    {
        //        get
        //        {
        //            var stringBuilder = new System.Text.StringBuilder(str1);
        //            stringBuilder.Append(str2);
        //            stringBuilder.Append(str3);
        //            stringBuilder.Append(str4);

        //            return stringBuilder.ToString();
        //        }
        //    }

            /// <summary>
            /// [ v1, v2 ]
            /// </summary>
            public ILArray<double> this[double value1, double value2]
            {
                get
                {
                    //return ((ILArray<double>)(new double[] { value1, value2 })).T;
                    return ((ILArray<double>)(new double[,] { { value1 }, { value2 } }));
                }
            }

            /// <summary>
            /// [ v1, v2, v3 ]
            /// </summary>
            public ILArray<double> this[double value1, double value2, double value3]
            {
                get
                {
                    return ((ILArray<double>)(new double[,] { { value1 }, { value2 }, { value3 } }));
                }
            }

        //    public ILArray<double> this[double value1, double value2, double value3, double value4, double value5]
        //    {
        //        get
        //        {
        //            return ((ILArray<double>)(new double[] { value1, value2, value3, value4, value5 })).T;
        //        }
        //    }

            /// <summary>
            /// [ v1 ; v2 ]
            /// </summary>
            /// <param name="semicolon">';'</param>
            public ILArray<double> this[double value1, char semicolon, double value2]
            {
                get
                {
                    return ((ILArray<double>)(new double[,] { { value1, value2 } }));
                }
            }

            /// <summary>
            /// [ v1, v2 ; v3, v4 ]
            /// </summary>
            /// <param name="semicolon">';'</param>
            public ILArray<double> this[double value11, double value12, char semicolon, double value21, double value22]
            {
                get
                {
                    return (ILArray<double>)(new double[,] { { value11, value21 }, { value12, value22 } });
                }
            }

            /// <summary>
            /// [ a1, a2 ]
            /// </summary>
            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2]
            {
                get
                {
                    return (value1.Concat(value2, 1));
                    //int dim0 = value1.Size[0];
                    //int offset2 = value1.Size[1];
                    //int dim1 = offset2 + value2.Size[1];
                    //ILArray<double> result = ILMath.zeros(dim0, dim1);
                    //result[ILMath.full, ILMath.r(0, offset2 - 1)] = value1;
                    //result[ILMath.full, ILMath.r(offset2, ILMath.end)] = value2;
                    //return result;
                }
            }

            /// <summary>
            /// [ a1; a2 ]
            /// </summary>
            /// <param name="semicolon">';'</param>
            public ILArray<double> this[ILArray<double> value1, char semicolon, ILArray<double> value2]
            {
                get
                {
                    return (value1.Concat(value2, 0));
                    //int offset2 = value1.Size[0];
                    //int dim0 = offset2 + value2.Size[0];
                    //int dim1 = value1.Size[1];
                    //ILArray<double> result = ILMath.zeros(dim0, dim1);
                    //result[ILMath.r(0, offset2 - 1), ILMath.full] = value1;
                    //result[ILMath.r(offset2, ILMath.end), ILMath.full] = value2;
                    //return result;
                }
            }

        //    public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3]
        //    {
        //        get
        //        {
        //            return (value1.Concat(value2, 1).Concat(value3, 1));
        //            //int dim0 = value1.Size[0];
        //            //int offset2 = value1.Size[1];
        //            //int offset3 = offset2 + value2.Size[1];
        //            //int dim1 = offset3 + value3.Size[1];
        //            //ILArray<double> result = ILMath.zeros(dim0, dim1);
        //            //result[ILMath.full, ILMath.r(0, offset2 - 1)] = value1;
        //            //result[ILMath.full, ILMath.r(offset2, offset3 - 1)] = value2;
        //            //result[ILMath.full, ILMath.r(offset3, ILMath.end)] = value3;
        //            //return result;
        //        }
        //    }

            /// <summary>
            /// [ a1, a2, a3, a4 ]
            /// </summary>
            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3, ILArray<double> value4]
            {
                get
                {
                    return (value1.Concat(value2, 1).Concat(value3, 1).Concat(value4, 1));
                    //int dim0 = value1.Size[0];
                    //int offset2 = value1.Size[1];
                    //int offset3 = offset2 + value2.Size[1];
                    //int offset4 = offset3 + value3.Size[1];
                    //int dim1 = offset4 + value3.Size[1];
                    //ILArray<double> result = ILMath.zeros(dim0, dim1);
                    //result[ILMath.full, ILMath.r(0, offset2 - 1)] = value1;
                    //result[ILMath.full, ILMath.r(offset2, offset3 - 1)] = value2;
                    //result[ILMath.full, ILMath.r(offset3, offset4 - 1)] = value3;
                    //result[ILMath.full, ILMath.r(offset4, ILMath.end)] = value4;
                    //return result;
                }
            }

            /// <summary>
            /// [ a1, a2, a3, a4, a5 ]
            /// </summary>
            public ILArray<double> this[ILArray<double> value1, ILArray<double> value2, ILArray<double> value3, ILArray<double> value4, ILArray<double> value5]
            {
                get
                {
                    //return (value1.Concat(value2, 1).Concat(value3, 1).Concat(value4, 1).Concat(value5, 1));
                    return this[this[value1, value2, value3, value4], value5];
                    //int dim0 = value1.Size[0];
                    //int offset2 = value1.Size[1];
                    //int offset3 = offset2 + value2.Size[1];
                    //int offset4 = offset3 + value3.Size[1];
                    //int offset5 = offset4 + value4.Size[1];
                    //int dim1 = offset5 + value3.Size[1];
                    //ILArray<double> result = ILMath.zeros(dim0, dim1);
                    //result[ILMath.full, ILMath.r(0, offset2 - 1)] = value1;
                    //result[ILMath.full, ILMath.r(offset2, offset3 - 1)] = value2;
                    //result[ILMath.full, ILMath.r(offset3, offset4 - 1)] = value3;
                    //result[ILMath.full, ILMath.r(offset4, offset5 - 1)] = value4;
                    //result[ILMath.full, ILMath.r(offset5, ILMath.end)] = value5;
                    //return result;
                }
            }
        }

        protected static ArrayInitializer __ = new ArrayInitializer();

        protected static double pi = ILMath.pi;
        protected static ILNumerics.Misc.ILExpression end = ILMath.end;

        protected static double _double;
        protected static ILArray<double> _ILArray_double;

        private static bool IsScalar<T>(ILArray<T> ilArray)
        {
            return ilArray.IsScalar;
        }

        #region "Indexes functions"
        /// <summary>
        /// (..., i, ...)
        /// </summary>
        protected static int _(int index)
        {
            return (index - 1);
        }

        /// <summary>
        /// (..., :, ...)
        /// </summary>
        /// <param name="colon">':'</param>
        protected static ILBaseArray _(char colon)
        {
            return ILMath.full;
        }

        /// <summary>
        /// (..., i1:i2, ...)
        /// </summary>
        /// <param name="colon">':'</param>
        protected static ILBaseArray _(int index1, char colon, int index2)
        {
            return ILMath.r(index1 - 1, index2 - 1);
        }

        /// <summary>
        /// (..., indexesArray, ...)
        /// </summary>
        protected static ILArray<int> _(ILArray<int> indexes)
        {
            return indexes - 1;
        }

        /// <summary>
        /// (..., end, ...)
        /// </summary>
        /// <param name="ilExpression">~ end</param>
        protected static ILNumerics.Misc.ILExpression _(ILNumerics.Misc.ILExpression ilExpression)
        {
            return ilExpression;
        }

        /// <summary>
        /// (..., i:end, ...)
        /// </summary>
        /// <param name="colon">':'</param>
        /// <param name="ilExpression">~ end</param>
        protected static ILBaseArray _(int index, char colon, ILNumerics.Misc.ILExpression ilExpression)
        {
            return ILMath.r(index - 1, ilExpression);
        }
        #endregion


        //protected static void error(string message)
        //{
        //    throw new ApplicationException(message);
        //}

        private static void warning(string message, ref System.Collections.Generic.List<string> warningMessage)
        {
            warningMessage.Add(message);
        }

        protected static void warning(string message)
        {
            System.Collections.Generic.List<string> warningMessage = new System.Collections.Generic.List<string>();
            MatlabCode.warning(message, ref warningMessage);
        }

        #region "Array size functions"
        //protected static int[] size(ILArray<double> ilArray)
        //{
        //    return ilArray.Size.ToIntArray();
        //}

        protected static void size(out int size1, out int size2, ILArray<double> ilArray)
        {
            ILSize size = ilArray.Size;
            size1 = size[0];
            size2 = size[1];
        }

        protected static int size(ILArray<double> ilArray, int dim)
        {
            return ilArray.Size[dim - 1];
        }

        private static int length<T>(ILArray<T> ilArray)
        {
            return ilArray.Size.Longest;
        }

        protected static int length(ILArray<int> ilArray)
        {
            return MatlabCode.length<int>(ilArray);
        }

        protected static int length(ILArray<double> ilArray)
        {
            return MatlabCode.length<double>(ilArray);
        }
        #endregion

        #region "Rounding functions"
        protected static double ceil(double value)
        {
            return Math.Ceiling(value);
        }

        protected static int ceil_(double value)
        {
            var result = MatlabCode.ceil(value);
            return (int)result;
        }

        private static double floor(double value)
        {
            return Math.Floor(value);
        }

        protected static int floor_(double value)
        {
            var result = MatlabCode.floor(value);
            return (int)result;
        }

        protected static double round(double value)
        {
            return Math.Round(value);
        }

        protected static int round_(double value)
        {
            var result = MatlabCode.round(value);
            return (int)result;
        }
        #endregion

        protected static double abs(double value)
        {
            return Math.Abs(value);
        }

        protected static ILArray<double> abs(ILArray<double> ilArray)
        {
            return ILMath.abs(ilArray);
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

        protected static int _p_(int value, int power)
        {
            if (power == 2)
            {
                return value * value;
            }
            else if (power == 3)
            {
                return value * value * power;
            }
            throw new ApplicationException();
        }

        protected static double sqrt(double value)
        {
            return Math.Sqrt(value);
        }

        protected static ILArray<double> sqrt(ILArray<double> value)
        {
            return ILMath.sqrt(value);
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

        //protected static double min(double value1, double value2)
        //{
        //    return Math.Min(value1, value2);
        //}

        //protected static double min(double[] array)
        //{
        //    if (array.Length == 2)
        //    {
        //        return Math.Min(array[0], array[1]);
        //    }

        //    throw new NotImplementedException();
        //}

        private static ILArray<int> min(ILArray<int> ilArray)
        {
            return ILMath.min(ilArray);
        }

        protected static int min_(ILArray<int> ilArray)
        {
            ILArray<int> result = MatlabCode.min(ilArray);
            return result._Scalar();
        }

        private static ILArray<double> min(ILArray<double> ilArray)
        {
            return ILMath.min(ilArray);
        }

        protected static double min_(ILArray<double> ilArray)
        {
            ILArray<double> result = MatlabCode.min(ilArray);
            return result._Scalar();
        }

        protected static ILArray<double> min(ILArray<double> ilArray, double value)
        {
            return ILMath.min(ilArray, value);
        }

        private static ILArray<double> min(out ILArray<int> idx, ILArray<double> ilArray)
        {
            idx = ILMath.empty<int>();
            ILArray<double> result = ILMath.min(ilArray, idx);
            idx += 1;
            return result;
        }

        protected static double min(out int index, ILArray<double> ilArray)
        {
            ILArray<int> idx;
            ILArray<double> result = MatlabCode.min(out idx, ilArray);
            index = idx._Scalar();

            return result._Scalar();
        }

        protected static int max(int value1, int value2)
        {
            return Math.Max(value1, value2);
        }

        protected static double max(double value1, double value2)
        {
            return Math.Max(value1, value2);
        }

        //protected static double max(double[] array)
        //{
        //    if (array.Length == 2)
        //    {
        //        return Math.Max(array[0], array[1]);
        //    }

        //    throw new NotImplementedException();
        //}

        private static ILArray<int> max(ILArray<int> ilArray)
        {
            return ILMath.max(ilArray);
        }

        protected static int max_(ILArray<int> ilArray)
        {
            ILArray<int> result = MatlabCode.max(ilArray);
            return result._Scalar();
        }

        private static ILArray<double> max(ILArray<double> ilArray)
        {
            return ILMath.max(ilArray);
        }

        protected static double max_(ILArray<double> ilArray)
        {
            ILArray<double> result = MatlabCode.max(ilArray);
            return result._Scalar();
        }

        protected static ILArray<double> max(ILArray<double> ilArray, double value)
        {
            return ILMath.max(ilArray, value);
        }

        protected static ILArray<double> max(out ILArray<int> idx, ILArray<double> ilArray)
        {
            idx = ILMath.empty<int>();
            ILArray<double> result = ILMath.max(ilArray, idx);
            idx += 1;
            return result;
        }
        #endregion

        #region "Matrix operations functions"
        //protected static bool isempty<T>(ILArray<T> ilArray)
        //{
        //    return ILMath.isempty(ilArray);
        //}

        protected static bool isempty(ILArray<double> ilArray)
        {
            return ILMath.isempty(ilArray);
        }

        protected static ILArray<double> _dbl(ILArray<int> ilArray)
        {
            return ILMath.todouble(ilArray);
        }

        protected static ILArray<int> _int(ILArray<double> ilArray)
        {
            return ILMath.toint32(ilArray);
        }

        protected static ILLogical all(ILLogical ilLogical)
        {
            return ILMath.all(ilLogical);
        }

        protected static ILArray<int> find(ILLogical Logical)
        {
            ILArray<int> idx = ILMath.empty<int>();
            idx = ILMath.find(Logical);
            idx += 1;
            return idx;
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="star">'*'</param>
        protected static ILArray<double> _m(ILArray<double> ilArray1, char star, ILArray<double> ilArray2)
        {
            return ILMath.multiply(ilArray1, ilArray2);
        }

        /// <summary>
        /// Matrix division (matrix equation A * X = B solving)
        /// </summary>
        /// <param name="star">'\'</param>
        protected static ILArray<double> _s(ILArray<double> ilArray1, char backslash, ILArray<double> ilArray2)
        {
            return ILMath.linsolve(ilArray1, ilArray2);
        }

        protected static ILArray<double> transpose(ILArray<double> ilArray)
        {
            return ilArray.T;
        }

        protected static ILArray<double> inv(ILArray<double> ilArray)
        {
            ILArray<double> ilArrayInverted = ILMath.empty();
            ILMath.invert(ilArray, ilArrayInverted);
            return ilArrayInverted;
        }

        protected static ILArray<double> reshape(ILArray<double> ilArray, int dim1, int dim2)
        {
            return ILMath.reshape(ilArray, dim1, dim2);
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
        protected static ILArray<int> _c_(int start, int incrementation, int limit)
        {
            return ILMath.counter<int>(start, incrementation, 1, (limit - start) / incrementation + 1);
        }

        protected static ILArray<int> _c_(int start, int limit)
        {
            return MatlabCode._c_(start, 1, limit);
        }

        protected static ILArray<double> _c(double start, double incrementation, double limit)
        {
            return ILMath.counter(start, incrementation, 1, (limit - start) / incrementation + 1);
        }

        protected static ILArray<double> _c(double start, double limit)
        {
            return MatlabCode._c(start, 1, limit);
        }

        private static ILArray<T> zeros<T>(int size1, int size2)
        {
            return ILMath.zeros<T>(size1, size2);
        }

        protected static ILArray<double> zeros(int size1, int size2)
        {
            return MatlabCode.zeros<double>(size1, size2);
        }

        protected static ILArray<int> zeros_(int size1, int size2)
        {
            return MatlabCode.zeros<int>(size1, size2);
        }

        protected static ILArray<double> zeros(int size1, int size2, int size3)
        {
            return ILMath.zeros<double>(size1, size2, size3);
        }

        protected static ILArray<double> ones(int size1, int size2)
        {
            return ILMath.ones(size1, size2);
        }

        protected static ILArray<double> eye(int size)
        {
            return ILMath.eye(size, size);
        }

        protected static ILArray<double> nan(int size1, int size2)
        {
            return double.NaN * MatlabCode.ones(size1, size2);
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

        //protected static bool isempty(string str)
        //{
        //    return (str == string.Empty);
        //}

        protected static double randn()
        {
            //get
            {
                return (double)(ILMath.randn());
            }
        }

        #region "Arithmetic functions"
        protected static int sign(double value)
        {
            return Math.Sign(value);
        }

        protected static double mod(double value1, double value2)
        {
            return (value1 % value2);//ILMath.mod(value1, value2);
        }
        #endregion

        #region "Aggregation functions"
        protected static ILArray<double> mean(ILArray<double> ilArray)
        {
            return ILMath.mean(ilArray);
        }

        protected static ILArray<double> sum(ILArray<double> ilArray)
        {
            return ILMath.sum(ilArray);
        }

        protected static double sum_(ILArray<double> ilArray)
        {
            ILArray<double> result = sum(ilArray);
            return result._Scalar();
        }
        #endregion

        #region "String functions"
        protected static bool strncmpi(string str1, string str2, int n)
        {
            return (string.Compare(str1, 0, str2, 0, n, true) == 0);
        }

        protected static string num2str(double value)
        {
            return value.ToString();
        }
        #endregion
    }
}
