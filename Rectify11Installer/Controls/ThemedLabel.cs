using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
    public class ThemedLabel : Label
    {
        public ThemedLabel()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
            BackColor = Color.Transparent;
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
                ForeColor = Color.White;
            else
                ForeColor = Color.Black;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(0, 0));
        }
    }
}