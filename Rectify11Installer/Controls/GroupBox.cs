using libmsstyle;
using Rectify11Installer.Core;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
	public sealed partial class GroupBox : Control
	{
		private Bitmap Glyph;

		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				SetState(value ? ThemeParts.Normal : ThemeParts.Disabled);
				this.Invalidate();
			}
		}

		public GroupBox()
		{
			this.Location = new Point(3, 28);
			this.Size = new Size(345, 294);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Theme.IsUsingDarkMode ? Color.Black : Color.White;
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
			//IsDesignMode and licensing did not work for me
			if (!Application.ExecutablePath.Contains("DesignToolsServer.exe") && !Application.ExecutablePath.Contains("devenv.exe"))
			{
				var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;
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
				var g = Graphics.FromImage(Glyph);
				var rect = new Rectangle(0, 0, Width, Height);
				var lBrush = new LinearGradientBrush(rect, Color.Red, Color.Orange, LinearGradientMode.BackwardDiagonal);
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
