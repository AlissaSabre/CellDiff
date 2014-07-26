using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alissa.Differ2
{
    /// <summary>
    /// The Differ interface.
    /// </summary>
    public interface IDiffer<T>
    {
        string Compare(IList<T> src, IList<T> dst);
    }
}
