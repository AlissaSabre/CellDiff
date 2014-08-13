using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CellDiff
{
    [ComVisible(false)]
    public struct Decoration
    {
        public bool Underline;
        public bool Strikeout;
        public bool Bold;
        public Color Color;
    }
}
