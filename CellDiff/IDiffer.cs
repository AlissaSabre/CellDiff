using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Alissa.Differ2
{
    /// <summary>
    /// The Differ interface.
    /// </summary>
    [ComVisible(false)]
    public interface IDiffer<T>
    {
        /// <summary>
        /// Compares two sequences and return a string describing the difference.
        /// </summary>
        /// <param name="src">The source sequence.</param>
        /// <param name="dst">The destination sequence.</param>
        /// <returns>A string describing the difference of <paramref name="src"/> and <paramref name="dst"/>.</returns>
        /// <remarks>
        /// <para>
        /// The returned string consists of three command characters, '=', '+', and '-', where 
        /// '=' is <i>pass</i> (the corresponding source and destination elements are equal),
        /// '+' is <i>add</i> (the corresponding destination element is added), and
        /// '-' is <i>delete</i> (the corresponding source element is deleted).
        /// </para>
        /// <para>
        /// For example, if T is char, and src is "ABCxyz" and dst is "xyzDEF",
        /// the returned string will be "---===+++" which means that the steps to make src to dst are:
        /// delete three characters ("ABC"), pass three characters ("xyz"), then add three characters ("DEF").
        /// </para>
        /// </remarks>
        string Compare(IList<T> src, IList<T> dst);
    }
}
