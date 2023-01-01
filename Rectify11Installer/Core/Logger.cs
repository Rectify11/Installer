using System;
using System.Reflection;
#nullable enable

namespace Rectify11Installer.Core
{
	public class Logger
	{
		private static string Text = "═════════════════════════\nSTART: " + DateTime.Now.ToString() + "\nRectify11 Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n═════════════════════════\n";
		public static void WriteLine(string s)
		{
			Text += s + "\n";
		}
		public static void WriteLine(string s, Exception ex)
		{
			Text += s + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;
		}
		public static void CommitLog()
		{
			System.IO.File.WriteAllText(System.IO.Path.Combine(Variables.r11Folder, "installer.log"), Text);
		}

		public static void Warn(string v)
		{
			WriteLine("[WARNING] " + v);
		}
		public static void Warn(string v, Exception ex)
		{
			WriteLine("[WARNING] " + v, ex);
		}
	}
}
