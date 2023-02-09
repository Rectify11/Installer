using libmsstyle;
using System.Drawing;
using System.Windows.Forms;
using Rectify11Installer.Core;

namespace Rectify11Installer.Controls
{
    // will be used later
    public sealed class CustomProgressBar : ProgressBar
    {
        private bool _error = false;
        public bool Error 
        { 
	        get => _error;
	        set
	        {
		        _error = value; 
		        Invalidate();
	        }
        }
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
            var rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum));

            if (DesignMode)
            {
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            }
            else
            {
                var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;

                var part = Theme.GetProgressbarBg(currentTheme);
                var renderer2 = new PartRenderer(currentTheme, part);
                var b = renderer2.RenderPreview(ThemeParts.Normal, Width, Height);

                e.Graphics.DrawImage(b, new Point(0, 0));
            }

            if (rec.Width == 0)
                return;

            if (DesignMode)
            {
                e.Graphics.FillRectangle(Brushes.Green, 2, 2, rec.Width, rec.Height);
            }
            else
            {
                var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;

                var part = Theme.GetProgressbarFill(currentTheme);

				using var renderer2 = new PartRenderer(currentTheme, part);
				var tpart = Error ? ThemeParts.Pressed : ThemeParts.Normal;
				var b = renderer2.RenderPreview(tpart, rec.Width, rec.Height);

				e.Graphics.DrawImage(b, new Point(0, 0));
				b.Dispose();
			}
        }
    }
}