using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.XamlTypeInfo;
using WinUIForms;
using Application = Microsoft.UI.Xaml.Application;

namespace Rectify11Installer;

/// <summary>
/// C# equivalent of App.xaml
/// </summary>
internal class App : Application, IXamlMetadataProvider
{
    private static XamlControlsXamlMetaDataProvider? xamlMetaDataProvider = null;
    private bool _contentLoaded;
    private Window _mainWindow;

    private XamlControlsXamlMetaDataProvider _AppProvider
    {
        get
        {
            if (xamlMetaDataProvider == null)
            {
                xamlMetaDataProvider = new XamlControlsXamlMetaDataProvider();
            }
            return xamlMetaDataProvider;
        }
    }

    public App()
    {
        InitializeComponent();
    }

    public IXamlType GetXamlType(Type type)
    {
        return _AppProvider.GetXamlType(type);
    }

    public IXamlType GetXamlType(string fullName)
    {
        return _AppProvider.GetXamlType(fullName);
    }

    public XmlnsDefinition[] GetXmlnsDefinitions()
    {
        return _AppProvider.GetXmlnsDefinitions();
    }

    public void InitializeComponent()
    {
        if (_contentLoaded)
        {
            return;
        }
        _contentLoaded = true;
        base.DebugSettings.BindingFailed += delegate (object sender, BindingFailedEventArgs args)
        {
            Debug.WriteLine(args.Message);
        };
        base.DebugSettings.XamlResourceReferenceFailed += delegate (DebugSettings sender, XamlResourceReferenceFailedEventArgs args)
        {
            Debug.WriteLine(args.Message);
        };
        base.UnhandledException += delegate
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        };
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        this.Resources.MergedDictionaries.Add(new XamlControlsResources());

        Form tmp = new FrmWizard();
        _mainWindow = tmp.ToWinUI(new MicaBackdrop());
        _mainWindow.Activate();
    }
}
