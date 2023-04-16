using Rectify11Installer.Core;
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
		private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
		// hack to disable artifacts
		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				cp.ExStyle |= 0x02000000; //WS_EX_COMPOSITED
				return cp;
			}
		}
		public DarkAwareTreeView()
		{
			Theme.OnThemeChanged += delegate
			{
				UpdateTheming();
			};
		}
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_LBUTTONDBLCLK)
			{
				var info = HitTest(PointToClient(Cursor.Position));
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
			Theme.OnThemeChanged += delegate
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
	public class DarkAwareRichTextBox : RichTextBox
	{
		public DarkAwareRichTextBox()
		{
			Theme.OnThemeChanged += delegate
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
			Theme.OnThemeChanged += delegate
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
		private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
		public DarkAwareRadioButton()
		{
			Theme.OnThemeChanged += delegate
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
				SetWindowTheme(Handle, "DarkMode_Explorer", null);
				ForeColor = Color.White;
			}
			else
			{
				SetWindowTheme(Handle, "Explorer", null);
				ForeColor = Color.Black;
			}
		}
	}
	public class DarkAwareCheckBox : CheckBox
	{
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
		public DarkAwareCheckBox()
		{
			Theme.OnThemeChanged += delegate
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
				SetWindowTheme(Handle, "DarkMode_Explorer", null);
				ForeColor = Color.White;
			}
			else
			{
				SetWindowTheme(Handle, "Explorer", null);
				ForeColor = Color.Black;
			}
		}
	}
	public class DarkAwareComboBox : ComboBox
	{
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
		public DarkAwareComboBox()
		{
			Theme.OnThemeChanged += delegate
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
				SetWindowTheme(Handle, "DarkMode_CFD", null);
				ForeColor = Color.White;
				BackColor = Color.Black;
			}
			else
			{
				SetWindowTheme(Handle, "CFD", null);
				ForeColor = Color.Black;
				BackColor = Color.White;
			}
		}
	}

}
