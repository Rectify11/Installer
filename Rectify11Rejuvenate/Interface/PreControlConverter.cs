using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUIForms.Interface;

/// <summary>
/// PreControlConverters allow WinUIForms & apps to add controls before WinUIForms will convert controls
/// </summary>
public class PreControlConverter
{
    public virtual void Invoke(Window w, Grid g, Form f)
    {

    }
}
