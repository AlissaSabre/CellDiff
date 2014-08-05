using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellDiff
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Count lengths of runs in a sequence
        /// </summary>
        /// <typeparam name="T">Type of items in a sequence.</typeparam>
        /// <param name="source">The sequence of items.</param>
        /// <returns>Sequence of tuples.</returns>
        /// <remarks>
        /// <para>
        /// A <i>run</i> is a series of same items in a sequence.  A <i>length</i> of a run is a 
        /// </para>
        /// </remarks>
        public static IEnumerable<Tuple<T, int>> Runs<T>(this IEnumerable<T> source)
        {
            T last = default(T);
            int run = 0;
            bool first = true;
            foreach (var item in source)
            {
                if (first)
                {
                    first = false;
                    last = item;
                    run = 1;
                }
                else if (EqualityComparer<T>.Default.Equals(last, item))
                {
                    run++;
                }
                else
                {
                    yield return Tuple.Create(last, run);
                    last = item;
                    run = 1;
                }
            }
            if (run > 0)
            {
                yield return Tuple.Create(last, run);
            }
        }
    }
}
