using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32UIDemo.Controls
{
    public class WinUIButton : Button
    {
        protected override void OnPaint(PaintEventArgs args)
        {
            args.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, this.Width, this.Height);
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(args.Graphics, Text, Font, new Point(Width + 3, this.Height / 2), ForeColor, flags);

            args.Graphics.DrawImage(null, 0, 0, Width, Height);
        }
    }
}
