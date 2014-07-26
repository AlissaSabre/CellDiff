using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetOffice.ExcelApi;


namespace CellDiff
{
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

            Range[] range1, range2;
            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[1];
                    var dim = area.Dimension();
                    if (dim[0] == 2)
                    {
                        // Go vertical
                        range1 = area.Columns[1].ToArray();
                        range2 = area.Columns[2].ToArray();
                    }
                    else if (dim[1] == 2)
                    {
                        // GO horizontal
                        range1 = area.Rows[1].ToArray();
                        range2 = area.Rows[2].ToArray();
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
                    var dim1 = area1.Dimension();
                    var dim2 = area2.Dimension();
                    if (dim1[0] == 1 && dim2[0] == 1 && dim1[1] == dim2[1] ||
                        dim1[0] == 1 && dim2[1] == 1 && dim1[1] == dim2[0] ||
                        dim1[1] == 1 && dim2[0] == 1 && dim1[0] == dim2[1] ||
                        dim1[1] == 1 && dim2[1] == 1 && dim1[0] == dim2[0])
                    {
                        range1 = area1.ToArray();
                        range2 = area2.ToArray();
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

            MessageBox.Show("Range1 = " + string.Join(",", range1.Select(r => r.Address(false, false)))
                        + ", Range2 = " + string.Join(",", range2.Select(r => r.Address(false, false))));
        }

        public static void Error(string s)
        {
            MessageBox.Show(s);
        }
    }
}
