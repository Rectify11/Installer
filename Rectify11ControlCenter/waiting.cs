using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Rectify11ControlCenter
{
    public partial class waiting : Form
    {
        public waiting(string s)
        {
            InitializeComponent();
            label1.Text = Rectify11ControlCenter.Controls.waitingtxt;
            applyTheme(s);
        }

        private void waiting_Load(object sender, EventArgs e)
        {

        }
        private async void applyTheme(string name)
        {
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Variables.sys32Folder, "cmd.exe") + " /c " + '"' + Path.Combine(Variables.Variables.Windir, "Resources", "Themes", name + ".theme") + '"' + " & timeout /t 03 /nobreak > NUL & taskkill /f /im systemsettings.exe", AppWinStyle.Hide, true));
            var MyIni = new IniFile(Path.Combine(Variables.Variables.Windir, "Resources", "Themes", name + ".theme"));
            string themename = MyIni.Read("DisplayName", "Theme");
            if (File.Exists(Path.Combine(Variables.Variables.Windir, "SecureUXHelper.exe")))
            {
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Variables.Windir, "SecureUXHelper.exe") + " apply " + '"' + themename + '"', AppWinStyle.Hide, true));
            }
            this.Close();
            this.Dispose();
        }
    }
}
