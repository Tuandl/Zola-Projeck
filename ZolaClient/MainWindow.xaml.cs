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
using ZolaClient.ZolaService;
using System.ServiceModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ZolaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class MainWindow : Window, ZolaService.IChatServiceCallback
    {
        private ZolaService.User _curUser = null;
        private List<ZolaService.User> _friends = null;
        private ObservableCollection<ListViewItem> _displayFriends = null;

        

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init(ZolaService.User user)
        {
            this._curUser = user;
            InitUser();
            InitFriends();
            InitFriendsAvatar();
        }

        #region private methods

        private void InitUser()
        {
            txtblCurUserName.Text = _curUser.Name;
            AvatarHelper.LoadAvatarFromServer(imgCurUserAvatar, _curUser.Username);
        }

        private void InitFriends()
        {
            _displayFriends = new ObservableCollection<ListViewItem>();
            _friends = App.Proxy.GetFriends(_curUser.Username);
            foreach (ZolaService.User friend in _friends)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = AvatarHelper.DefaultAvatarPath,
                    Name = friend.Name,
                    Username = friend.Username,
                    IsOnline = friend.IsOnline
                };
                //string avatarPath = AvatarHelper.GetAvatarPath(friend.Username);
                //if (avatarPath != null)
                //{
                //    user.AvatarUrl = avatarPath;
                //}
                ListViewItem item = new ListViewItem();
                item.Content = user;
                if (user.IsOnline)
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("OnlineTemplate");
                }
                else
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
                }

                _displayFriends.Add(item);
            }
            lvFriends.ItemsSource = _displayFriends;
        }

        private void InitFriendsAvatar()
        {
            foreach (ZolaService.User friend in _friends)
            {
                BackgroundWorker findFriendAvatar = new BackgroundWorker();
                findFriendAvatar.WorkerReportsProgress = false;
                findFriendAvatar.WorkerSupportsCancellation = true;
                findFriendAvatar.DoWork += FindFriendAvatar_DoWork;
                findFriendAvatar.RunWorkerCompleted += FindFriendAvatar_RunWorkerCompleted;
                findFriendAvatar.RunWorkerAsync(friend.Username);
            }
        }


        private void TestTemplate()
        {
            List<ListViewItem> friends = new List<ListViewItem>();

            for (int i = 0; i < 10; i++)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = Environment.CurrentDirectory + @"\Resources\img\avatar-default.png",
                    Name = "Tuan",
                    Username = "tmp"
                };
                if (i % 2 == 0)
                {
                    user.IsOnline = true;
                }
                else user.IsOnline = false;
                ListViewItem item = new ListViewItem();
                item.Content = user;
                if (user.IsOnline)
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("OnlineTemplate");
                }
                else
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
                }
                friends.Add(item);
            }
            lvFriends.ItemsSource = friends;

            List<ListViewItem> messages = new List<ListViewItem>();
            for (int i = 0; i < 20; i++)
            {
                DisplayMessage message = new DisplayMessage()
                {
                    AvatarUrl = Environment.CurrentDirectory + @"\Resources\img\avatar-default.png",
                    MessageContent = "Hello"
                };
                ListViewItem item = new ListViewItem();
                item.Content = message;
                if (i % 2 == 0)
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("MyMessageTemplate");
                }
                else
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("FriendMessageTemplate");
                }
                messages.Add(item);
            }
            lvChatMessages.ItemsSource = messages;
        }



        #endregion

        #region Callback methods
        public void FriendOnline(User onlineFriend)
        {
            ZolaService.User friend = _friends.Find(x => x.Username == onlineFriend.Username);
            friend.IsOnline = true;
            ListViewItem item = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == onlineFriend.Username);
            item.ContentTemplate = (DataTemplate)this.FindResource("OnlineTemplate");
            //MessageBox.Show("complete online change");

        }

        public void FriendOffline(User offlineFriend)
        {
            ZolaService.User friend = _friends.Find(x => x.Username == offlineFriend.Username);
            friend.IsOnline = true;
            ListViewItem item = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == offlineFriend.Username);
            item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
            //MessageBox.Show("complete online change");
        }

        public void FriendOnlineListChangeUnexpectedly()
        {
            InitFriends();
        }

        public bool ReceiveMessage(DataMessage message)
        {
            throw new NotImplementedException();
        }

        public void FriendChangeAvatar(User friend)
        {
            BackgroundWorker findFriendAvatar = new BackgroundWorker();
            findFriendAvatar.WorkerReportsProgress = false;
            findFriendAvatar.WorkerSupportsCancellation = true;
            findFriendAvatar.DoWork += FindFriendAvatar_DoWork;
            findFriendAvatar.RunWorkerCompleted += FindFriendAvatar_RunWorkerCompleted;
            findFriendAvatar.RunWorkerAsync(friend.Username);
        }

        public void FriendIsWrittingMessage(User Friend)
        {
            throw new NotImplementedException();
        }
        public void ReceiveMakeFriendRequest(User stranger)
        {
            throw new NotImplementedException();
        }
        public void GotANewFriend()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Asyn callback methods

        public IAsyncResult BeginFriendOnline(User onlineFriend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOnline(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        public IAsyncResult BeginFriendOnlineListChangeUnexpectedly(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOnlineListChangeUnexpectedly(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        public IAsyncResult BeginReceiveMessage(DataMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public bool EndReceiveMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendChangeAvatar(User friend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendChangeAvatar(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendIsWrittingMessage(User Friend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendIsWrittingMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        public IAsyncResult BeginReceiveMakeFriendRequest(User stranger, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndReceiveMakeFriendRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }


        public IAsyncResult BeginGotANewFriend(AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndGotANewFriend(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginFriendOffline(User offlineFriend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendOffline(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Load Friend Avatar worker
        private void FindFriendAvatar_DoWork(object sender, DoWorkEventArgs e)
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

        private void FindFriendAvatar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string username = e.Result.ToString();
            DisplayUser user = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == username).Content as DisplayUser;
            user.AvatarUrl = null;
            user.AvatarUrl = AvatarHelper.GetAvatarPath(username);
        }
        #endregion

        #region Even Process
        private void btnUpdateInformation_Click(object sender, RoutedEventArgs e)
        {
            Dialogs.UpdateInformation updateInformationDialog = new Dialogs.UpdateInformation(_curUser);
            //this.Hide();
            try
            {
                updateInformationDialog.ShowDialog();
                InitUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            updateInformationDialog.Close();
            //this.Show();
        }

        private void txtSearchStranger_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this._curUser = null;
            this._friends.Clear();
            this._friends = null;
            this._displayFriends.Clear();
            this._displayFriends = null;
        }

        /// <summary>
        /// Select cur chat user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFriends_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = (ListViewItem)(sender as ListView).SelectedItem;
            if (item != null)
            {
                DisplayUser user = item.Content as DisplayUser;
                txtblNameCurFriend.Text = user.Name;
                txtblUsernameCurFriend.Text = user.Username;
                AvatarHelper.LoadAvatarFromLocal(imgCurFriend, user.Username);
            }
        }
        #endregion

    }

    #region Display Classed

    class DisplayUser : INotifyPropertyChanged
    {
        private string _avatarUrl;
        private string _name;
        private string _username;

        public string AvatarUrl
        {
            get { return this._avatarUrl; }
            set
            {
                if (this._avatarUrl != value)
                {
                    this._avatarUrl = value;
                    NotifyPropertyChanged("AvatarUrl");
                }
            }
        }
        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        public string Username
        {
            get { return this._username; }
            set
            {
                if (value != this._username)
                {
                    this._username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }
        public bool IsOnline { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    class DisplayMessage : INotifyPropertyChanged
    {
        private string _avatarUrl;
        private string _content;

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                if (_avatarUrl != value)
                {
                    _avatarUrl = value;
                    NotifyPropertyChanged("AvatarUrl");
                }
            }
        }
        public string MessageContent
        {
            get { return _content; }
            set
            {
                if (value != _content)
                {
                    _content = value;
                    NotifyPropertyChanged("MessageContent");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

    #endregion

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
