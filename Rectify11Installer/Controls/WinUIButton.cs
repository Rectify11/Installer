using libmsstyle;
using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Rectify11Installer.Controls
{
	public enum ButtonState
	{
		Normal = 1,
		Hot,
		Pressed,
		Disabled,
		Focused
	}
	public sealed class WinUIButton : Control
	{
		private string _ButtonText = "";
		[Browsable(false), DesignerSerializationVisibility(
							DesignerSerializationVisibility.Hidden)]
		public new string Text { get; set; }

		private ButtonState CurrentState = ButtonState.Normal;
		private const int DTT_COMPOSITED = 8192;
		private const int DTT_GLOWSIZE = 2048;
		private const int DTT_TEXTCOLOR = 1;
		public string ButtonText
		{
			get { return _ButtonText; }
			set
			{
				_ButtonText = value;
				InvalidateEx();
			}
		}
		public WinUIButton()
		{
			SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			BackColor = Color.Transparent;
		}
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (Enabled)
			{
				CurrentState = ButtonState.Normal;
			}
			else
			{
				CurrentState = ButtonState.Disabled;
			}

			InvalidateEx();
		}
		protected override CreateParams CreateParams
		{
			get
			{
				var parms = base.CreateParams;
				parms.ExStyle |= 0x20;
				return parms;
			}
		}
		protected override unsafe void OnPaint(PaintEventArgs args)
		{
			Bitmap buttonImage = null;

			//Hack to fix black borders, but we lose transpancy
			args.Graphics.Clear(Theme.IsUsingDarkMode ? Color.White : Color.Black);

			var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;

			//Update text borders
			if (Theme.IsUsingDarkMode)
			{
				ForeColor = Color.White;
			}
			else
			{
				ForeColor = Color.Black;
			}

			var tpart = ThemeParts.Defaulted;
			switch (CurrentState)
			{
				case ButtonState.Normal:
					tpart = ThemeParts.Normal;
					break;
				case ButtonState.Disabled:
					tpart = ThemeParts.Disabled;
					break;
				case ButtonState.Hot:
					tpart = ThemeParts.Hot;
					break;
				case ButtonState.Pressed:
					tpart = ThemeParts.Pressed;
					break;
				case ButtonState.Focused:
					break;
				default:
					tpart = ThemeParts.Normal;
					break;
			}
			if (DesignMode)
			{
				buttonImage = new Bitmap(Width, Height);

				var g = Graphics.FromImage(buttonImage);
				var rectt = new Rectangle(0, 0, Width, Height);
				var lBrush = new LinearGradientBrush(rectt, Color.Red, Color.Orange, LinearGradientMode.BackwardDiagonal);
				g.FillRectangle(lBrush, rectt);
			}
			else
			{
				var part = Theme.GetButtonPart(currentTheme);
				var renderer2 = new PartRenderer(currentTheme, part);
				buttonImage = renderer2.RenderPreview(tpart, Width, Height);
			}

			if (buttonImage == null)
			{
				return;
			}

			//TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
			//TextRenderer.DrawText(args.Graphics, _ButtonText, Font, new Point(Width + 3, this.Height / 2), ForeColor, flags);

			var hdc = args.Graphics.GetHdc();
			VisualStyleRenderer renderer = new(VisualStyleElement.Window.Caption.Active);

			var memoryHdc = NativeMethods.CreateCompatibleDC(hdc);

			var bounds = ClientRectangle;

			// Create a device-independent bitmap and select it into our DC
			NativeMethods.BITMAPINFO info = new();
			info.biSize = Marshal.SizeOf(info);
			info.biWidth = buttonImage.Width;
			info.biHeight = -buttonImage.Height;
			info.biPlanes = 1;
			info.biBitCount = 32;
			info.biCompression = 0; // BI_RGB
			var dib = NativeMethods.CreateDIBSection(memoryHdc, info, 0, out var pixels, IntPtr.Zero, 0);
			for (var y = 0; y < buttonImage.Height; y++)
			{
				for (var x = 0; x < buttonImage.Width; x++)
				{
					var pixel = buttonImage.GetPixel(x, y);
					var d = Theme.IsUsingDarkMode ? Color.Black : Color.White;

					*pixels++ = pixel.ToArgb();
				}
			}

			NativeMethods.SelectObject(memoryHdc, dib);

			var rect = new RECT(0, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
			var opt = new NativeMethods.DTTOPTS
			{
				dwSize = Marshal.SizeOf<NativeMethods.DTTOPTS>(),
				crText = ColorTranslator.ToWin32(ForeColor),
				dwFlags = DTT_TEXTCOLOR | DTT_COMPOSITED
			};

			NativeMethods.SelectObject(memoryHdc, Font.ToHfont());

			var h = NativeMethods.DrawThemeTextEx(renderer.Handle, memoryHdc, NativeMethods.WP_CAPTION, NativeMethods.CS_ACTIVE, ButtonText, -1, (int)(TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine), ref rect, ref opt);

			const int SRCCOPY = 0x00CC0020;
			NativeMethods.BitBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, SRCCOPY);
			//NativeMethods.TransparentBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, bounds.Width, bounds.Height, (uint)Color.Magenta.ToArgb());

			args.Graphics.ReleaseHdc(hdc);
			base.OnPaint(args);

		}
		public void PerformClick()
		{
			if (CanSelect)
			{
				base.OnClick(EventArgs.Empty);
			}
		}
		protected override void OnDoubleClick(EventArgs e)
		{
			if (CurrentState == ButtonState.Pressed)
			{
				Focus();
				PerformClick();
			}
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			if (!Enabled)
			{
				return;
			}

			CurrentState = ButtonState.Hot;
			InvalidateEx();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			if (!Enabled)
			{
				return;
			}

			CurrentState = ButtonState.Normal;
			InvalidateEx();

		}

		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			base.OnMouseDown(mevent);

			if (!Enabled)
			{
				return;
			}

			CurrentState = ButtonState.Pressed;
			InvalidateEx();
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);

			if (!Enabled)
			{
				return;
			}

			CurrentState = ButtonState.Hot;
			InvalidateEx();
		}

		private void InvalidateEx()
		{
			if (Parent == null)
			{
				return;
			}

			Invalidate();
			//Rectangle rc = new(this.Location, this.Size);
			//Parent.Invalidate(rc, true);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
	}
}