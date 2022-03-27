using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32UIDemo
{
    public partial class CommandLinkButton : Button
    {
        private const int BS_COMMANDLINK = 0x0000000E;
        private string note = null;
        protected override CreateParams CreateParams
        {
            get
            {
                var cParams = base.CreateParams;
                cParams.Style |= BS_COMMANDLINK;
                return cParams;
            }
        }
        [Category("Appearance"), Browsable(true), DefaultValue((string)null)]
        public string Note
        {
            get { return note; }
            set
            {
                const uint BCM_SETNOTE = 0x1609;
                note = value;
                SendMessage(Handle, BCM_SETNOTE, 0, note);
                Invalidate();
            }
        }

        public CommandLinkButton()
        {
            this.FlatStyle = FlatStyle.System;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    }
}
