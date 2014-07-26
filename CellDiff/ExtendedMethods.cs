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
        /// Gets location values of a Range.
        /// </summary>
        /// <param name="range">The range to get the location.</param>
        /// <returns>An array of four integer values to specify a location.</returns>
        /// <remarks>
        /// <para>
        /// This method assumes the <paramref name="range"/> specifies a contiguous Range,
        /// i.e., a single rectangular area of cells.
        /// Otherwise the behaviour is undefined.
        /// </para>
        /// <para>
        /// This method returns an array of four values, <c>{ c, r, w, h }</c>, where
        /// <ul>
        /// <li>c is the column address of the left-most cells, </li>
        /// <li>r is the row address of the top-most cells, </li>
        /// <li>w is the number of columns the range spans, and</li>
        /// <li>h is the number of rows the range spans.</li>
        /// </ul>
        /// </para>
        /// <para>
        /// For example, 
        /// given a range consisting of the top-left cell ("A1") on a worksheet,
        /// this method will return <c>{ 1, 1, 1, 1 }</c>
        /// </para>
        /// </remarks>
        public static int[] GetLocation(this Range range)
        {
            var a = range.Address(true, true, XlReferenceStyle.xlR1C1)
                .Split(R1C1_DELIMITERS, 6)
                .Select(s => (s == "") ? 0 : Int32.Parse(s))
                .ToArray();
            if (a.Length < 4)
            {
                // "R1C2"
                return new[] { a[2], a[1], 1, 1 };
            }
            else
            {
                // "R1C2:R4C5
                return new[] { a[2], a[1], a[5] - a[2] + 1, a[4] - a[1] + 1 };
            }
        }
    }
}
