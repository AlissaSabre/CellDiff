using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alissa.Differ2
{
    /// <summary>
    /// A variation of the greedy algorithm by Eugene W. Myers (1986). O((M+N)*D).
    /// It means this algorithm works fast if given two lists are <i>similar</i>.
    /// </summary>
    [ComVisible(false)]
    public class GreedyDiffer<T> : DifferBase<T>
    {
        private struct Head
        {
            public int Src, Dst;
            public string Tra;
        }

        /// <summary>
        /// Implements <see cref="IDiffer.Compare"/>.
        /// </summary>
        /// <param name="src">The source sequence.</param>
        /// <param name="dst">The destination sequence.</param>
        /// <returns>A canonical difference string.</returns>
        public override string Compare(IList<T> src, IList<T> dst)
        {
            var sn = src.Count;
            var dn = dst.Count;
            while (sn > 0 && dn > 0 && Comp(src[sn - 1], dst[dn - 1]) == 0)
            {
                --sn;
                --dn;
            }
            if (sn == 0) return new String('+', dn) + new String('=', src.Count);
            if (dn == 0) return new String('-', sn) + new String('=', dst.Count);

            var heads = new Head[sn + dn + 3];
            var z = heads.Length - 2;
            var orig = sn + 1;
            var goal = dn + 1;
            heads[orig].Tra = "";

            for (var i = 1; i < z; i++)
            {
                var min = orig - i + 1;
                var max = orig + i - 1;
                for (var p = min; p <= max; p += 2)
                {
                    if (p < 1 || p > z) continue;

                    var s = heads[p].Src;
                    var d = heads[p].Dst;
                    var r = heads[p].Tra;

                    // Find a snake.
                    var u = 0;
                    while (s + u < sn && d + u < dn && Comp(src[s + u], dst[d + u]) == 0) u++;
                    if (u > 0)
                    {
                        s += u;
                        d += u;
                        r += new String('=', u);
                    }

                    // Recognize a difference.
                    if (heads[p - 1].Src + heads[p - 1].Dst <= s + d)
                    {
                        heads[p - 1].Src = s + 1;
                        heads[p - 1].Dst = d;
                        heads[p - 1].Tra = r + '-';
                    }
                    if (heads[p + 1].Src + heads[p + 1].Dst <= s + d)
                    {
                        heads[p + 1].Src = s;
                        heads[p + 1].Dst = d + 1;
                        heads[p + 1].Tra = r + '+';
                    }
                }

                // See if we have reached the goal.
                if (heads[goal].Src >= sn && heads[goal].Dst >= dn)
                {
                    return Reorder(heads[goal].Tra) + new string('=', src.Count - sn);
                }
            }

            throw new ApplicationException("Edit matrix overrun.  It means this program has a bug in it.");
        }
    }
}
