using libmsstyle;
using Rectify11Installer.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
	public sealed partial class FakeCommandLink : UserControl
	{
		private static Color DefaultText => Theme.IsUsingDarkMode ? Color.FromArgb(192, 192, 192) : Color.FromArgb(64, 64, 64);

		private static Color HotText => Theme.IsUsingDarkMode ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);

		private static Color PressedText => Theme.IsUsingDarkMode ? Color.FromArgb(160, 160, 160) : Color.FromArgb(96, 96, 96);

		public string Note
		{
			get => lblBody.Text;
			set => lblBody.Text = value;
		}
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get => lblTitle.Text;
			set => lblTitle.Text = value;
		}

		public new EventHandler Click;
		public FakeCommandLink()
		{
			InitializeComponent();
			SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			for (var i = 0; i < Controls.Count; i++)
			{
				Controls[i].MouseEnter += TheMouseEnter;
				Controls[i].MouseLeave += TheMouseLeave;
				Controls[i].MouseDown += TheMouseDown;
				Controls[i].MouseUp += TheMouseUp;
				Controls[i].Click += TheMouseClick;
			}
			this.MouseEnter += TheMouseEnter;
			this.MouseLeave += TheMouseLeave;
			this.MouseDown += TheMouseDown;
			this.MouseUp += TheMouseUp;
			base.Click += TheMouseClick;

			BackColor = Theme.IsUsingDarkMode ? Color.Black : Color.White;

			pictureBox1.BackColor = Color.Transparent;
			lblTitle.BackColor = Color.Transparent;
			lblBody.BackColor = Color.Transparent;

			SetState(ThemeParts.Normal);
			pictureBox1.Image = GetGlyphImage(ThemeParts.Normal);

			lblTitle.ForeColor = DefaultText;
			lblBody.ForeColor = DefaultText;

			SetDoubleBuffered(lblTitle);
			SetDoubleBuffered(lblBody);
			SetDoubleBuffered(pictureBox1);
			Invalidate();
		}
		private void SetState(ThemeParts s)
		{
			//IsDesignMode and licensing did not work for me
			if (Application.ExecutablePath.Contains("DesignToolsServer.exe") ||
				Application.ExecutablePath.Contains("devenv.exe")) return;
			var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;
			if (currentTheme == null) return;
			var part = Theme.GetCommandLinkPart(currentTheme);
			var renderer2 = new PartRenderer(currentTheme, part);
			BackgroundImage = renderer2.RenderPreview(s, Width, Height);
		}
		public static void SetDoubleBuffered(Control c)
		{
			if (SystemInformation.TerminalServerSession)
				return;
			var t = typeof(Control);
			var aProp = t.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			if (aProp != null)
				aProp.SetValue(c, true, null);
		}
		private Bitmap GetGlyphImage(ThemeParts s)
		{
			if (!Application.ExecutablePath.Contains("DesignToolsServer.exe") && !Application.ExecutablePath.Contains("devenv.exe"))
			{
				var currentTheme = Theme.IsUsingDarkMode ? Theme.DarkStyle : Theme.LightStyle;
				if (currentTheme == null) return null;
				var part = Theme.GetCommandLinkGlyphPart(currentTheme);
				var renderer2 = new PartRenderer(currentTheme, part);
				return renderer2.RenderPreview(s, pictureBox1.Width, pictureBox1.Height);
			}
			else
			{
				//var b = new Bitmap(Width, Height);
				//Graphics g = Graphics.FromImage(b);
				//Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
				//LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Blue, Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
				//g.FillRectangle(lBrush, rect);
				//return b;
			}
			return null;
		}

		private void TheMouseClick(object sender, EventArgs e)
		{
			Click?.Invoke(sender, e);
		}

		private void TheMouseUp(object sender, MouseEventArgs e)
		{
			if (!Enabled) return;
			SetState(ThemeParts.Hot);
			pictureBox1.Image = GetGlyphImage(ThemeParts.Hot);
			var forecolor = Theme.IsUsingDarkMode ? DefaultBackColor : Color.Black;
			lblTitle.ForeColor = forecolor;
			lblBody.ForeColor = forecolor;
		}
		private void TheMouseDown(object sender, MouseEventArgs e)
		{
			if (!Enabled) return;
			SetState(ThemeParts.Pressed);
			pictureBox1.Image = GetGlyphImage(ThemeParts.Pressed);
			lblTitle.ForeColor = PressedText;
			lblBody.ForeColor = PressedText;
		}

		private void TheMouseLeave(object sender, EventArgs e)
		{
			SetState(ThemeParts.Normal);
			pictureBox1.Image = GetGlyphImage(ThemeParts.Normal);
			lblTitle.ForeColor = DefaultText;
			lblBody.ForeColor = DefaultText;
		}

		private void TheMouseEnter(object sender, EventArgs e)
		{
			SetState(ThemeParts.Hot);
			pictureBox1.Image = GetGlyphImage(ThemeParts.Hot);
			lblTitle.ForeColor = HotText;
			lblBody.ForeColor = HotText;
		}
	}
}