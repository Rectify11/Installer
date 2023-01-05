using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11ControlCenter
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            OSname.Text = Rectify11ControlCenter.Controls.osV;
            username.Text = Rectify11ControlCenter.Controls.userN;
            pcname.Text = Rectify11ControlCenter.Controls.CumterName;
            themeApplied.Text = Rectify11ControlCenter.Controls.theme;
            r11Ver.Text = Rectify11ControlCenter.Controls.r11Version;
            deskImg.Image = Rectify11ControlCenter.Controls.deskimg();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
