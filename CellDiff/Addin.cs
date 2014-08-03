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
using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;
using NetOffice.OfficeApi;
using NetOffice.OfficeApi.Enums;
using NetOffice.Tools;
using NetOffice.ExcelApi.Tools;

using Alissa.GUI;

namespace CellDiff
{
    [COMAddin("CellDiff","Detect differences of texts in worksheet cells",3)]
    //[CustomUI("CellDiff.RibbonUI.xml")]
    [GuidAttribute("3DF43D38-7D7E-4B28-A5D9-CF795FF10A32"), ProgId("CellDiff.Addin")]
    public partial class Addin : COMAddin
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

        private static readonly Decoration DEFAULT_SOURCE_DECORATION = new Decoration() { Strikeout = true, Bold = true, Color = 0x000080 };
        private static readonly Decoration DEFAULT_TARGET_DECORATION = new Decoration() { Underline = true, Bold = true, Color = 0x008000 }; 

        private static readonly Options QUICK_OPTIONS = new Options()
        {
            Src = DEFAULT_SOURCE_DECORATION,
            Tgt = DEFAULT_TARGET_DECORATION
        };

        private Advanced.OptionValues AdvancedOptions = new Advanced.OptionValues()
        {
            SourceDecoration = DEFAULT_SOURCE_DECORATION,
            TargetDecoration = DEFAULT_TARGET_DECORATION
        };

        public void OnAction(IRibbonControl control)
        {
            Application.ScreenUpdating = false;
            try
            {
                switch (control.Id)
                {
                    case "compareCellsButton":
                        using (var selection = Application.Selection as IDisposable)
                        {
                            if (selection is Range)
                            {
                                QuickCompare(selection as Range, QUICK_OPTIONS);
                            }
                            else
                            {
                                Error("Please select cells to compare.");
                            }
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
            catch (Exception e)
            {
                using (var dlg = new ExceptionDialog())
                {
                    dlg.Exception = e;
                    dlg.ShowDialog();
                }
            }
            finally
            {
                // Never leave the status bar in our own.
                Application.StatusBar = false;
                Application.ScreenUpdating = true;
            }
        }

        #endregion
    }
}
