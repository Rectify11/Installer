using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUIForms.Interface;

namespace WinUIForms.Internal;

/// <summary>
/// PreControlConverter which will add a solid app background if a MenuStrip is present inside a Form's controls
/// </summary>
internal class AddBackgroundIfMenuStripPresentInForm : PreControlConverter
{
    public override void Invoke(Window w, Grid g, Form f)
    {
        if (f.Controls.OfType<MenuStrip>() != null)
        {
            Microsoft.UI.Xaml.Shapes.Rectangle rect = new();
            rect.Margin = new(0, 40, 0, 0);
            if (Microsoft.UI.Xaml.Application.Current.Resources.TryGetValue("ApplicationPageBackgroundThemeBrush", out var brush))
                rect.Fill = (SolidColorBrush)brush;
            g.Children.Add(rect);
        }
    }
}
