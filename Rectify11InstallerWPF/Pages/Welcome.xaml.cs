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
using WPFUI.Controls.Interfaces;

namespace Rectify11InstallerWPF.Pages
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page, INavigationItem
    {
        public Welcome()
        {
            InitializeComponent();
        }

        public bool IsValid => true;

        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Page Instance { get => this;set { throw new NotImplementedException(); } }
        public Type Page { get => this.GetType(); set => throw new NotImplementedException(); }

        public event RoutedEventHandler Click;
    }
}
