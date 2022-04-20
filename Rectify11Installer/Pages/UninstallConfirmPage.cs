using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class UninstallConfirmPage : WizardPage, IRectifyInstalllerUninstallOptions
    {
        public UninstallConfirmPage()
        {
            InitializeComponent();
        }

        public bool RemoveExplorerPatcher => chkExplorerPatcher.Checked;

        public bool RemoveThemesAndThemeTool => chkRemoveThemes.Checked;

        public bool RestoreWallpapers => chkRestoreWallpaper.Checked;

        public bool RestoreWinver => chkRestoreWinVer.Checked;
    }
}
