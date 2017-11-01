using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using ZolaClient.Helpers;

namespace ZolaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            txtUsername.Focus();
            AvatarHelper.InitAvatarDirectory();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow chatWindow = null;
            try
            {
                chatWindow = new MainWindow();
                App.Connect(chatWindow);
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                if (App.Proxy.Login(username, password))
                {
                    MessageBox.Show("Login Success");
                    ZolaService.User user = App.Proxy.GetUserInformation(username);
                    chatWindow.Init(user);
                    this.Hide();
                    chatWindow.ShowDialog();
                    App.Proxy.Logout(user);
                    chatWindow.Close();
                }
                else
                {
                    MessageBox.Show("Login Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                App.Disconnect();
                if(chatWindow != null)
                {
                    chatWindow.Hide();
                    chatWindow.Close();
                }
                this.Show();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register registerDialog = new Register();
            this.Hide();

            if (registerDialog.ShowDialog() == true)
            {
                MessageBox.Show("Register complete!");
            }
            else
            {
                //this.Show();
            }
            registerDialog.Close();
            this.Show();
        }

        private void ctmIPConfig_Click(object sender, RoutedEventArgs e)
        {
            IpConfig ipConfigDialog = new IpConfig();
            if (ipConfigDialog.ShowDialog() == true)
            {
                App.IP = ipConfigDialog.NewIP;
            }
            ipConfigDialog.Close();
            MessageBox.Show("new ip: " + App.IP);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Disconnect();
            //MessageBox.Show("Closing");
            //e.Cancel = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
