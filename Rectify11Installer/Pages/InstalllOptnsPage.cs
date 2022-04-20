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
    public partial class InstalllOptnsPage : WizardPage, IRectifyInstalllerInstallOptions
    {
        public bool ShouldInstallExplorerPatcher { get => chkThemes.Checked; }
        public bool ShouldInstallThemes { get => chkThemes.Checked; }
        public bool ShouldInstallWallpaper { get => chkWallpaper.Checked; }
        public bool ShouldInstallWinver { get => chkWinVer.Checked; }
        public bool DoSafeInstall { get => radSafe.Checked; }
        public InstalllOptnsPage()
        {
            InitializeComponent();
        }
    }
}
