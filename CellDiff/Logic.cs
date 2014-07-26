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
                MessageBox.Show("Please select cells to compare.");
                return;
            }
            switch (selection.Areas.Count)
            {
                case 1:
                    var area = selection.Areas[0];
                    var address = area.Address(true, true);
                    MessageBox.Show(address);
                    break;
                case 2:
                    break;
                default:
                    MessageBox.Show("Please select either two contiguous ranges of cells or a contiguous range of cells.");
                    break;
            }
        }
    }
}
