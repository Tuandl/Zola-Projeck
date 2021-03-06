﻿using System;
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
using ZolaClient.Helpers;

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
            txtUsername.Focus();
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

            //Validate
            if (username.Length <= 2)
            {
                //MessageBox.Show("Username must have more than 2 chars", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                NotificationHelper.NotifyError("Username must have more than 2 chars");
            }
            else if (password.Length < 5)
            {
                //MessageBox.Show("Password must have more than 4 chars", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                NotificationHelper.NotifyError("Password must have more than 4 chars");
            }
            else if (name.Length < 1)
            {
                //MessageBox.Show("Please input name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                NotificationHelper.NotifyError("Please input name");
            }
            else if (password != confirmPassword)
            {
                //MessageBox.Show("Password confirm not match!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                NotificationHelper.NotifyError("Password confirm not match!");
            }
            else
            {
                App.Connect(new CallbackObject());
                try
                {
                    if (App.Proxy.Register(username, password, name) == true)
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.DialogResult = false;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    NotificationHelper.NotifyError(ex.Message);
                }
                finally
                {
                    try
                    {
                        App.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        NotificationHelper.NotifyError(ex.Message);
                    }
                }

            }
        }
    }
}
