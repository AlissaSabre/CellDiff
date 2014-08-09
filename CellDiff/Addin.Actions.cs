using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetOffice;
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using NetOffice.ExcelApi.Tools;

using Alissa.Differ2;

using CellDiff.Properties;

namespace CellDiff
{
    public partial class Addin : COMAddin
    {
        public class Options
        {
            public Decoration Src;
            public Decoration Tgt;
        }

        /// <summary>
        /// Compare Cells in Selection using the default set of options.
        /// </summary>
        public void QuickCompare()
        {
            var selection = Application.Selection as Range;
            if (selection != null)
            {
                var ranges = FindCompareRanges(selection as Range, false);
                if (ranges != null)
                {
                    CompareRanges(ranges[0], ranges[1], null, QUICK_OPTIONS);
                }
            }
            else
            {
                Error("Please select cells to compare.");
            }
        }

        /// <summary>
        /// Compare cells through an AdvancedOptions dialog.
        /// </summary>
        public void AdvancedCompare()
        {
            using (var dlg = new AdvancedDialog())
            {
                dlg.Options = AdvancedOptions;
                if (DialogResult.OK != dlg.ShowDialog()) return;
                AdvancedOptions = dlg.Options;
            }

            var src = Application.Range(AdvancedOptions.Sources).Cells;
            var tgt = Application.Range(AdvancedOptions.Targets).Cells;
            var dst = AdvancedOptions.SeparateDestinateions ? Application.Range(AdvancedOptions.Destinations).Cells : null;
            CompareRanges(src, tgt, dst,
                new Options()
                {
                    Src = AdvancedOptions.SourceDecoration,
                    Tgt = AdvancedOptions.TargetDecoration
                });
        }

        /// <summary>
        /// A sort of a tiebreaker for a quick compare 
        /// </summary>
        private bool PreferVertical = false;

        Range[] FindCompareRanges(Range selection, bool generous)
        {
            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[1];
                    if (area.Columns.Count == 2 && (PreferVertical || area.Rows.Count != 2))
                    {
                        // Go vertical
                        PreferVertical = true;
                        return new[] { area.Columns[1].Cells, area.Columns[2].Cells };
                    }
                    else if (area.Rows.Count == 2)
                    {
                        // GO horizontal
                        PreferVertical = false;
                        return new [] { area.Rows[1].Cells, area.Rows[2].Cells };
                    }
                    else
                    {
                        if (!generous) Error("WRONG SELECTION");
                        return null;
                    }

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
                        return new[] { area1.Cells, area2.Cells };
                    }
                    else
                    {
                        if (!generous) Error("WRONG SELECTION");
                        return null;
                    }

                default:
                    if (!generous) Error("WRONG SELECTION");
                    return null;
            }
        }

        private readonly TimeSpan UPDATE_TIME_DELTA = TimeSpan.FromSeconds(5);

        private const int UPDATE_INDEX_DIVIDER = 20;

        /// <summary>
        /// Compare cells.
        /// </summary>
        /// <param name="sources">The source cells.</param>
        /// <param name="targets">The target cells.</param>
        /// <param name="destinations">The cells to store the results; or null if the results are to overwrite source and target cells.</param>
        /// <param name="options">The options for results.</param>
        public void CompareRanges(Range sources, Range targets, Range destinations, Options options)
        {
            var src = sources.Cells;
            var tgt = targets.Cells;
            var dst = (destinations == null) ? null : destinations.Cells;

            if (dst != null)
            {
                dst.Clear();
                dst.NumberFormat = "@";
            }

            var length = src.Count;
            var update_index_delta = length / UPDATE_INDEX_DIVIDER;
            var next_update_index = 0;
            var next_update_time = DateTime.Now;

            for (int i = 1; i <= length; i++)
            {
                if (i > next_update_index || DateTime.Now > next_update_time)
                {
                    Application.ScreenUpdating = true;
                    Application.StatusBar = string.Format(Resources.ProgressMessage, (i - 1) / (double)length);
                    Application.ScreenUpdating = false;
                    next_update_index = i + update_index_delta;
                    next_update_time = DateTime.Now + UPDATE_TIME_DELTA;
                }

                using (Range s = src[i], t = tgt[i], d = (dst == null) ? null : dst[i])
                {
                    if (dst == null)
                    {
                        CompareCells2(s, t, options);
                    }
                    else
                    {
                        CompareCells3(s, t, d, options);
                    }
                }
            }
        }

        private static readonly IDiffer<char> Differ = new GreedyDiffer<char>();

        private void CompareCells2(Range src, Range tgt, Options options)
        {
            var src_text = src.Text.ToString();
            var tgt_text = tgt.Text.ToString();
            var diff = Differ.Compare(src_text.ToCharArray(), tgt_text.ToCharArray()).Runs();

            src.NumberFormat = "@";  src.Value2 = src_text;
            tgt.NumberFormat = "@";  tgt.Value2 = tgt_text;

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

        private void CompareCells3(Range src, Range tgt, Range dst, Options options)
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

        private void Decorate(Range cell, int start, int length, Decoration d)
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

        internal void Error(string s)
        {
            MessageBox.Show(s);
        }
    }
}
