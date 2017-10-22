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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;
            string name = txtName.Text;
            if(password != confirmPassword)
            {
                MessageBox.Show("Password confirm not match!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            } else
            {
                App.Connect();
                if(App.Proxy.Register(username, password, name) == true)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
                App.Disconnect();
            }
        }
    }
}
