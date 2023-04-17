using libmsstyle;
using Rectify11Installer.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
	public enum NavigationButtonType { Forward, Backward, Menu }
	public sealed class NavigationButton : Control
	{
		#region Variables
		private NavigationButtonType t;
		public ThemeParts state;
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
				Invalidate();
			}
		}
		#endregion

		public NavigationButton()
		{
			Location = new Point(3, 3);
			Size = new Size(39, 39);
			SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;
			SetState(ThemeParts.Normal);
			BackgroundImageLayout = ImageLayout.Stretch;
		}

		#region Overriden Methods
		public void PerformClick()
		{
			if (CanSelect)
			{
				OnClick(EventArgs.Empty);
			}
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			if (state != ThemeParts.Pressed) return;
			Focus();
			PerformClick();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (Enabled)
			{
				SetState(ThemeParts.Hot);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (Enabled)
			{
				SetState(ThemeParts.Pressed);
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (Enabled)
			{
				SetState(ThemeParts.Normal);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (Enabled)
			{
				SetState(ThemeParts.Hot);
			}

		}
		private void SetState(ThemeParts c)
		{
			state = c;
			//IsDesignMode and licensing did not work for me
			if (!Application.ExecutablePath.Contains("DesignToolsServer.exe") && !Application.ExecutablePath.Contains("devenv.exe"))
			{
				var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;
				if (currentTheme != null)
				{
					var part = Theme.GetNavArrowPart(currentTheme, t);
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
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (Glyph == null) return;
			e.Graphics.DrawImage(Glyph, new Point(0, 0));
		}
		#endregion
	}
}