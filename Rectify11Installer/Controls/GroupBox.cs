using libmsstyle;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
    public partial class GroupBox : Control
    {
        private NavigationButtonType t;
        private Bitmap Glyph;
        public NavigationButtonType NavigationButtonType
        {
            get { return t; }
            set
            {
                t = value;
                if (Enabled)
                {
                    SetState(ThemeParts.Normal);
                    Invalidate();
                }
                else
                {
                    SetState(ThemeParts.Disabled);
                    Invalidate();
                }

            }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (value)
                {
                    SetState(ThemeParts.Normal);
                }
                else
                {
                    SetState(ThemeParts.Disabled);
                }
                this.Invalidate();
            }
        }

        public GroupBox()
        {
            this.Location = new Point(3, 28);
            this.Size = new Size(313, 294);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
            SetState(ThemeParts.Normal);
            this.MouseUp += NavigationButton_MouseUp;

            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void NavigationButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                SetState(ThemeParts.Normal);
            }
        }

        private void SetState(ThemeParts c)
        {
            //IsDesignMode and licesning did not work for me
            if (!Application.ExecutablePath.Contains("DesignToolsServer.exe") && !Application.ExecutablePath.Contains("devenv.exe"))
            {
                VisualStyle currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;
                if (currentTheme != null)
                {
                    var part = Theme.GetGroupBox(currentTheme);
                    var renderer2 = new PartRenderer(currentTheme, part);
                    Glyph = renderer2.RenderPreview(c, Width, Height);
                }
                else
                {
                    Glyph = new Bitmap(Width, Height);
                }
            }
            else
            {
                Glyph = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(Glyph);
                Rectangle rect = new Rectangle(0, 0, Width, Height);
                LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Red, Color.Orange, LinearGradientMode.BackwardDiagonal);
                g.FillRectangle(lBrush, rect);
            }
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Glyph != null)
                e.Graphics.DrawImage(Glyph, new Point(0, 0));
        }
    }
}
