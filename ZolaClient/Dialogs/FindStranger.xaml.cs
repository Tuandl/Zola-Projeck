using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for FindStranger.xaml
    /// </summary>
    public partial class FindStranger : Window
    {
        private ObservableCollection<ListViewItem> _displayUsers = new ObservableCollection<ListViewItem>();
        private ZolaService.User _curUser;
        private BackgroundWorker _findStrangerWorker = null;
        private List<BackgroundWorker> _findStrangerAvatarWorker = new List<BackgroundWorker>();

        public FindStranger(ZolaService.User curUser)
        {
            InitializeComponent();
            this._curUser = curUser;
            lvStrangers.ItemsSource = _displayUsers;
            txtStrangerUsername.Focus();
        }

        #region private methods
        /// <summary>
        /// Send request to server to find stranger Async
        /// If last request does not completed
        /// It will be cancle
        /// </summary>
        /// <param name="strangerUsername"></param>
        private void MakeFindStrangerRequest(string strangerUsername)
        {
            _displayUsers.Clear();
            if (_findStrangerWorker != null && _findStrangerWorker.IsBusy)
            {
                _findStrangerWorker.CancelAsync();
            }
            foreach (BackgroundWorker avatarWorker in _findStrangerAvatarWorker)
            {
                if (avatarWorker.IsBusy)
                {
                    avatarWorker.CancelAsync();
                }
            }
            _findStrangerAvatarWorker.Clear();

            _findStrangerWorker = new BackgroundWorker();
            _findStrangerWorker.WorkerSupportsCancellation = true;
            _findStrangerWorker.WorkerReportsProgress = false;
            _findStrangerWorker.DoWork += FindStrangerWorker_DoWork;
            _findStrangerWorker.RunWorkerCompleted += FindStrangerWorker_RunWorkerCompleted;

            _findStrangerWorker.RunWorkerAsync(strangerUsername);
        }

        #endregion

        #region Avatar Worker
        private void AvatarWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string username = e.Result.ToString();
            if (AvatarHelper.GetAvatarPath(username) == null)
                return;
            DisplayUser user = _displayUsers.ToList().Find(x => (x.Content as DisplayUser).Username == username).Content as DisplayUser;
            user.AvatarUrl = null;
            user.AvatarUrl = AvatarHelper.GetAvatarPath(username);
        }

        private void AvatarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ZolaService.DataFile avatar = null;
            string username = e.Argument.ToString();
            if (App.Proxy.IsUserHasAvatar(username))
            {
                avatar = App.Proxy.GetAvatarFile(username);
                AvatarHelper.SaveAvatar(username, avatar);
            }
            e.Result = username;
        }
        #endregion

        #region FindStranger Worker
        private void FindStrangerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string strangerUsername = e.Argument.ToString();
            List<ZolaService.User> strangers = App.Proxy.FindStranger(_curUser.Username, strangerUsername);
            e.Result = strangers;
        }

        private void FindStrangerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<ZolaService.User> strangers = e.Result as List<ZolaService.User>;
            foreach (ZolaService.User stranger in strangers)
            {
                DisplayUser displayStranger = new DisplayUser()
                {
                    AvatarUrl = AvatarHelper.DefaultAvatarPath,
                    Name = stranger.Name,
                    Username = stranger.Username,
                    IsOnline = stranger.IsOnline
                };
                ListViewItem item = new ListViewItem();
                item.Content = displayStranger;
                item.ContentTemplate = (DataTemplate)this.FindResource("StrangerItem");
                _displayUsers.Add(item);

                BackgroundWorker avatarWorker = new BackgroundWorker();
                avatarWorker.WorkerSupportsCancellation = true;
                avatarWorker.WorkerReportsProgress = false;
                avatarWorker.DoWork += AvatarWorker_DoWork;
                avatarWorker.RunWorkerCompleted += AvatarWorker_RunWorkerCompleted;
                _findStrangerAvatarWorker.Add(avatarWorker);
                avatarWorker.RunWorkerAsync(stranger.Username);
            }
        }
        #endregion

        #region Evens
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DisplayUser strangerUser = button.DataContext as DisplayUser;
            App.Proxy.SendFriendRequestCompleted += Proxy_SendFriendRequestCompleted;
            App.Proxy.SendFriendRequestAsync(_curUser.Username, strangerUser.Username);
        }

        private void Proxy_SendFriendRequestCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MakeFindStrangerRequest(txtStrangerUsername.Text);
            //MessageBox.Show("Request is pending...");
            NotificationHelper.NotifyInfo("Request is pending...");
        }

        private void txtStrangerUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeFindStrangerRequest(txtStrangerUsername.Text);
        }
        #endregion

        private void Test()
        {
            List<ListViewItem> friends = new List<ListViewItem>();

            for (int i = 0; i < 10; i++)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = Environment.CurrentDirectory + @"\Resources\img\avatar-default.png",
                    Name = "Tuan",
                    Username = "tmp_" + i
                };
                if (i % 2 == 0)
                {
                    user.IsOnline = true;
                }
                else user.IsOnline = false;
                ListViewItem item = new ListViewItem();
                item.Content = user;
                item.ContentTemplate = (DataTemplate)this.FindResource("StrangerItem");
                friends.Add(item);
            }
            lvStrangers.ItemsSource = friends;
        }
    }

    #region Converter
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            string uri = value as string;
            if (uri != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(uri);
                image.EndInit();
                result = image;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    #endregion
}
