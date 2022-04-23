using Rectify11Installer.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rectify11InstallerWPF.Controls
{
    /// <summary>
    /// Interaction logic for RButton.xaml
    /// </summary>
    public partial class RButton : UserControl
    {
        public RButton()
        {
            InitializeComponent();
            host.Child = new WinUIButton();
        }
    }
}
