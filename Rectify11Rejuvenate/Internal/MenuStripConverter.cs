using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using System.Windows.Forms;
using WinUIForms.Helpers;
using WinUIForms.Interface;

namespace WinUIForms.Internal;

internal class MenuStripConverter : ControlConverter
{
    public MenuStripConverter() : base(nameof(MenuStripConverter), typeof(MenuStrip), typeof(MenuBar)) { }

    private void ParseContextMenuStrip(ToolStripMenuItem item, MenuBarItem target)
    {
        if (item.DropDownItems.Count == 0)
            return;

        foreach (ToolStripMenuItem ddi in item.DropDownItems)
        {
            Debug.WriteLine(Name + ": Found ToolStripMenuItem");
            MenuFlyoutItem i = new();
            i.Text = ddi.Text;
            i.Click += (s, e) => ddi.PerformClick();
            target.Items.Add(i);
        }
    }

    public override object Invoke(Window parent, object inp)
    {
        MenuStrip input = (MenuStrip)inp;
        MenuBar outp = (MenuBar)ConverterUtil.ConvertBaseControl(parent, typeof(MenuBar), input, true);

        outp.Margin = new(0, 0, 0, 0);
        outp.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
        outp.Height = 40;
        foreach (ToolStripMenuItem item in input.Items)
        {
            MenuBarItem t = new();
            t.Title = item.Text;
            ParseContextMenuStrip(item, t);
            outp.Items.Add(t);
        }

        return outp;
    }
}
