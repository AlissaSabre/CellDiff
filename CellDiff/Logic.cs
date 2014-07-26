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

            Range range1, range2;
            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[1];
                    var loc = area.GetLocation();
                    if (loc[2] == 2)
                    {
                        // Go vertical
                        range1 = area.Columns[1];
                        range2 = area.Columns[2];
                    }
                    else if (loc[3] == 2)
                    {
                        // GO horizontal
                        range1 = area.Rows[1];
                        range2 = area.Rows[2];
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
                    var loc1 = area1.GetLocation();
                    var loc2 = area2.GetLocation();
                    if ((loc1[2] != 1 && loc1[3] != 1) || (loc2[2] != 1 && loc2[3] != 1))
                    {
                        Error("WRONG SELECTION");
                        return;
                    }
                    else if (loc1[2] == 1 && loc2[2] == 1 && loc1[3] == loc2[3]
                        ||   loc1[2] == 1 && loc2[3] == 1 && loc1[3] == loc2[2]
                        ||   loc1[3] == 1 && loc2[2] == 1 && loc1[2] == loc2[3]
                        ||   loc1[3] == 1 && loc2[3] == 1 && loc1[2] == loc2[2])
                    {
                        range1 = area1;
                        range2 = area2;
                    }
                    else
                    {
                        Error("When selecting two separate ranges, they must have a same length.");
                        return;
                    }
                    break;
                default:
                    MessageBox.Show("WRONG SELECTION");
                    return;
            }

            MessageBox.Show("Range1 = " + range1.Address + ", Range2 = " + range2.Address);
        }

        public static void Error(string s)
        {
            MessageBox.Show(s);
        }
    }
}
