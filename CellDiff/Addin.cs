using System;
using System.ComponentModel;
using System.Drawing;
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

using CellDiff.Properties;

namespace CellDiff
{
    /// <summary>
    /// Main class of CellDiff Addin.
    /// </summary>
    [COMAddin("CellDiff", "Excel add-in to compare cell contents", 3)]
    [RegistryLocation(RegistrySaveLocation.LocalMachine)]
    [GuidAttribute("3DF43D38-7D7E-4B28-A5D9-CF795FF10A32"), ProgId("CellDiff.Addin")]
    public partial class Addin : COMAddin
    {
        /// <summary>
        /// Creates an instance of Addin.
        /// </summary>
        public Addin()
        {
            //this.OnStartupComplete += new OnStartupCompleteEventHandler(Addin_OnStartupComplete);
            //this.OnConnection += new OnConnectionEventHandler(Addin_OnConnection);
            //this.OnDisconnection += new OnDisconnectionEventHandler(Addin_OnDisconnection);
        }

        #region IDTExtensibility2 Members

        //void Addin_OnConnection(object Application, NetOffice.Tools.ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        //{
        //}

        //void Addin_OnStartupComplete(ref Array custom)
        //{

        //}

        //void Addin_OnDisconnection(NetOffice.Tools.ext_DisconnectMode RemoveMode, ref Array custom)
        //{
           
        //}

        #endregion		

		#region IRibbonExtensibility Members

        /// <summary>
        /// Implements <see cref="IRibbonExtensibility"/>.
        /// </summary>
        /// <param name="RibbonID">Ignored.</param>
        /// <returns>An XML instance to define RibbonUI elements of this Addin.</returns>
        public override string GetCustomUI(string RibbonID)
        {
            SyncUICulture();
            return Resources.RibbonUI;
        }

        private static readonly Decoration DEFAULT_SOURCE_DECORATION = new Decoration() { Strikeout = true, Bold = true, Color = Color.Maroon };
        private static readonly Decoration DEFAULT_TARGET_DECORATION = new Decoration() { Underline = true, Bold = true, Color = Color.Green }; 

        private static readonly Options QUICK_OPTIONS = new Options()
        {
            Src = DEFAULT_SOURCE_DECORATION,
            Tgt = DEFAULT_TARGET_DECORATION
        };

        private AdvancedDialog.OptionValues AdvancedOptions = new AdvancedDialog.OptionValues()
        {
            SourceDecoration = DEFAULT_SOURCE_DECORATION,
            TargetDecoration = DEFAULT_TARGET_DECORATION
        };

        /// <summary>
        /// Called back by Ribbon controls.
        /// </summary>
        /// <param name="control">The control that caused the callback.</param>
        public void OnAction(IRibbonControl control)
        {
            Application.ScreenUpdating = false;
            try
            {
                switch (control.Id)
                {
                    case "quickCompare":
                        QuickCompare();
                        break;
                    case "advancedCompare":
                        AdvancedCompare();
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

                // Ensure reclaiming all temporary COM objects.
                Application.DisposeChildInstances();
            }
        }

        #endregion

        /// <summary>
        /// Synchronizes the UI culture of the current thread with the Excel's.
        /// </summary>
        internal void SyncUICulture()
        {
            // The following code is from http://msdn.microsoft.com/en-us/library/vstudio/w9x4hz7x(v=vs.100).aspx
            // For whatever reason, it doesn't work well on my PC, so it is disabled.

            //using (var languageSettings = Application.LanguageSettings)
            //{
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSettings.LanguageID(MsoAppLanguageID.msoLanguageIDUI));
            //}
        }
    }
}
