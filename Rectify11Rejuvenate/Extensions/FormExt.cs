#define USE_FASTCTRLCCDICT

using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Vanara.PInvoke;
using WinRT;
using WinUIForms.Helpers;
using WinUIForms.Interface;
using WinUIForms.Internal;

namespace System.Windows.Forms; // Put it in default WinForms namespace so the user can have it directly

public static class FormExt
{
    #region Window conversion
    private static void LoadIcon(Window w, string iconName)
    {
        // Since WinUI 3 doesn't have a Window.Icon property, we will use P/Invoke to set the icon
        var hwnd = w.As<IWindowNative>().WindowHandle;
        nint hIcon = User32.LoadImage(nint.Zero, iconName,
                  User32.LoadImageType.IMAGE_ICON, 16, 16, User32.LoadImageOptions.LR_LOADFROMFILE);

        User32.SendMessage(hwnd, User32.WindowMessage.WM_SETICON, (nint)0, hIcon);
    }

    /// <summary>
    ///  Move & resize an AppWindow to reflect the window bounds and start position.
    ///  
    ///  Original implementation: https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms/src/System/Windows/Forms/Form.cs#L3492
    /// </summary>
    private static void MoveAppWindowStartPosition(Form f, AppWindow aw)
    {
        switch (f.StartPosition)
        {
            case FormStartPosition.WindowsDefaultBounds:
                goto case FormStartPosition.WindowsDefaultLocation;
                break;
            case FormStartPosition.WindowsDefaultLocation:
                aw.Move(new(50, 50));
                break;
            case FormStartPosition.Manual:
                break;
            case FormStartPosition.CenterParent:
                goto case FormStartPosition.CenterScreen;
            case FormStartPosition.CenterScreen:
                Screen desktop = Screen.FromPoint(Cursor.Position);
                System.Drawing.Rectangle screenRect = desktop.WorkingArea;

                // if, we're maximized, then don't set the x & y coordinates (they're @ (0,0) )
                if (f.WindowState != FormWindowState.Maximized)
                    aw.Move(new(Math.Max(screenRect.X, screenRect.X + (screenRect.Width - f.Width) / 2), Math.Max(screenRect.Y, screenRect.Y + (screenRect.Height - f.Height) / 2)));

                break;
        }
    }

    private static Window ConvertToWindow(Form f, SystemBackdrop bd)
    {
        var icp = System.IO.Path.GetTempFileName();
        var fs = File.Open(icp, FileMode.OpenOrCreate);
        f.Icon.Save(fs);
        fs.Close();

        Window w = new();
        var whwnd = WinRT.Interop.WindowNative.GetWindowHandle(w);
        w.Title = f.Text;
        w.AppWindow.MoveAndResize(new(f.Left, f.Top, f.Size.Width, f.Size.Height));
        MoveAppWindowStartPosition(f, w.AppWindow);
        switch (f.WindowState)
        {
            case FormWindowState.Maximized:
                User32.ShowWindow(whwnd, ShowWindowCommand.SW_MAXIMIZE);
                break;
            case FormWindowState.Minimized:
                User32.ShowWindow(whwnd, ShowWindowCommand.SW_MINIMIZE);
                break;
            case FormWindowState.Normal:
                // Do nothing, since it's "normal" by default
                break;
        }

        w.SystemBackdrop = bd;

        if (bd != null)
        {
            if (bd.GetType() == typeof(MicaBackdrop))
            {
                DWMUtil.SystemBackdrop bdr = DWMUtil.SystemBackdrop.MicaRegular;

                if (((MicaBackdrop)bd).Kind == Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt)
                    bdr = DWMUtil.SystemBackdrop.MicaAlt;

                DWMUtil.SetSystemBackdrop(whwnd, bdr);
            }
            else if (bd.GetType() == typeof(DesktopAcrylicBackdrop))
                DWMUtil.SetSystemBackdrop(whwnd, DWMUtil.SystemBackdrop.AcrylicRegular);
        }

        LoadIcon(w, icp);

        // Use a ThemeListener to change the window frame theme
        ThemeListener lt = new(DispatcherQueue.GetForCurrentThread());
        DWMUtil.EnableImmersiveDarkMode(whwnd, lt.CurrentTheme == ApplicationTheme.Dark);
        lt.ThemeChanged += new((s) => DWMUtil.EnableImmersiveDarkMode(whwnd, lt.CurrentTheme == ApplicationTheme.Dark));
        return w;
    }
    #endregion

