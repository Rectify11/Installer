using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    namespace FunnyExplorer.Controls
    {
        public class NavigationButton : Control
        {
            private VisualStyleRenderer? renderer = null;
            public VisualStyleElement Element { get; set; } = VisualStyleElementEx.Navigation.BackButton.Normal;
            private NavigationButtonType t = NavigationButtonType.forward;
            public NavigationButtonType NavigationButtonType
            {
                get { return t; }
                set
                {
                    t = value;
                    if (Enabled)
                    {
                        this.SetState(ButtonStateEx.Normal);
                        this.Invalidate();
                    }
                    else
                    {
                        this.SetState(ButtonStateEx.Disabled);
                        this.Invalidate();
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
                        this.SetState(ButtonStateEx.Normal);
                    }
                    else
                    {
                        this.SetState(ButtonStateEx.Disabled);
                    }
                    this.Invalidate();
                }
            }
            public NavigationButton()
            {
                this.Location = new Point(50, 50);
                this.Size = new Size(30, 30);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.BackColor = Color.Transparent;
                //this.MaximumSize = new Size(30, 30);
                SetElement(Element);
                SetState(ButtonStateEx.Normal);
                this.MouseEnter += NavigationButton_MouseEnter;
                this.MouseLeave += NavigationButton_MouseLeave;

                this.MouseDown += NavigationButton_MouseDown;
                this.MouseUp += NavigationButton_MouseUp;

                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

            private void NavigationButton_MouseUp(object? sender, MouseEventArgs e)
            {
                if (Enabled)
                {
                    SetState(ButtonStateEx.Hover);
                }
            }

            private void NavigationButton_MouseDown(object? sender, MouseEventArgs e)
            {
                if (Enabled)
                {
                    SetState(ButtonStateEx.Clicked);
                }
            }

            private void NavigationButton_Click(object? sender, EventArgs e)
            {
                
            }

            private void NavigationButton_MouseLeave(object? sender, EventArgs e)
            {
                if (Enabled)
                {
                    SetState(ButtonStateEx.Normal);
                }
            }

            private void NavigationButton_MouseEnter(object? sender, EventArgs e)
            {
                if (Enabled)
                {
                    SetState(ButtonStateEx.Hover);
                }

            }

            private void SetElement(VisualStyleElement e)
            {
                this.Element = e;
                try
                {
                    if (VisualStyleRenderer.IsElementDefined(e))
                    {
                        renderer = new VisualStyleRenderer(e);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Debug.WriteLine("Failed to render the button, falling back...");

                }
                this.Invalidate();
            }
            private void SetState(ButtonStateEx d)
            {
                if (NavigationButtonType == NavigationButtonType.back)
                {
                    switch (d)
                    {
                        case ButtonStateEx.Normal:
                            SetElement(VisualStyleElementEx.Navigation.BackButton.Normal);
                            break;
                        case ButtonStateEx.Hover:
                            SetElement(VisualStyleElementEx.Navigation.BackButton.Hot);
                            break;
                        case ButtonStateEx.Clicked:
                            SetElement(VisualStyleElementEx.Navigation.BackButton.Pressed);
                            break;
                        case ButtonStateEx.Disabled:
                            SetElement(VisualStyleElementEx.Navigation.BackButton.Disabled);
                            break;
                        default:
                            break;
                    }
                }
                else if (NavigationButtonType == NavigationButtonType.forward)
                {
                    switch (d)
                    {
                        case ButtonStateEx.Normal:
                            SetElement(VisualStyleElementEx.Navigation.ForwardButton.Normal);
                            break;
                        case ButtonStateEx.Hover:
                            SetElement(VisualStyleElementEx.Navigation.ForwardButton.Hot);
                            break;
                        case ButtonStateEx.Clicked:
                            SetElement(VisualStyleElementEx.Navigation.ForwardButton.Pressed);
                            break;
                        case ButtonStateEx.Disabled:
                            SetElement(VisualStyleElementEx.Navigation.ForwardButton.Disabled);
                            break;
                        default:
                            break;
                    }
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                // Draw the element if the renderer has been set.
                try
                {
                    if (renderer != null)
                        renderer.DrawBackground(e.Graphics, this.ClientRectangle);
                    else
                        throw new Exception();
                }
                catch
                {
                    e.Graphics.DrawRectangle(Pens.Black, this.ClientRectangle);
                    if (NavigationButtonType == NavigationButtonType.back)
                        e.Graphics.DrawString("<-", SystemFonts.DefaultFont, new SolidBrush(Color.Black), this.Width / 2, this.Height / 2);
                    else
                        e.Graphics.DrawString("->", SystemFonts.DefaultFont, new SolidBrush(Color.Black), this.Width / 2, this.Height / 2);

                }
            }
        }
        public enum NavigationButtonType { forward, back }
        public enum ButtonStateEx { Normal, Hover, Clicked, Disabled }
    }

    /// <summary>Identifies a control or user interface (UI) element that is drawn with visual styles.</summary>
    public static class VisualStyleElementEx
    {
        /// <summary>
        /// Contains classes that provide <see cref="VisualStyleElement"/> objects for AeroWizard-related controls. This class cannot be inherited.
        /// </summary>
        public static class AeroWizard
        {
            private const string className = "AEROWIZARD";

            /// <summary>Provides a <see cref="VisualStyleElement"/> for the button of a wizard. This class cannot be inherited.</summary>
            public static class Button
            {
                /// <summary>Gets a visual style element that represents a button in a wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a button in a wizard.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, 5, 0);
            }

            /// <summary>Provides a <see cref="VisualStyleElement"/> for the command area of a wizard. This class cannot be inherited.</summary>
            public static class CommandArea
            {
                /// <summary>Gets a visual style element that represents the command area of a wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the command area of a wizard.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, 4, 0);
            }

            /// <summary>Provides a <see cref="VisualStyleElement"/> for the content area of a wizard. This class cannot be inherited.</summary>
            public static class ContentArea
            {
                /// <summary>Gets a visual style element that represents the content area of a wizard without a margin.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the content area of a wizard without a margin.</value>
                public static VisualStyleElement NoMargin => VisualStyleElement.CreateElement(className, 3, 1);

                /// <summary>Gets a visual style element that represents the content area of a wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the content area of a wizard.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, 3, 0);
            }

            /// <summary>Provides a <see cref="VisualStyleElement"/> for the header area of a wizard. This class cannot be inherited.</summary>
            public static class HeaderArea
            {
                /// <summary>Gets a visual style element that represents the header area of a wizard without a margin.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the header area of a wizard without a margin.</value>
                public static VisualStyleElement NoMargin => VisualStyleElement.CreateElement(className, 2, 1);

                /// <summary>Gets a visual style element that represents the header area of a wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the header area of a wizard.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, 2, 0);
            }

            /// <summary>
            /// Provides a <see cref="VisualStyleElement"/> for each state of the titlebar of a wizard. This class cannot be inherited.
            /// </summary>
            public static class TitleBar
            {
                /// <summary>Gets a visual style element that represents the titlebar of an active wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the titlebar of an active wizard.</value>
                public static VisualStyleElement Active => VisualStyleElement.CreateElement(className, 1, 1);

                /// <summary>Gets a visual style element that represents the titlebar of an inactive wizard.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents the titlebar of an inactive wizard.</value>
                public static VisualStyleElement Inactive => VisualStyleElement.CreateElement(className, 1, 2);
            }
        }

        /// <summary>
        /// Contains classes that provide <see cref="VisualStyleElement"/> objects for navigation-related controls. This class cannot be inherited.
        /// </summary>
        public static class Navigation
        {
            private const string className = "NAVIGATION";

            /// <summary>
            /// Provides <see cref="VisualStyleElement"/> objects for the different states of the Back Button control. This class cannot be inherited.
            /// </summary>
            public static class BackButton
            {
                private const int part = 1;

                /// <summary>Gets a visual style element that represents a back button in the disabled state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a back button in the disabled state.</value>
                public static VisualStyleElement Disabled => VisualStyleElement.CreateElement(className, part, 4);

                /// <summary>Gets a visual style element that represents a back button in the hot state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a back button in the hot state.</value>
                public static VisualStyleElement Hot => VisualStyleElement.CreateElement(className, part, 2);

                /// <summary>Gets a visual style element that represents a back button in the normal state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a back button in the normal state.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, part, 1);

                /// <summary>Gets a visual style element that represents a back button in the pressed state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a back button in the pressed state.</value>
                public static VisualStyleElement Pressed => VisualStyleElement.CreateElement(className, part, 3);
            }

            /// <summary>
            /// Provides <see cref="VisualStyleElement"/> objects for the different states of the Forward Button control. This class cannot
            /// be inherited.
            /// </summary>
            public static class ForwardButton
            {
                private const int part = 2;

                /// <summary>Gets a visual style element that represents a forward button in the disabled state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a forward button in the disabled state.</value>
                public static VisualStyleElement Disabled => VisualStyleElement.CreateElement(className, part, 4);

                /// <summary>Gets a visual style element that represents a forward button in the hot state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a forward button in the hot state.</value>
                public static VisualStyleElement Hot => VisualStyleElement.CreateElement(className, part, 2);

                /// <summary>Gets a visual style element that represents a forward button in the normal state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a forward button in the normal state.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, part, 1);

                /// <summary>Gets a visual style element that represents a forward button in the pressed state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a forward button in the pressed state.</value>
                public static VisualStyleElement Pressed => VisualStyleElement.CreateElement(className, part, 3);
            }

            /// <summary>
            /// Provides <see cref="VisualStyleElement"/> objects for the different states of the Menu Button control. This class cannot be inherited.
            /// </summary>
            public static class MenuButton
            {
                private const int part = 3;

                /// <summary>Gets a visual style element that represents a menu button in the disabled state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a menu button in the disabled state.</value>
                public static VisualStyleElement Disabled => VisualStyleElement.CreateElement(className, part, 4);

                /// <summary>Gets a visual style element that represents a menu button in the hot state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a menu button in the hot state.</value>
                public static VisualStyleElement Hot => VisualStyleElement.CreateElement(className, part, 2);

                /// <summary>Gets a visual style element that represents a menu button in the normal state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a menu button in the normal state.</value>
                public static VisualStyleElement Normal => VisualStyleElement.CreateElement(className, part, 1);

                /// <summary>Gets a visual style element that represents a menu button in the pressed state.</summary>
                /// <value>A <see cref="VisualStyleElement"/> that represents a menu button in the pressed state.</value>
                public static VisualStyleElement Pressed => VisualStyleElement.CreateElement(className, part, 3);
            }
        }
    }
}
