using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Controls
{
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.FromArgb(36, 36, 36);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;


            rec.Height -= 4;
            e.Graphics.FillRectangle(Brushes.Green, 2, 2, rec.Width, rec.Height);
        }
    }
}
