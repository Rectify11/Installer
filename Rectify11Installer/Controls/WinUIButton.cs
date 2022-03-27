using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Controls
{
    public class WinUIButton : Button
    {
        private string _ButtonText="";
        [Browsable(false), DesignerSerializationVisibility(
                            DesignerSerializationVisibility.Hidden)]
        public new string Text { get; set; } = "";

        private ButtonState CurrentState = ButtonState.Normal;

        public string ButtonText
        {
            get
            {
                return _ButtonText;
            }
            set
            {
                _ButtonText = value;
                InvalidateEx();
            }
        }
        protected override void OnPaint(PaintEventArgs args)
        {
            Image? buttonImage = null;
            if (!Enabled)
            {
                CurrentState = ButtonState.Disabled;
            }

            //Hack to fix black borders, but we lose transpancy
            args.Graphics.Clear(Theme.IsUsingDarkMode ? Color.Black : Color.White);

            if (Theme.IsUsingDarkMode)
            {
                ForeColor = Color.White;
                switch (CurrentState)
                {
                    case ButtonState.Normal:
                        buttonImage = Properties.Resources.Button_Dark_Normal_96;
                        break;
                    case ButtonState.Disabled:
                        buttonImage = Properties.Resources.Button_Dark_Disabled_96;
                        break;
                    case ButtonState.Hover:
                        buttonImage = Properties.Resources.Button_Dark_Hot_96;
                        break;
                    case ButtonState.HoverClicked:
                        buttonImage = Properties.Resources.Button_Dark_Pressed_96;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //light
                ForeColor = Color.Black;

                switch (CurrentState)
                {
                    case ButtonState.Normal:
                        buttonImage = Properties.Resources.Button_LightMode_Normal_96;
                        break;
                    case ButtonState.Disabled:
                        buttonImage = Properties.Resources.Button_LightMode_Disabled_96;
                        break;
                    case ButtonState.Hover:
                        buttonImage = Properties.Resources.Button_LightMode_Hot_96;
                        break;
                    case ButtonState.HoverClicked:
                        buttonImage = Properties.Resources.Button_LightMode_Pressed_96;
                        break;
                    default:
                        break;
                }
            }

            if (buttonImage == null)
            {
                return;
            }

            args.Graphics.DrawImage(buttonImage, 0, 0, Width, Height);

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(args.Graphics, _ButtonText, Font, new Point(Width + 3, this.Height / 2), ForeColor, flags);

           
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!Enabled)
            {
                return;
            }

            CurrentState = ButtonState.Hover;
            InvalidateEx();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);

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

            CurrentState = ButtonState.HoverClicked;
            InvalidateEx();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            if (!Enabled)
            {
                return;
            }

            CurrentState = ButtonState.Normal;
            InvalidateEx();
        }

        private void InvalidateEx()
        {
            if (Parent == null)
                return;

            Rectangle rc = new(this.Location, this.Size);

            Parent.Invalidate(rc, true);
        }
        enum ButtonState
        {
            Normal,
            Disabled,
            Hover,
            HoverClicked
        }

        public WinUIButton()
        {
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ExStyle |= 0x20;
                return parms;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
           
        }
    }
}
