using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
    public class DarkAwareTreeView : TreeView
    {
        private const int WM_LBUTTONDBLCLK = 0x0203;
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);
        // hack to disable artifacts
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; //WS_EX_COMPOSITED
                return cp;
            }
        }
        public DarkAwareTreeView()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                var info = this.HitTest(PointToClient(Cursor.Position));
                if (info.Location == TreeViewHitTestLocations.StateImage)
                {
                    m.Result = IntPtr.Zero;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
            {
                SetWindowTheme(Handle, "DarkMode_Explorer", null);
                ForeColor = Color.White;
                BackColor = Color.Black;
            }
            else
            {
                SetWindowTheme(Handle, "explorer", null);
                ForeColor = Color.Black;
                BackColor = Color.White;
            }
        }
    }
    public class DarkAwareLabel : Label
    {
        public DarkAwareLabel()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
            BackColor = Color.Transparent;
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
                ForeColor = Color.White;
            else
                ForeColor = Color.Black;
        }
    }
    public class DarkAwareRichTextBox : RichTextBox
    {
        public DarkAwareRichTextBox()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
            }
        }
    }

    public class DarkAwareTabPage : TabPage
    {
        public DarkAwareTabPage()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
            }
        }
    }
    public class DarkAwareRadioButton : RadioButton
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);
        public DarkAwareRadioButton()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
            {
                SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
                ForeColor = Color.White;
            }
            else
            {
                SetWindowTheme(this.Handle, "Explorer", null);
                ForeColor = Color.Black;
            }
        }
    }
    public class DarkAwareCheckBox : CheckBox
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);
        public DarkAwareCheckBox()
        {
            Theme.OnThemeChanged += delegate (object sender, EventArgs e)
            {
                UpdateTheming();
            };
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            UpdateTheming();
        }
        private void UpdateTheming()
        {
            if (Theme.IsUsingDarkMode)
            {
                SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
                ForeColor = Color.White;
            }
            else
            {
                SetWindowTheme(this.Handle, "Explorer", null);
                ForeColor = Color.Black;
            }
        }
    }

}
