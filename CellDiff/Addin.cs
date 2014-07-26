using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using NetOffice;
using Excel = NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using Office = NetOffice.OfficeApi;
using NetOffice.OfficeApi.Enums;
using VBIDE = NetOffice.VBIDEApi;
using NetOffice.VBIDEApi.Enums;
using NetOffice.Tools;
using NetOffice.ExcelApi.Tools;

namespace CellDiff
{
    [COMAddin("CellDiff","Detect differences of texts in worksheet cells",3)]
    //[CustomUI("CellDiff.RibbonUI.xml")]
    [GuidAttribute("3DF43D38-7D7E-4B28-A5D9-CF795FF10A32"), ProgId("CellDiff.Addin")]
    public class Addin : COMAddin
    {
        public Addin()
        {
            this.OnStartupComplete += new OnStartupCompleteEventHandler(Addin_OnStartupComplete);
            this.OnConnection += new OnConnectionEventHandler(Addin_OnConnection);
            this.OnDisconnection += new OnDisconnectionEventHandler(Addin_OnDisconnection);
        }

        #region IDTExtensibility2 Members

        void Addin_OnConnection(object Application, NetOffice.Tools.ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        {   
            //using (var languageSettings = (Application as Excel.Application).LanguageSettings)
            //{
            //    System.Windows.Forms.Application.CurrentCulture = CultureInfo.GetCultureInfo(languageSettings.LanguageID(MsoAppLanguageID.msoLanguageIDUI));
            //}
        }

        void Addin_OnStartupComplete(ref Array custom)
        {

        }

        void Addin_OnDisconnection(NetOffice.Tools.ext_DisconnectMode RemoveMode, ref Array custom)
        {
           
        }

        #endregion		

		#region IRibbonExtensibility Members

        public override string GetCustomUI(string RibbonID)
        {
            return Properties.Resources.RibbonUI;
        }

        private Advanced.OptionValues AdvancedOptions = new Advanced.OptionValues()
        {
            SourceStrikeout = true,
            SourceBold = true,
            TargetUnderline = true,
            TargetBold = true
        };

        public void OnAction(Office.IRibbonControl control)
        {
            try
            {
                switch (control.Id)
                {
                    case "compareCellsButton":
                        using (var excel = Application.Application)
                        {
                            Logic.QuickCompare(excel);
                        }
                        break;
                    case "dialogLauncher":
                        DialogResult result;
                        using (var dlg = new Advanced())
                        {
                            dlg.Options = AdvancedOptions;
                            result = dlg.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                AdvancedOptions = dlg.Options;
                            }
                        }

                        break;
                    default:
                        MessageBox.Show("Unkown Control Id: " + control.Id);
                        break;
                }
            }
            catch (Exception throwedException)
            {
                string details = string.Format("{1}{1}Details:{1}{1}{0}", throwedException.Message, Environment.NewLine);
                MessageBox.Show("An error occured in OnAction: " + details, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
