using System;
using System.Collections.Generic;

namespace Offwind.Infrastructure
{
    public static class TreeTraversal
    {
        /// <summary>
        /// Taken from http://stackoverflow.com/questions/10253161/efficient-graph-traversal-with-linq-eliminating-recursion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static IEnumerable<T> Traverse<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var stack = new Stack<T>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                T item = stack.Pop();
                yield return item;
                foreach (var child in children(item))
                    stack.Push(child);
            }
        }

        /// <summary>
        /// If you have a cyclic or interconnected graph then what you want is the transitive closure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static IEnumerable<T> Closure<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                T item = stack.Pop();
                if (seen.Contains(item))
                    continue;
                seen.Add(item);
                yield return item;
                foreach (var child in children(item))
                    stack.Push(child);
            }
        }
    }
}
