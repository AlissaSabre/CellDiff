using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetOffice.ExcelApi;


namespace CellDiff
{
    public class CompareOptions
    {
    }

    public static class Logic
    {
        public static void QuickCompare(NetOffice.ExcelApi.Application excel)
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
                        CompareCells(area.Columns[1], area.Columns[2], null, null);
                    }
                    else if (area.Rows.Count == 2)
                    {
                        // GO horizontal
                        CompareCells(area.Rows[1], area.Rows[2], null, null);
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
                        CompareCells(area1, area2, null, null);
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

        public static void CompareCells(Range sources, Range targets, Range destinations, CompareOptions options)
        {
            var src = sources.Cells.ToArray();
            var tgt = targets.Cells.ToArray();
            var dst = destinations == null ? null : destinations.Cells.ToArray();

            MessageBox.Show("src = " + string.Join(",", src.Select(r => r.Address(false, false)))
                        + ", dst = " + string.Join(",", dst.Select(r => r.Address(false, false))));
        }

        public static void Error(string s)
        {
            MessageBox.Show(s);
        }
    }
}
