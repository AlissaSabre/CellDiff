using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

namespace CellDiff
{
    public static class ExtendedMethods
    {
        private static readonly char[] R1C1_DELIMITERS = "RC:".ToCharArray();

        /// <summary>
        /// Gets dimension of a Range.
        /// </summary>
        /// <param name="range">The range to get the dimension.</param>
        /// <returns>An array of two integer values to specify a dimension.</returns>
        /// <remarks>
        /// <para>
        /// This method assumes the <paramref name="range"/> specifies a contiguous Range,
        /// i.e., a single rectangular area of cells.
        /// Otherwise the behaviour is undefined.
        /// </para>
        /// <para>
        /// This method returns an array of two values, <c>{ c, r }</c>, where
        /// <ul>
        /// <li>c is the number of columns the range spans, and</li>
        /// <li>r is the number of rows the range spans.</li>
        /// </ul>
        /// </para>
        /// <para>
        /// For example, 
        /// If the given range consists of a single cell,
        /// this method will return <c>{ 1, 1 }</c>.
        /// If the range was "A2:C3", it returns <c>{ 3, 2 }</c>.
        /// </para>
        /// </remarks>
        public static int[] Dimension(this Range range)
        {
            var addr = range.Address(true, true, XlReferenceStyle.xlR1C1);
            if (addr.IndexOf(':') >= 0) return new[] { 1, 1 };

            var a = addr.Split(R1C1_DELIMITERS, 6)
                .Select(s => (s == "") ? 0 : Int32.Parse(s))
                .ToArray();
            return new[] { a[5] - a[2] + 1, a[4] - a[1] + 1 };
        }
    }
}
