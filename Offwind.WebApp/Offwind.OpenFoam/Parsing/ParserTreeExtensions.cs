using System;
using System.Collections.Generic;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Products.OpenFoam.Parsing
{
    public static class ParserTreeExtensions
    {
        public static IEnumerable<ParseTreeNode> FindDictEntries(this ParseTreeNode dictionaryContentNode, string name)
        {
            foreach (ParseTreeNode node in dictionaryContentNode.ChildNodes)
            {
                if (node.ChildNodes.Count == 0) continue;
                if (node.ChildNodes[0].ChildNodes.Count == 0) continue;
                if (name == null || node.ChildNodes[0].ChildNodes[0].Token.Text == name)
                {
                    yield return node.ChildNodes[0];
                }
            }
        }

        public static ParseTreeNode GetDictContent(this ParseTreeNode dictionaryNode)
        {
            return dictionaryNode.ChildNodes[2];
        }

        public static ParseTreeNode GetDictContentHead(this ParseTreeNode node)
        {
            return node.ChildNodes[0].ChildNodes[0];
        }

        public static List<T> DictionaryWalk<T>(this ParseTreeNode dictionary, Func<string[], T> callback)
        {
            var ret = new List<T>();

            foreach (var entry in dictionary.ChildNodes)
            {
                var array = entry.GetDictContentHead();
                var y = array.GetArrayOfString();
                if (y != null)
                {
                    ret.Add(callback(y));
                }
            }
            return ret;
        }

        public static string GetEntryIdentifier(this ParseTreeNode entryNode)
        {
            if (entryNode.ChildNodes.Count == 0) return null;
            if (entryNode.ChildNodes[0].Token == null) return null;
            return entryNode.ChildNodes[0].Token.Text;
        }

        /// <summary>
        /// Bev - Basic Entry Value
        /// </summary>
        /// <param name="entryNode"></param>
        /// <returns></returns>
        public static string GetBasicValString(this ParseTreeNode entryNode)
        {
            if (entryNode.ChildNodes.Count < 4) return null;
            if (entryNode.ChildNodes[2].Token == null) return null;
            return entryNode.ChildNodes[2].Token.Text.Replace("\"", "");
        }

        public static T GetBasicValEnum<T>(this ParseTreeNode entryNode)
        {
            return entryNode.GetBasicValString().ToEnum<T>();
        }

        public static T ToEnum<T>(this string txt)
        {
            return (T) Enum.Parse(typeof (T), txt);
        }

        public static decimal GetBasicValDecimal(this ParseTreeNode entryNode)
        {
            //return decimal.Parse(entryNode.GetBasicValString());
            if (entryNode.ChildNodes.Count < 2) return 0;
            return Convert.ToDecimal(entryNode.ChildNodes[2].Token.Value);
        }

        public static int GetBasicValInt(this ParseTreeNode entryNode)
        {
            return int.Parse(entryNode.GetBasicValString());
        }

        public static bool GetBasicValBool(this ParseTreeNode entryNode)
        {
            return bool.Parse(entryNode.GetBasicValString());
        }

        public static DimensionedValue GetDimVal(this ParseTreeNode entryNode)
        {
            if (entryNode.ChildNodes.Count < 12) return default(DimensionedValue);
            //if (entryNode.ChildNodes.Any(n => n.Token == null)) return default(DimensionedValue);

            var dval = new DimensionedValue();
            dval.Name = entryNode.ChildNodes[2].Token.Text;

            dval.Mass = (int) entryNode.ChildNodes[3].Token.Value;
            dval.Length = (int) entryNode.ChildNodes[4].Token.Value;
            dval.Time = (int) entryNode.ChildNodes[5].Token.Value;
            dval.Temperature = (int) entryNode.ChildNodes[6].Token.Value;
            dval.Quantity = (int) entryNode.ChildNodes[7].Token.Value;
            dval.Current = (int) entryNode.ChildNodes[8].Token.Value;
            dval.LuminousIntensity = (int) entryNode.ChildNodes[9].Token.Value;

            dval.ScalarValue = Convert.ToDecimal(entryNode.ChildNodes[10].Token.Value);

            return dval;
        }

        public static ParseTreeNode GetDictArrayBody(this ParseTreeNode node)
        {
            return node.ChildNodes[2].ChildNodes[1];
        }

        public static T[] GetArray<T>(this ParseTreeNode arrayBodyNode, Func<ParseTreeNode, T> fconvert)
        {
            if (arrayBodyNode.ChildNodes.Count < 1) return null;
            var arraySize = arrayBodyNode.ChildNodes.Count;
            var a = new T[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                a[i] = fconvert(arrayBodyNode.ChildNodes[i].ChildNodes[0]);
            }
            return a;
        }

        public static decimal[] GetArrayOfDecimal(this ParseTreeNode arrayBodyNode)
        {
            return arrayBodyNode.GetArray(n => Convert.ToDecimal(n.Token.Value));
        }

        public static Int32[] GetArrayOfInt32(this ParseTreeNode arrayBodyNode)
        {
            return arrayBodyNode.GetArray(n => Convert.ToInt32(n.Token.Value));
        }

        public static String[] GetArrayOfString(this ParseTreeNode arrayBodyNode)
        {
            return arrayBodyNode.GetArray(n => Convert.ToString(n.Token.Value));
        }

        public static Vertice[] GetVectorsArray(this ParseTreeNode arrayBodyNode)
        {
            Func<ParseTreeNode, Vertice> fconv = node =>
            {
                var arrayBody = node.ChildNodes[1];
                var v = new Vertice();
                v.X = arrayBody.GetArrayOfDecimal()[0];
                v.Y = arrayBody.GetArrayOfDecimal()[1];
                v.Z = arrayBody.GetArrayOfDecimal()[2];
                return v;
            };
            return arrayBodyNode.GetArray(fconv);
        }
    }
}