using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Controls
{
    public class DarkRichTextBox : RichTextBox
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            FrmWizard.SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
        }
    }
}
