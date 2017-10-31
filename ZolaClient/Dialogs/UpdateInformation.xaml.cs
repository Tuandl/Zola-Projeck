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
using ZolaClient.Helpers;

namespace ZolaClient.Dialogs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UpdateInformation : Window
    {
        private ZolaService.User _curUser;

        public UpdateInformation(ZolaService.User user)
        {
            InitializeComponent();
            this._curUser = user;
            this.txtUsername.Text = user.Username;
            this.txtName.Text = user.Name;
            if(_curUser.IsMale == true)
            {
                cbMale.IsChecked = true;
            }
            if(_curUser.IsMale == false)
            {
                cbFemale.IsChecked = true;
            }
            string avatarPath = AvatarHelper.GetAvatarPath(user.Username);
            if(avatarPath != null)
            {
                imgAvatar.Source = new BitmapImage(new Uri(avatarPath, UriKind.Absolute));
            }
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
            if(name.Length < 1)
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
    }
}
