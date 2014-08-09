using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alissa.Differ2
{
    /// <summary>
    /// Abstract base class for Diff algorithms.
    /// </summary>
    /// <typeparam name="T">Type of elements of sequences.</typeparam>
    [ComVisible(false)]
    public abstract class DifferBase<T> : IDiffer<T>
    {
        /// <summary>
        /// Subclasses use this delegate when comparing elements in sequences.
        /// </summary>
        protected Comparison<T> Comp = Comparer<T>.Default.Compare;

        /// <summary>
        /// A Comparison to compare two elements.
        /// </summary>
        /// <remarks>
        /// <para>
        /// You may specify either <see cref="Comparison"/> or <see cref="Comparer"/> but not both.
        /// If neither are specified, a default for type T is used.
        /// </para>
        /// <para>
        /// Many algorithms use it as an equality comparator, i.e., 
        /// only checks it is zero or non-zero.
        /// However, some algorithms require less-than and greater-than
        /// are returned appropriately.
        /// </para>
        /// </remarks>
        public Comparison<T> Comparison { set { Comp = value; } }

        /// <summary>
        /// A Comparer to compare two elements.
        /// </summary>
        /// <remarks>
        /// See <see cref="Comparison"/>.
        /// </remarks>
        public Comparer<T> Comparer { set { Comp = value.Compare; } }

        /// <summary>
        /// Subclasses implements this method to implement <see cref="IDiffer.Compare"/>
        /// </summary>
        /// <param name="src">The source sequence.</param>
        /// <param name="dst">The destination sequence.</param>
        /// <returns>A string describing the difference.</returns>
        public abstract string Compare(IList<T> src, IList<T> dst);

        /// <summary>
        /// Reorder the difference string so that it is in a canonical order.
        /// </summary>
        /// <param name="changes">A string of '=', '+', and/or '-'.</param>
        /// <returns>A canonical difference string.</returns>
        protected static string Reorder(string changes)
        {
            var sb = new StringBuilder(changes.Length);
            int a = 0;
            foreach (var c in changes)
            {
                switch (c)
                {
                    case '+':
                        a++;
                        break;
                    case '=':
                        if (a > 0)
                        {
                            sb.Append('+', a);
                            a = 0;
                        }
                        sb.Append(c);
                        break;
                    case '-':
                        sb.Append(c);
                        break;
                    default:
                        throw new ArgumentException(string.Format("unknown char '{0}' (U+{0:X})", c), "changes");
                }
            }
            return sb.Append('+', a).ToString();
        }
    }
}
