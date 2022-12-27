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
    public class ThemedLabel : Label
    {
        public ThemedLabel()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
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
                this.ImageIndex = -1;
                this.ImageList = (ImageList)null;
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);
            if (this.Text.Length > 0)
                preferredSize.Width += 10;
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
            if (!this.Visible)
                return;
            using (NativeMethods.SafeThemeHandle hTheme = new NativeMethods.SafeThemeHandle(NativeMethods.OpenThemeData(this.Handle, "Window")))
            {
                if (!hTheme.IsInvalid && (Application.RenderWithVisualStyles || Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled()))
                {
                    using (NativeMethods.SafeDCHandle hdc = new NativeMethods.SafeDCHandle((IDeviceContext)e.Graphics))
                        NativeMethods.DrawThemeParentBackground(this.Handle, hdc, (NativeMethods.PRECT)this.ClientRectangle);
                }
                Rectangle r = ThemedLabel.DeflateRect(this.ClientRectangle, this.Padding);
                NativeMethods.RECT rR = (NativeMethods.RECT)r;
                if (this.Image != null)
                {
                    if (this.ImageList != null && this.ImageIndex == 0)
                    {
                        if (!hTheme.IsInvalid && !IsDesignMode(this) && Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
                            VisualStyleRendererExtension.DrawWrapper((IDeviceContext)e.Graphics, r, (VisualStyleRendererExtension.DrawWrapperMethod)(g => NativeMethods.DrawThemeIcon(hTheme, g, 1, 1, ref rR, this.ImageList.Handle, this.ImageIndex)));
                        else
                            this.ImageList.Draw(e.Graphics, r.X, r.Y, r.Width, r.Height, this.ImageIndex);
                    }
                    else if (!hTheme.IsInvalid && !IsDesignMode(this) && Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
                        VisualStyleRendererExtension.DrawWrapper((IDeviceContext)e.Graphics, r, (VisualStyleRendererExtension.DrawWrapperMethod)(g => Graphics.FromHdc(g.DangerousGetHandle()).DrawImage(this.Image, r)));
                    else
                        e.Graphics.DrawImage(this.Image, r);
                }
                if (this.Text.Length <= 0)
                    return;
                if (IsDesignMode(this) || hTheme.IsInvalid || !Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled())
                {
                    Brush brush = Vanara.Interop.DesktopWindowManager.DesktopWindowManager.IsCompositionEnabled() ? SystemBrushes.ActiveCaptionText : SystemBrushes.ControlText;
                    StringFormat format = new StringFormat(StringFormat.GenericDefault);
                    if (GetRightToLeftProperty(this) == RightToLeft.Yes)
                        format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    e.Graphics.DrawString(this.Text, this.Font, brush, (RectangleF)this.ClientRectangle, format);
                }
                else
                {
                    TextFormatFlags tff = this.CreateTextFormatFlags(this.RtlTranslateAlignment(this.TextAlign), this.AutoEllipsis, this.UseMnemonic);
                    VisualStyleRendererExtension.DrawWrapper((IDeviceContext)e.Graphics, this.ClientRectangle, (VisualStyleRendererExtension.DrawWrapperMethod)(g =>
                    {
                        using (new NativeMethods.SafeDCObjectHandle(g, this.Font.ToHfont()))
                        {
                            NativeMethods.DrawThemeTextOptions pOptions = new NativeMethods.DrawThemeTextOptions(true)
                            {
                                GlowSize = 10,
                                AntiAliasedAlpha = true,
                                TextColor = this.ForeColor
                            };
                            NativeMethods.RECT pRect = new NativeMethods.RECT(4, 0, this.Width - 4, this.Height);
                            NativeMethods.DrawThemeTextEx(hTheme, g, 1, 1, this.Text, this.Text.Length, tff, ref pRect, ref pOptions);
                        }
                    }));
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
            Control p = ctrl;
            while (p is not null)
            {
                System.ComponentModel.ISite site = p.Site;
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
                return;
            m.Result = new IntPtr(-1);
        }

        private TextFormatFlags CreateTextFormatFlags(
          System.Drawing.ContentAlignment textAlign,
          bool showEllipsis,
          bool useMnemonic)
        {
            TextFormatFlags textFormatFlags = TextFormatFlags.SingleLine;
            if ((textAlign & (System.Drawing.ContentAlignment.BottomLeft | System.Drawing.ContentAlignment.BottomCenter | System.Drawing.ContentAlignment.BottomRight)) != (System.Drawing.ContentAlignment)0)
                textFormatFlags |= TextFormatFlags.Bottom;
            if ((textAlign & (System.Drawing.ContentAlignment.MiddleLeft | System.Drawing.ContentAlignment.MiddleCenter | System.Drawing.ContentAlignment.MiddleRight)) != (System.Drawing.ContentAlignment)0)
                textFormatFlags |= TextFormatFlags.VerticalCenter;
            if ((textAlign & (System.Drawing.ContentAlignment.TopRight | System.Drawing.ContentAlignment.MiddleRight | System.Drawing.ContentAlignment.BottomRight)) != (System.Drawing.ContentAlignment)0)
                textFormatFlags |= TextFormatFlags.Right;
            if ((textAlign & (System.Drawing.ContentAlignment.TopCenter | System.Drawing.ContentAlignment.MiddleCenter | System.Drawing.ContentAlignment.BottomCenter)) != (System.Drawing.ContentAlignment)0)
                textFormatFlags |= TextFormatFlags.HorizontalCenter;
            if (showEllipsis)
                textFormatFlags |= TextFormatFlags.EndEllipsis;
            if (GetRightToLeftProperty(this) == RightToLeft.Yes)
                textFormatFlags |= TextFormatFlags.RightToLeft;
            if (!useMnemonic)
                return textFormatFlags | TextFormatFlags.NoPrefix;
            if (!this.ShowKeyboardCues)
                textFormatFlags |= TextFormatFlags.HidePrefix;
            return textFormatFlags;
        }
    }
}
