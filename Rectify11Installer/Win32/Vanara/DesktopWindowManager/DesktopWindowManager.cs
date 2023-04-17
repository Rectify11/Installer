using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace Vanara.Interop.DesktopWindowManager
{
	[SecuritySafeCritical]
	public static class DesktopWindowManager
	{
		private static readonly object _lock = new();
		private static Vanara.Interop.DesktopWindowManager.DesktopWindowManager.MessageWindow _window;
		private static readonly object ColorizationColorChangedKey = new();
		private static readonly object CompositionChangedKey = new();
		private static readonly object NonClientRenderingChangedKey = new();
		private static EventHandlerList eventHandlerList;
		private static readonly object[] keys = new object[3]
		{
			Vanara.Interop.DesktopWindowManager.DesktopWindowManager.CompositionChangedKey,
			Vanara.Interop.DesktopWindowManager.DesktopWindowManager.NonClientRenderingChangedKey,
			Vanara.Interop.DesktopWindowManager.DesktopWindowManager.ColorizationColorChangedKey
		};

		public static event EventHandler ColorizationColorChanged
		{
			add => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.AddEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.ColorizationColorChangedKey, value);
			remove => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.RemoveEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.ColorizationColorChangedKey, value);
		}

		public static event EventHandler CompositionChanged
		{
			add => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.AddEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.CompositionChangedKey, value);
			remove => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.RemoveEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.CompositionChangedKey, value);
		}

		public static event EventHandler NonClientRenderingChanged
		{
			add => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.AddEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.NonClientRenderingChangedKey, value);
			remove => Vanara.Interop.DesktopWindowManager.DesktopWindowManager.RemoveEventHandler(Vanara.Interop.DesktopWindowManager.DesktopWindowManager.NonClientRenderingChangedKey, value);
		}

		public static bool CompositionSupported => Environment.OSVersion.Version.Major >= 6;

		public static void ExtendFrameIntoClientArea(this IWin32Window window, Padding padding)
		{
			if (window == null)
				throw new ArgumentNullException(nameof(window));
			var pMarInset = new Vanara.Interop.NativeMethods.Margins(padding);
			Vanara.Interop.NativeMethods.DwmExtendFrameIntoClientArea(window.Handle, ref pMarInset);
		}

		public static bool IsCompositionEnabled()
		{
			if (!Vanara.Interop.DesktopWindowManager.DesktopWindowManager.CompositionSupported || !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "dwmapi.dll")))
				return false;
			var pfEnabled = 0;
			Vanara.Interop.NativeMethods.DwmIsCompositionEnabled(ref pfEnabled);
			return pfEnabled != 0;
		}

		private static void AddEventHandler(object id, EventHandler value)
		{
			lock (Vanara.Interop.DesktopWindowManager.DesktopWindowManager._lock)
			{
				if (Vanara.Interop.DesktopWindowManager.DesktopWindowManager._window == null)
					Vanara.Interop.DesktopWindowManager.DesktopWindowManager._window = new Vanara.Interop.DesktopWindowManager.DesktopWindowManager.MessageWindow();
				if (Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList == null)
					Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList = new EventHandlerList();
				Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList.AddHandler(id, (Delegate)value);
			}
		}

		private static void RemoveEventHandler(object id, EventHandler value)
		{
			lock (Vanara.Interop.DesktopWindowManager.DesktopWindowManager._lock)
			{
				if (Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList == null)
					return;
				Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList.RemoveHandler(id, (Delegate)value);
			}
		}

		[SecuritySafeCritical]
		private sealed class MessageWindow : NativeWindow, IDisposable
		{
			private const int WM_DWMCOLORIZATIONCOLORCHANGED = 800;
			private const int WM_DWMCOMPOSITIONCHANGED = 798;
			private const int WM_DWMNCRENDERINGCHANGED = 799;

			public MessageWindow()
			{
				var cp = new CreateParams()
				{
					Style = 0,
					ExStyle = 0,
					ClassStyle = 0,
					Parent = IntPtr.Zero
				};
				cp.Caption = this.GetType().Name;
				this.CreateHandle(cp);
			}

			public void Dispose() => this.DestroyHandle();

			protected override void WndProc(ref System.Windows.Forms.Message m)
			{
				if (m.Msg >= 798 && m.Msg <= 800)
					this.ExecuteEvents(m.Msg - 798);
				base.WndProc(ref m);
			}

			private void ExecuteEvents(int idx)
			{
				if (Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList == null)
					return;
				lock (Vanara.Interop.DesktopWindowManager.DesktopWindowManager._lock)
				{
					try
					{
						var eventHandler = (EventHandler)Vanara.Interop.DesktopWindowManager.DesktopWindowManager.eventHandlerList[Vanara.Interop.DesktopWindowManager.DesktopWindowManager.keys[idx]];
						if (eventHandler == null)
							return;
						eventHandler((object)null, EventArgs.Empty);
					}
					catch
					{
					}
				}
			}
		}
	}
}
