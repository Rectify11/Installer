using Microsoft.UI.Xaml;
using WinUIForms.Helpers;
using WinUIForms.Interface;
using MUXCBtn = Microsoft.UI.Xaml.Controls.Button;
using WFBtn = System.Windows.Forms.Button;

namespace WinUIForms.Internal;

internal class ButtonConverter : ControlConverter
{
    public ButtonConverter() : base(nameof(ButtonConverter), typeof(WFBtn), typeof(MUXCBtn)) { }

    public override object Invoke(Window parent, object inp)
    {
        WFBtn ibtn = (WFBtn)inp;
        MUXCBtn obtn = (MUXCBtn)ConverterUtil.ConvertBaseControl(parent, typeof(MUXCBtn), ibtn);

        obtn.Content = ibtn.Text;
        obtn.Click += (s, e) =>
        {
            ibtn.PerformClick();
        };

        return obtn;
    }
}
