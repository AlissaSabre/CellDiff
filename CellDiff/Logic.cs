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

        public static void QuickCompare(NetOffice.ExcelApi.Application excel, Options options)
        {
            var selection = excel.Selection as Range;
            if (selection == null)
            {
                Error("Please select cells to compare.");
                return;
            }

            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[1];
                    if (area.Columns.Count == 2)
                    {
                        // Go vertical
                        CompareRanges(area.Columns[1], area.Columns[2], null, options);
                    }
                    else if (area.Rows.Count == 2)
                    {
                        // GO horizontal
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
        }

        public static void CompareRanges(Range sources, Range targets, Range destinations, Options options)
        {
            var src = sources.Cells.ToArray();
            var tgt = targets.Cells.ToArray();
            var dst = destinations == null ? null : destinations.Cells.ToArray();

            MessageBox.Show("src = " + string.Join(",", src.Select(r => r.Address(false, false)))
                        + ", tgt = " + string.Join(",", tgt.Select(r => r.Address(false, false))));

            for (int i = 0; i < src.Length; i++)
            {
                CompareCells(src[i], tgt[i], (dst == null) ? null : dst[i], options);
            }
        }

        private static readonly IDiffer<char> Differ = new GreedyDiffer<char>();

        private static void CompareCells(Range src, Range tgt, Range dst, Options options)
        {
            var src_text = src.Text.ToString();
            var tgt_text = tgt.Text.ToString();
            var diff = Differ.Compare(src_text.ToCharArray(), tgt_text.ToCharArray()).ToCharArray();

            if (dst == null)
            {
                src.Value2 = src_text;
                tgt.Value2 = tgt_text;
                int i = 1, j = 1;
                foreach (var c in diff)
                {
                    switch (c)
                    {
                        case '-': Decorate(src, i++, 1, options.Src); break;
                        case '+': Decorate(tgt, j++, 1, options.Tgt); break;
                        case '=': i++; j++; break;
                    }
                }
            }
            else
            {
                int i = 0, j = 0;
                var d = new StringBuilder();
                foreach (var c in diff)
                {
                    switch (c)
                    {
                        case '-': d.Append(src_text[i++]); break;
                        case '+': d.Append(tgt_text[j++]); break;
                        case '=': d.Append(src_text[i++]); j++; break;
                    }
                }
                dst.Value2 = d.ToString();

                int k = 1;
                foreach (var c in diff)
                {
                    switch (c)
                    {
                        case '-': Decorate(dst, k++, 1, options.Src); break;
                        case '+': Decorate(dst, k++, 1, options.Tgt); break;
                        case '=': k++; break;
                    }
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
    }
}
