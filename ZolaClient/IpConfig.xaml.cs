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
using System.Windows.Shapes;

namespace ZolaClient
{
    /// <summary>
    /// Interaction logic for IpConfig.xaml
    /// </summary>
    public partial class IpConfig : Window
    {
        public string NewIP { get; set; }

        public IpConfig()
        {
            InitializeComponent();
            this.txtIP.Text = App.IP;
            NewIP = App.IP;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            NewIP = txtIP.Text;
        }
    }
}