    public static List<ControlConverter> RegisteredControlConverters { get; private set; } = new();
    public static List<PreControlConverter> RegisteredPreControlConverters { get; private set; } = new();

    static FormExt()
    {
        RegisteredPreControlConverters.Add(new AddBackgroundIfMenuStripPresentInForm());
        RegisteredControlConverters.Add(new ButtonConverter());
        RegisteredControlConverters.Add(new MenuStripConverter());
    }

    private static void InvokePreControlConverters(Window w, Grid g, Form f)
    {
        foreach (PreControlConverter p in RegisteredPreControlConverters)
            p.Invoke(w, g, f);
    }

    private static Window Convert(Form f, SystemBackdrop bd)
    {
        bool FoundConverter = false;
        int ConvertedControls = 0;
        int TotalControls = 0;
        UIElement currentElement = null;
        List<Type> UnsupportedControls = new();
#if USE_FASTCTRLCCDICT
        Dictionary<Type, ControlConverter> FastCtrlCCDict = new();
#endif

        Window w = ConvertToWindow(f, bd);
        Grid appContentGrid = new();

        InvokePreControlConverters(w, appContentGrid, f);

        foreach (Control c in f.Controls)
        {
            if (c.Tag != null)
                if (c.Tag == "WinUIForms_Ignore")
                    continue;

            TotalControls++;

            // Skip to the next control if it's in the unsupported controls list.
            if (UnsupportedControls.Contains(c.GetType()))
            {
                Debug.WriteLine("The type has been found in the 'UnsupportedControls' list, skip the control");
                continue;
            }

#if USE_FASTCTRLCCDICT
            if (FastCtrlCCDict.ContainsKey(c.GetType()))
            {
                Debug.WriteLine("Found control's type in FastCtrlCCDict!");
                currentElement = (UIElement)FastCtrlCCDict[c.GetType()].Invoke(w, c);
                appContentGrid.Children.Add(currentElement);
                FoundConverter = true;
                ConvertedControls++;
            }
            else
            {
#endif
                Debug.WriteLine("Didn't found control's type in FastCtrlCCDict! Trying searching for the correct converter.");
                foreach (ControlConverter cc in RegisteredControlConverters)
                {
                    Debug.WriteLine($"Trying converter: {cc.GetType().FullName}\n\tInput type: {cc.InputWinFormsType.FullName}\n\tOutput type: {cc.OutputWinUIType.FullName}");
                    if (cc.InputWinFormsType == c.GetType())
                    {
                        Debug.WriteLine("Found the correct converter. Converting now.");
                        currentElement = (UIElement)cc.Invoke(w, c);
                        appContentGrid.Children.Add(currentElement);
                        FoundConverter = true;
#if USE_FASTCTRLCCDICT
                        FastCtrlCCDict[c.GetType()] = cc;
#endif
                        ConvertedControls++;
                    }
                }
#if USE_FASTCTRLCCDICT
            }
#endif
            if (!FoundConverter)
            {
                Debug.WriteLine($"Didn't found any converter for {c.GetType().FullName} (located in {c.GetType().Assembly.FullName})");
                UnsupportedControls.Add(c.GetType());


            }
            else
                Debug.WriteLine($"Successfully converted {c.GetType().FullName} into {currentElement.GetType()}.");
        }

        Debug.WriteLine($"Finished converting controls! TotalCtrls: {TotalControls}, ConvertedCtrls: {ConvertedControls}, UnconvertedCtrls: {TotalControls - ConvertedControls}");

        w.Content = appContentGrid;

        return w;
    }

    /// <summary>
    /// Convert a form into a WinUI window
    /// </summary>
    /// <param name="bd">The system backdrop to apply (Can be null)</param>
    /// <returns>The new window</returns>
    public static Window ToWinUI(this Form f, SystemBackdrop bd = null)
    {
        // Todo: make some caching system, for faster loading
        Debug.WriteLine("WinForms -> WinUI converter called.");
        Window w = Convert(f, bd);
        return w;
    }
}
