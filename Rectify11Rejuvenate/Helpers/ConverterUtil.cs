using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinUIForms.Helpers;

internal class ConverterUtil
{
    public static object ConvertBaseControl(Window parent, Type outCtrlType, Control inp, bool dontSetSize = false)
    {
        if (outCtrlType == null)
            throw new ArgumentNullException(nameof(outCtrlType));

        object? outCtrl = Activator.CreateInstance(outCtrlType);

        if (outCtrl == null)
            throw new Exception("System.Reflection.Activator.CreateInstance returned null!");

        FrameworkElement outCtrl2 = outCtrl as FrameworkElement;

        if (!dontSetSize)
        {
            outCtrl2.Width = inp.Width;
            outCtrl2.Height = inp.Height;
        }

        if (inp.Dock == DockStyle.None)
        {
            bool ASTopPresent = false;
            bool ASLeftPresent = false;
            Thickness margin = new();

            // Get all individual enum values from the combined value
            foreach (AnchorStyles value in GetIndividualEnumValues(inp.Anchor))
            {
                if (value == AnchorStyles.Top)
                {
                    ASTopPresent = true;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Top;
                }
                if (value == AnchorStyles.Bottom)
                {
                    if (ASTopPresent)
                    {
                        outCtrl2.VerticalAlignment = VerticalAlignment.Stretch;
                    }
                    outCtrl2.VerticalAlignment = VerticalAlignment.Top;
                }

                if (value == AnchorStyles.Left)
                {
                    ASLeftPresent = true;
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Left;
                }
                if (value == AnchorStyles.Right)
                {
                    if (ASLeftPresent)
                    {
                        outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
                    }
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Right;
                }
            }

            switch (outCtrl2.HorizontalAlignment)
            {
                case Microsoft.UI.Xaml.HorizontalAlignment.Left:
                    margin.Left = inp.Left;
                    break;
                case Microsoft.UI.Xaml.HorizontalAlignment.Right:
                    margin.Right = inp.Right;
                    break;
                case Microsoft.UI.Xaml.HorizontalAlignment.Stretch | Microsoft.UI.Xaml.HorizontalAlignment.Center:
                    margin.Left = inp.Left;
                    margin.Right = inp.Right;
                    break;
            }

            switch (outCtrl2.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    margin.Top = inp.Top;
                    break;
                case VerticalAlignment.Bottom:
                    margin.Bottom = inp.Bottom;
                    break;
                case VerticalAlignment.Stretch | VerticalAlignment.Center:
                    margin.Top = inp.Top;
                    margin.Bottom = inp.Bottom;
                    break;
            }

            outCtrl2.Margin = margin;
        }
        else
        {
            //throw new NotImplementedException("TODO: DockStyle");
            switch (inp.Dock)
            {
                case DockStyle.Top:
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Top;
                    //outCtrl2.Width = parent.AppWindow.ClientSize.Width;
                    break;
                case DockStyle.Left:
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Left;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Stretch;
                    //outCtrl2.Height = parent.AppWindow.ClientSize.Height;
                    break;
                case DockStyle.Bottom:
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Bottom;
                    //outCtrl2.Width = parent.AppWindow.ClientSize.Width;
                    break;
                case DockStyle.Right:
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Right;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Stretch;
                    //outCtrl2.Height = parent.AppWindow.ClientSize.Height;
                    break;
                case DockStyle.Fill:
                    outCtrl2.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
                    outCtrl2.VerticalAlignment = VerticalAlignment.Stretch;
                    //outCtrl2.Width = parent.AppWindow.ClientSize.Width;
                    //outCtrl2.Height = parent.AppWindow.ClientSize.Height;
                    break;
            }
        }

        return outCtrl;
    }

    private static IEnumerable<AnchorStyles> GetIndividualEnumValues(AnchorStyles combinedValue)
    {
        foreach (AnchorStyles enumValue in Enum.GetValues(typeof(AnchorStyles)))
        {
            if (combinedValue.HasFlag(enumValue))
            {
                yield return enumValue;
            }
        }
    }
}
