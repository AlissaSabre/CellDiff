using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

using Alissa.Differ2;

namespace CellDiff
{
    static class Logic
    {
        public class Options
        {
            public Decoration Src;
            public Decoration Tgt;
        }

        public static TimeSpan DiffTime;

        /// <summary>
        /// A sort of a tiebreaker for a quick compare 
        /// </summary>
        private static bool PreferVertical = false;

        internal static void QuickCompare(Range selection, Options options)
        {
            DiffTime = TimeSpan.Zero;
            var started = DateTime.Now;

            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[1];
                    if (area.Columns.Count == 2 && (PreferVertical || area.Rows.Count != 2))
                    {
                        // Go vertical
                        PreferVertical = true;
                        CompareRanges(area.Columns[1], area.Columns[2], null, options);
                    }
                    else if (area.Rows.Count == 2)
                    {
                        // GO horizontal
                        PreferVertical = false;
                        CompareRanges(area.Rows[1], area.Rows[2], null, options);
                    }
                    else
                    {
                        Error("WRONG SELECTION");
                        return;
                    }
                    break;

                case 2:
                    var area1 = selection.Areas[1];
                    var area2 = selection.Areas[2];
                    int a1c = area1.Columns.Count, a1r = area1.Rows.Count;
                    int a2c = area2.Columns.Count, a2r = area2.Rows.Count;
                    if (a1c == 1 && a2c == 1 && a1r == a2r ||
                        a1c == 1 && a2r == 1 && a1r == a2c ||
                        a1r == 1 && a2c == 1 && a1c == a2r ||
                        a1r == 1 && a2r == 1 && a1c == a2c)
                    {
                        CompareRanges(area1, area2, null, options);
                    }
                    else
                    {
                        Error("WRONG SELECTION");
                        return;
                    }
                    break;

                default:
                    Error("WRONG SELECTION");
                    return;
            }

            var GlueTime = DateTime.Now - started - DiffTime;

            MessageBox.Show(string.Format("Diff = {0}, Glue = {1}", DiffTime, GlueTime));
        }

        internal static void CompareRanges(Range sources, Range targets, Range destinations, Options options)
        {
            var src = sources.Cells;
            var tgt = targets.Cells;

            if (destinations == null)
            {
                var length = src.Count;
                for (int i = 1; i <= length; i++)
                {
                    using (Range s = src[i], t = tgt[i])
                    {
                        CompareCells2(s, t, options);
                    }
                }
            }
            else
            {
                var dst = destinations.Cells;
                var length = src.Count;
                for (int i = 1; i <= length; i++)
                {
                    using (Range s = src[i], t = tgt[i], d = dst[i])
                    {
                        CompareCells3(s, t, d, options);
                    }
                }
            }

        }

        private static readonly IDiffer<char> Differ = new TimedGreedyDiffer<char>();

        private class TimedGreedyDiffer<T> : IDiffer<T>
        {
            private readonly IDiffer<T> differ = new GreedyDiffer<T>();

            public string Compare(IList<T> src, IList<T> dst)
            {
                var started = DateTime.Now;
                var d = differ.Compare(src, dst);
                DiffTime += (DateTime.Now - started);
                return d;
            }
        }

        private static void CompareCells2(Range src, Range tgt, Options options)
        {
            var src_text = src.Text.ToString();
            var tgt_text = tgt.Text.ToString();
            var diff = Differ.Compare(src_text.ToCharArray(), tgt_text.ToCharArray()).Runs();

            src.Value2 = src_text;
            tgt.Value2 = tgt_text;

            int i = 1, j = 1;
            foreach (var t in diff)
            {
                var n = t.Item2;
                switch (t.Item1)
                {
                    case '-': Decorate(src, i, n, options.Src); i += n; break;
                    case '+': Decorate(tgt, j, n, options.Tgt); j += n; break;
                    case '=': i += n; j += n; break;
                }
            }
        }

        private static void CompareCells3(Range src, Range tgt, Range dst, Options options)
        {
            var src_text = src.Text.ToString();
            var tgt_text = tgt.Text.ToString();
            var diff = Differ.Compare(src_text.ToCharArray(), tgt_text.ToCharArray()).Runs().ToList();

            int i = 0, j = 0;
            var d = new StringBuilder();
            foreach (var t in diff)
            {
                var n = t.Item2;
                switch (t.Item1)
                {
                    case '-': d.Append(src_text, i, n); i += n; break;
                    case '+': d.Append(tgt_text, j, n); j += n; break;
                    case '=': d.Append(src_text, i, n); i += n; j += n; break;
                }
            }
            dst.Value2 = d.ToString();

            int k = 1;
            foreach (var t in diff)
            {
                var n = t.Item2;
                switch (t.Item1)
                {
                    case '-': Decorate(dst, k, n, options.Src); k += n; break;
                    case '+': Decorate(dst, k, n, options.Tgt); k += n; break;
                    case '=': k += n; break;
                }
            }
        }

        private static void Decorate(Range cell, int start, int length, Decoration d)
        {
            using (var c = cell.Characters(start, length))
            {
                var f = c.Font;
                f.Underline = d.Underline ? XlUnderlineStyle.xlUnderlineStyleSingle : XlUnderlineStyle.xlUnderlineStyleNone;
                f.Strikethrough = d.Strikeout;
                f.Bold = d.Bold;
                f.Color = d.Color;
            }
        }

        public static void Error(string s)
        {
            MessageBox.Show(s);
        }

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
