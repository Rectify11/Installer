using Microsoft.UI.Xaml;
using System;

namespace WinUIForms.Interface;

public abstract class ControlConverter
{
    public string Name { get; private set; }
    public Type InputWinFormsType { get; private set; }
    public Type OutputWinUIType { get; private set; }

    public ControlConverter(string name, Type inp, Type outp)
    {
        // Do some checks
        if (inp == null)
            throw new ArgumentNullException(nameof(inp));
        if (outp == null)
            throw new ArgumentNullException(nameof(outp));

        // Then, assign properties
        Name = name;
        InputWinFormsType = inp;
        OutputWinUIType = outp;
    }

    public virtual object Invoke(Window parent, object inp)
    {
        return null;
    }
}
