using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
    public class DarkAwareTreeView : TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);

        protected override void CreateHandle()
        {
            base.CreateHandle();

            if (Theme.IsUsingDarkMode)
            {
                SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
            }
            else
            {
                SetWindowTheme(this.Handle, "explorer", null);
            }
        }
    }
    public class DarkAwareLabel : Label
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            if (Theme.IsUsingDarkMode)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
        }
    }
    public class DarkAwareRichTextBox : RichTextBox
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            if (Theme.IsUsingDarkMode)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
        }
    }

    public class DarkAwareTabPage : TabPage
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            if (Theme.IsUsingDarkMode)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
        }
    }
}
