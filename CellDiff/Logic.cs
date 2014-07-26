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
                default:
                    MessageBox.Show("Please select either two contiguous ranges of cells or a contiguous range of cells.");
                    break;
                case 2:
                    
            }
            if (selection.Areas.Count == 2)
        }
    }
}
