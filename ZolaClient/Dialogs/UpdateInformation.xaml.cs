using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ZolaClient.Dialogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UpdateInformation : Window
    {
        private OpenFileDialog openFile = new OpenFileDialog();
        private ZolaService.User _curUser;

        public UpdateInformation(ZolaService.User user)
        {
            InitializeComponent();

            this._curUser = user;
            this.txtUsername.Text = user.Username;
            this.txtName.Text = user.Name;
            if (_curUser.IsMale == true)
            {
                cbMale.IsChecked = true;
            }
            if (_curUser.IsMale == false)
            {
                cbFemale.IsChecked = true;
            }
            string avatarPath = AvatarHelper.GetAvatarPath(user.Username);
            if (avatarPath != null)
            {
                imgAvatar.Source = new BitmapImage(new Uri(avatarPath, UriKind.Absolute));
            }
            
            openFile.Filter = "Image Files (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFile.Multiselect = false;
            btnUpdateImage.IsEnabled = false;
        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            bool? isMale = null;
            if (cbMale.IsChecked == true)
            {
                isMale = true;
            }
            if (cbFemale.IsChecked == true)
            {
                isMale = false;
            }

            //validate
            if (isMale == null)
            {
                MessageBox.Show("Please select a gender");
                return;
            }
            if (name.Length < 1)
            {
                MessageBox.Show("Please Input name");
                return;
            }

            _curUser.Name = name;
            _curUser.IsMale = isMale;
            if (App.Proxy.UpdateInformation(_curUser) == true)
            {
                MessageBox.Show("Update Complete");
            }
            else
            {
                MessageBox.Show("Update False");
            }
        }

        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string oldPass = txtOldPassword.Password;
            string newPass = txtNewPassword.Password;
            string confirmPass = txtConfirmPassword.Password;
            if (oldPass.Length == 0 || newPass.Length == 0 || confirmPass.Length == 0)
            {
                MessageBox.Show("Please fill the above form");
                return;
            }
            if (newPass != confirmPass)
            {
                MessageBox.Show("Confirm does not match");
                return;
            }
            if (App.Proxy.UpdatePassword(_curUser.Username, oldPass, newPass) == true)
            {
                MessageBox.Show("Change password success!!");
            }
            else
            {
                MessageBox.Show("Old password does not match!");
            }
        }

        private void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            if (openFile.ShowDialog() == true)
            {
                string imgUrl = openFile.FileName;
                imgAvatar.Source = new BitmapImage(new Uri(imgUrl, UriKind.Absolute));
                btnUpdateImage.IsEnabled = true;
            }
        }

        private void btnUpdateImage_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = new FileInfo(openFile.FileName);
            ZolaService.DataFile newAvatar = new ZolaService.DataFile()
            {
                FileName = file.Name,
                Data = File.ReadAllBytes(file.FullName)
            };
            if (App.Proxy.UpdateAvatar(_curUser.Username, newAvatar))
            {
                MessageBox.Show("update avatar complete");
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
