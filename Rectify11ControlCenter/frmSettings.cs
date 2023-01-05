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
            themeApplied.Text = Rectify11ControlCenter.Controls.theme();
            r11Ver.Text = Rectify11ControlCenter.Controls.r11Version;
            deskImg.Image = Rectify11ControlCenter.Controls.deskimg();
            Rectangle r = new Rectangle(0, 0, deskImg.Width, deskImg.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            int d = 10;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            deskImg.Region = new Region(gp);
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
