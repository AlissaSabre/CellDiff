using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alissa.Differ2
{
    public abstract class DifferBase<T> : IDiffer<T>
    {
        protected readonly Comparison<T> Comp;

        protected DifferBase(Comparison<T> comparison)
        {
            Comp = comparison;
        }

        protected DifferBase(IComparer<T> comparer)
        {
            Comp = comparer.Compare;
        }

        protected DifferBase()
        {
            Comp = Comparer<T>.Default.Compare;
        }

        public string Compare(IList<T> src, IList<T> dst)
        {
            return Reorder(DoCompare(src, dst));
        }

        protected abstract string DoCompare(IList<T> src, IList<T> dst);

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
