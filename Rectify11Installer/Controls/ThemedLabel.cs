using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Vanara.Interop;

namespace AeroWizard
{
	[DefaultProperty("Text")]
	[ToolboxItem(true)]
	public sealed class ThemedLabel : Label
	{
		public ThemedLabel()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			BackColor = Color.Transparent;
		}

		[DefaultValue(typeof(Color), "Transparent")]
		public override Color BackColor
		{
			get => base.BackColor;
			set => base.BackColor = value;
		}

		[DefaultValue(null)]
		public new System.Drawing.Image Image
		{
			get => base.Image;
			set
			{
				base.Image = value;
				ImageIndex = -1;
				ImageList = null;
			}
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			var preferredSize = base.GetPreferredSize(proposedSize);
			if (Text.Length > 0)
			{
				preferredSize.Width += 10;
			}

			return preferredSize;
		}

		internal static Rectangle DeflateRect(Rectangle rect, Padding padding)
		{
			rect.X += padding.Left;
			rect.Y += padding.Top;
			rect.Width -= padding.Horizontal;
			rect.Height -= padding.Vertical;
			return rect;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (!Visible)
			{
				return;
			}

			using (NativeMethods.SafeThemeHandle hTheme = new(NativeMethods.OpenThemeData(Handle, "Window")))
			{
				if (!hTheme.IsInvalid && (Application.RenderWithVisualStyles || Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled()))
				{
					using NativeMethods.SafeDCHandle hdc = new(e.Graphics);
					NativeMethods.DrawThemeParentBackground(Handle, hdc, ClientRectangle);
				}
				var r = ThemedLabel.DeflateRect(ClientRectangle, Padding);
				NativeMethods.RECT rR = r;
				if (Image != null)
				{
					if (ImageList != null && ImageIndex == 0)
					{
						if (!hTheme.IsInvalid && !IsDesignMode(this) && Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
						{
							VisualStyleRendererExtension.DrawWrapper(e.Graphics, r, g => NativeMethods.DrawThemeIcon(hTheme, g, 1, 1, ref rR, ImageList.Handle, ImageIndex));
						}
						else
						{
							ImageList.Draw(e.Graphics, r.X, r.Y, r.Width, r.Height, ImageIndex);
						}
					}
					else if (!hTheme.IsInvalid && !IsDesignMode(this) && Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
					{
						VisualStyleRendererExtension.DrawWrapper(e.Graphics, r, g => Graphics.FromHdc(g.DangerousGetHandle()).DrawImage(Image, r));
					}
					else
					{
						e.Graphics.DrawImage(Image, r);
					}
				}
				if (Text.Length <= 0)
				{
					return;
				}

				if (IsDesignMode(this) || hTheme.IsInvalid || !Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
				{
					var brush = Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled() ? SystemBrushes.ActiveCaptionText : SystemBrushes.ControlText;
					var format = new StringFormat(StringFormat.GenericDefault);
					if (GetRightToLeftProperty(this) == RightToLeft.Yes)
					{
						format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					}

					e.Graphics.DrawString(Text, Font, brush, ClientRectangle, format);
				}
				else
				{
					var tff = CreateTextFormatFlags(RtlTranslateAlignment(TextAlign), AutoEllipsis, UseMnemonic);
					VisualStyleRendererExtension.DrawWrapper(e.Graphics, ClientRectangle, g =>
					{
						using (new NativeMethods.SafeDCObjectHandle(g, Font.ToHfont()))
						{
							var pOptions = new NativeMethods.DrawThemeTextOptions(true)
							{
								GlowSize = 10,
								AntiAliasedAlpha = true,
								TextColor = ForeColor
							};
							var pRect = new NativeMethods.RECT(4, 0, Width - 4, Height);
							NativeMethods.DrawThemeTextEx(hTheme, g, 1, 1, Text, Text.Length, tff, ref pRect, ref pOptions);
						}
					});
				}
			}
		}
		public RightToLeft GetRightToLeftProperty(Control ctrl)
		{
			if (ctrl.RightToLeft == RightToLeft.Inherit)
			{
				return GetRightToLeftProperty(ctrl.Parent);
			}

			return ctrl.RightToLeft;
		}
		private bool IsDesignMode(Control ctrl)
		{
			var p = ctrl;
			while (p is not null)
			{
				var site = p.Site;
				if (site is not null && site.DesignMode)
				{
					return true;
				}

				p = p.Parent;
			}
			return false;
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			base.WndProc(ref m);
			if (m.Msg != 132)
			{
				return;
			}

			m.Result = new IntPtr(-1);
		}

		private TextFormatFlags CreateTextFormatFlags(
		  System.Drawing.ContentAlignment textAlign,
		  bool showEllipsis,
		  bool useMnemonic)
		{
			var textFormatFlags = TextFormatFlags.SingleLine;
			if ((textAlign & (System.Drawing.ContentAlignment.BottomLeft | System.Drawing.ContentAlignment.BottomCenter | System.Drawing.ContentAlignment.BottomRight)) != 0)
			{
				textFormatFlags |= TextFormatFlags.Bottom;
			}

			if ((textAlign & (System.Drawing.ContentAlignment.MiddleLeft | System.Drawing.ContentAlignment.MiddleCenter | System.Drawing.ContentAlignment.MiddleRight)) != 0)
			{
				textFormatFlags |= TextFormatFlags.VerticalCenter;
			}

			if ((textAlign & (System.Drawing.ContentAlignment.TopRight | System.Drawing.ContentAlignment.MiddleRight | System.Drawing.ContentAlignment.BottomRight)) != 0)
			{
				textFormatFlags |= TextFormatFlags.Right;
			}

			if ((textAlign & (System.Drawing.ContentAlignment.TopCenter | System.Drawing.ContentAlignment.MiddleCenter | System.Drawing.ContentAlignment.BottomCenter)) != 0)
			{
				textFormatFlags |= TextFormatFlags.HorizontalCenter;
			}

			if (showEllipsis)
			{
				textFormatFlags |= TextFormatFlags.EndEllipsis;
			}

			if (GetRightToLeftProperty(this) == RightToLeft.Yes)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}

			if (!useMnemonic)
			{
				return textFormatFlags | TextFormatFlags.NoPrefix;
			}

			if (!ShowKeyboardCues)
			{
				textFormatFlags |= TextFormatFlags.HidePrefix;
			}

			return textFormatFlags;
		}
	}
}
