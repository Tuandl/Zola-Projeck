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
using ZolaClient.Dialogs;

namespace ZolaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class MainWindow : Window, ZolaService.IChatServiceCallback
    {
        private ZolaService.User _curUser = null;
        private List<ZolaService.User> _friends = new List<User>();
        private ObservableCollection<ListViewItem> _displayFriends = null;
        private bool isWritting = false;

        private Dictionary<string, ObservableCollection<ListViewItem>> _displayMessages = new Dictionary<string, ObservableCollection<ListViewItem>>();

        public MainWindow()
        {
            InitializeComponent();
            CollapseAllPanels();
            pnInit.Visibility = Visibility.Visible;
        }

        public void Init(ZolaService.User user)
        {
            this._curUser = user;
            InitUser();
            InitFriends();
            InitFriendsAvatar();
            InitMessages();
        }

        #region private methods

        private void InitUser()
        {
            txtblCurUserName.Text = _curUser.Name;
            txtblCurUserName2.Text = _curUser.Name;
            AvatarHelper.LoadAvatarFromServer(imgCurUserAvatar, _curUser.Username);
        }

        /// <summary>
        /// Get Friend list, sent request list, reveiced request list
        /// </summary>
        private void InitFriends()
        {
            _displayFriends = new ObservableCollection<ListViewItem>();
            _friends.Clear();
            List<ZolaService.User> tmp;
            //Get Friend List
            #region Get List Friend
            tmp = App.Proxy.GetFriends(_curUser.Username);
            foreach (ZolaService.User friend in tmp)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = AvatarHelper.DefaultAvatarPath,
                    Name = friend.Name,
                    Username = friend.Username,
                    IsOnline = friend.IsOnline,
                    UserType = UserType.Friends
                };
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
            _friends.AddRange(tmp);
            #endregion


            //Get Received Friend request List
            #region get received friend request list
            tmp = App.Proxy.GetPendingFriendRequests(_curUser);
            foreach (ZolaService.User friend in tmp)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = AvatarHelper.DefaultAvatarPath,
                    Name = friend.Name,
                    Username = friend.Username,
                    IsOnline = friend.IsOnline,
                    UserType = UserType.Requests
                };
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
            _friends.AddRange(tmp);
            #endregion


            //Get Pending Friend Request
            #region Get Pending Friend Request
            tmp = App.Proxy.GetSentFriendRequest(_curUser);
            foreach (ZolaService.User friend in tmp)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = AvatarHelper.DefaultAvatarPath,
                    Name = friend.Name,
                    Username = friend.Username,
                    IsOnline = friend.IsOnline,
                    UserType = UserType.Pendings
                };
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
            _friends.AddRange(tmp);
            #endregion


            lvFriends.ItemsSource = _displayFriends;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvFriends.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Content.UserType");
            view.GroupDescriptions.Add(groupDescription);
            view.Filter = FriendNameFilter;
            txtFilter.Text = "";
        }

        /// <summary>
        /// Reload All Friends' avatar from server
        /// </summary>
        private void InitFriendsAvatar()
        {
            foreach (ZolaService.User friend in _friends)
            {
                InitAFriendAvatar(friend.Username);
            }
        }

        /// <summary>
        /// Init a single friend's avatar
        /// </summary>
        /// <param name="friendUsername"></param>
        private void InitAFriendAvatar(string friendUsername)
        {
            BackgroundWorker findFriendAvatar = new BackgroundWorker();
            findFriendAvatar.WorkerReportsProgress = false;
            findFriendAvatar.WorkerSupportsCancellation = true;
            findFriendAvatar.DoWork += FindFriendAvatar_DoWork;
            findFriendAvatar.RunWorkerCompleted += FindFriendAvatar_RunWorkerCompleted;
            findFriendAvatar.RunWorkerAsync(friendUsername);
        }

        /// <summary>
        /// Get All unreceived message
        /// </summary>
        private void InitMessages()
        {
            for (int i = 0; i < _friends.Count; i++)
            {
                _displayMessages[_friends[i].Username] = new ObservableCollection<ListViewItem>();
            }
            BackgroundWorker getMessageWorker = new BackgroundWorker();
            getMessageWorker.WorkerSupportsCancellation = false;
            getMessageWorker.WorkerReportsProgress = false;
            getMessageWorker.DoWork += GetMessageWorker_DoWork;
            getMessageWorker.RunWorkerCompleted += GetMessageWorker_RunWorkerCompleted;
            getMessageWorker.RunWorkerAsync(_curUser.Username);
        }

        /// <summary>
        /// Collapse All Panel in Main panel, containts:
        /// pnInit, pnChat, pnSentRequest, pnReceivedRequest
        /// </summary>
        private void CollapseAllPanels()
        {
            pnInit.Visibility = Visibility.Collapsed;
            pnChat.Visibility = Visibility.Collapsed;
            pnSentRequest.Visibility = Visibility.Collapsed;
            pnReceiveRequest.Visibility = Visibility.Collapsed;
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

        /// <summary>
        /// Filter for searching Friend Name
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool FriendNameFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return (((item as ListViewItem).Content as DisplayUser).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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
            NotificationHelper.NotifyInfo(onlineFriend.Name + " is online!");
        }

        public void FriendOffline(User offlineFriend)
        {
            ZolaService.User friend = _friends.Find(x => x.Username == offlineFriend.Username);
            friend.IsOnline = true;
            ListViewItem item = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == offlineFriend.Username);
            item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
            //MessageBox.Show("complete online change");
            NotificationHelper.NotifyInfo(offlineFriend.Name + " is offline");
        }

        public void FriendOnlineListChangeUnexpectedly()
        {
            InitFriends();
            InitFriendsAvatar();
        }

        public bool ReceiveMessage(DataMessage message)
        {
            DisplayMessage displayMessage = new DisplayMessage()
            {
                AvatarUrl = AvatarHelper.GetAvatarPath(message.Sender.Username),
                MessageContent = message.Message
            };
            if (displayMessage.AvatarUrl == null) displayMessage.AvatarUrl = AvatarHelper.DefaultAvatarPath;
            ListViewItem item = new ListViewItem
            {
                Content = displayMessage,
                ContentTemplate = (DataTemplate)this.FindResource("FriendMessageTemplate")
            };

            _displayMessages[message.Sender.Username].Add(item);
            lvChatMessages.ScrollIntoView(item);
            txtblIsWritting.Text = "";
            DisplayUser user = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == message.Sender.Username).Content as DisplayUser;
            //MessageBox.Show("Here");
            if (pnChat.Visibility == Visibility.Visible && txtblUsernameCurFriend.Text == message.Sender.Username)
            {
                user.UnreadMessage = null;
                //MessageBox.Show("null");
            }
            else
            {
                if(user.UnreadMessage == null)
                {
                    user.UnreadMessage = 1;
                }
                else
                {
                    user.UnreadMessage++;
                }
                NotificationHelper.NotifyGotAMessage(message.Sender.Name, message.Message);
                //MessageBox.Show("unread message = " + user.UnreadMessage);
            }
            return true;
        }

        public void FriendChangeAvatar(User friend)
        {
            //BackgroundWorker findFriendAvatar = new BackgroundWorker();
            //findFriendAvatar.WorkerReportsProgress = false;
            //findFriendAvatar.WorkerSupportsCancellation = true;
            //findFriendAvatar.DoWork += FindFriendAvatar_DoWork;
            //findFriendAvatar.RunWorkerCompleted += FindFriendAvatar_RunWorkerCompleted;
            //findFriendAvatar.RunWorkerAsync(friend.Username);
            InitAFriendAvatar(friend.Username);
            NotificationHelper.NotifyInfo(friend.Name + " have just changed avatar");
        }

        public void FriendIsWrittingMessage(User Friend)
        {
            if (pnChat.Visibility == Visibility.Visible && txtblUsernameCurFriend.Text == Friend.Username)
            {
                txtblIsWritting.Text = Friend.Name + " is writting....";
            }
        }

        public void ReceiveMakeFriendRequest(User stranger)
        {
            DisplayUser displayStranger = new DisplayUser()
            {
                AvatarUrl = AvatarHelper.DefaultAvatarPath,
                IsOnline = stranger.IsOnline,
                Name = stranger.Name,
                Username = stranger.Username,
                UserType = UserType.Requests
            };
            ListViewItem item = new ListViewItem()
            {
                Content = displayStranger,
                ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate")
            };
            _displayFriends.Add(item);
            _friends.Add(stranger);
            _displayMessages.Add(stranger.Username, new ObservableCollection<ListViewItem>());
            InitAFriendAvatar(stranger.Username);
            NotificationHelper.NotifyInfo(stranger.Name + " wants to make friend");
        }

        public void GotANewFriend(User newFriend)
        {
            ListViewItem item = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == newFriend.Username);
            DisplayUser user = item.Content as DisplayUser;
            user.UserType = UserType.Friends;
            user.IsOnline = newFriend.IsOnline;
            if (user.IsOnline == true)
            {
                item.ContentTemplate = (DataTemplate)this.FindResource("OnlineTemplate");
            }
            else
            {
                item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
            }
            ICollectionView view = CollectionViewSource.GetDefaultView(lvFriends.ItemsSource);
            view.Refresh();
            if (pnReceiveRequest.Visibility == Visibility.Visible || pnSentRequest.Visibility == Visibility.Visible)
            {
                CollapseAllPanels();
                pnChat.Visibility = Visibility.Visible;
                pnChat.Visibility = Visibility.Visible;
                txtblNameCurFriend.Text = newFriend.Name;
                txtblUsernameCurFriend.Text = newFriend.Username;
                AvatarHelper.LoadAvatarFromLocal(imgCurFriend, newFriend.Username);
                lvChatMessages.ItemsSource = _displayMessages[newFriend.Username];
                txtblIsWritting.Text = "";
            }
            NotificationHelper.NotifyInfo(newFriend.Name + " is your friend now!");
        }

        public void FriendStopWrittingMessage(User Friend)
        {
            if (pnChat.Visibility == Visibility.Visible && txtblUsernameCurFriend.Text == Friend.Username)
            {
                txtblIsWritting.Text = "";
            }
        }

        public void SentMakeFriendRequest(User stranger)
        {
            DisplayUser displayStranger = new DisplayUser()
            {
                AvatarUrl = AvatarHelper.DefaultAvatarPath,
                IsOnline = stranger.IsOnline,
                Name = stranger.Name,
                Username = stranger.Username,
                UserType = UserType.Pendings
            };
            ListViewItem item = new ListViewItem()
            {
                Content = displayStranger,
                ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate")
            };
            _displayFriends.Add(item);
            _friends.Add(stranger);
            _displayMessages.Add(stranger.Username, new ObservableCollection<ListViewItem>());
            InitAFriendAvatar(stranger.Username);
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

        public IAsyncResult BeginFriendStopWrittingMessage(User Friend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFriendStopWrittingMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGotANewFriend(User newFriend, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginSentMakeFriendRequest(User stranger, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndSentMakeFriendRequest(IAsyncResult result)
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
            string avatarPath = AvatarHelper.GetAvatarPath(username);
            if (avatarPath == null)
            {
                avatarPath = AvatarHelper.DefaultAvatarPath;
            }
            user.AvatarUrl = avatarPath;
        }
        #endregion

        #region Load message worker
        private void GetMessageWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<ZolaService.DataMessage> messages = e.Result as List<ZolaService.DataMessage>;
            if (messages.Count == 0)
            {
                return;
            }
            string curUserAvatarUrl = AvatarHelper.GetAvatarPath(_curUser.Username);
            messages.Sort(new MessageTimeCompare());
            for (int i = 0; i < messages.Count; i++)
            {
                ZolaService.DataMessage message = messages[i];
                string friendAvatarUrl = AvatarHelper.GetAvatarPath(message.Sender.Username);
                if (friendAvatarUrl == null)
                {
                    friendAvatarUrl = AvatarHelper.DefaultAvatarPath;
                }
                DisplayMessage displayMessage = new DisplayMessage()
                {
                    AvatarUrl = friendAvatarUrl,
                    MessageContent = message.Message
                };
                ListViewItem item = new ListViewItem();
                displayMessage.AvatarUrl = friendAvatarUrl;
                item.Content = displayMessage;
                item.ContentTemplate = (DataTemplate)this.FindResource("FriendMessageTemplate");
                _displayMessages[message.Sender.Username].Add(item);
                DisplayUser displayUser = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == message.Sender.Username).Content as DisplayUser;
                if (displayUser.UnreadMessage == null)
                {
                    displayUser.UnreadMessage = 1;
                }
                else
                {
                    displayUser.UnreadMessage++;
                }
            }
        }

        private void GetMessageWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string username = e.Argument.ToString();
            e.Result = App.Proxy.GetUnreceivedMessages(username);
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
                throw new Exception("An exeption in Update Information form: " + ex.Message);
            }
            finally
            {
                updateInformationDialog.Close();
            }
            //this.Show();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvFriends.ItemsSource).Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this._curUser = null;
            if (_friends != null)
                this._friends.Clear();
            this._friends = null;
            if (_displayFriends != null)
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
                switch (user.UserType)
                {
                    case UserType.Friends:
                        if (pnChat.Visibility == Visibility.Collapsed)
                        {
                            CollapseAllPanels();
                            pnChat.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            if (user.Username == txtblUsernameCurFriend.Text)
                                break;
                        }
                        txtblNameCurFriend.Text = user.Name;
                        txtblUsernameCurFriend.Text = user.Username;
                        AvatarHelper.LoadAvatarFromLocal(imgCurFriend, user.Username);
                        lvChatMessages.ItemsSource = _displayMessages[user.Username];
                        txtblIsWritting.Text = "";
                        DisplayUser displayUser = _displayFriends.ToList().Find(x => (x.Content as DisplayUser).Username == user.Username).Content as DisplayUser;
                        displayUser.UnreadMessage = null;
                        txtMessageToSend.Text = "";
                        txtMessageToSend.Focus();
                        break;
                    case UserType.Pendings:
                        if (pnSentRequest.Visibility == Visibility.Collapsed)
                        {
                            CollapseAllPanels();
                            pnSentRequest.Visibility = Visibility.Visible;
                        }
                        break;
                    case UserType.Requests:
                        if (pnReceiveRequest.Visibility == Visibility.Collapsed)
                        {
                            CollapseAllPanels();
                            pnReceiveRequest.Visibility = Visibility.Visible;
                        }
                        AvatarHelper.LoadAvatarFromLocal(imgRequestFriend, user.Username);
                        txtblRequestName.Text = user.Name;
                        txtblRequestUsername.Text = user.Username;
                        bool? gender = _friends.Find(x => x.Username == user.Username).IsMale;
                        if (gender == true) txtblRequestGender.Text = "Male";
                        if (gender == false) txtblRequestGender.Text = "Female";
                        if (gender == null) txtblRequestGender.Text = "Unknow";
                        break;
                }
            }
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (txtMessageToSend.Text.Length == 0)
                return;
            ZolaService.DataMessage message = new DataMessage()
            {
                Message = txtMessageToSend.Text,
                Receiver = new User()
                {
                    Username = txtblUsernameCurFriend.Text
                },
                Sender = _curUser,
                SentTime = DateTime.Now,
            };

            App.Proxy.SendMessageAsync(message);

            DisplayMessage displayMessage = new DisplayMessage()
            {
                AvatarUrl = AvatarHelper.GetAvatarPath(_curUser.Username),
                MessageContent = txtMessageToSend.Text
            };
            if (displayMessage.AvatarUrl == null) displayMessage.AvatarUrl = AvatarHelper.DefaultAvatarPath;
            ListViewItem item = new ListViewItem();
            item.Content = displayMessage;
            item.ContentTemplate = (DataTemplate)this.FindResource("MyMessageTemplate");
            _displayMessages[txtblUsernameCurFriend.Text].Add(item);
            lvChatMessages.ScrollIntoView(item);

            txtMessageToSend.Text = "";
            isWritting = false;
        }

        private void txtMessageToSend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;
            btnSendMessage_Click(null, null);
        }

        private void txtMessageToSend_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMessageToSend.Text.Length > 0)
            {
                if (isWritting)
                {

                }
                else
                {
                    isWritting = true;
                    App.Proxy.WrittingAsync(_curUser, new User() { Username = txtblUsernameCurFriend.Text });
                }
            }
            else
            {
                isWritting = false;
                App.Proxy.StopWrittingAsync(_curUser, new User() { Username = txtblUsernameCurFriend.Text });
            }
        }

        private void btnFindStranger_Click(object sender, RoutedEventArgs e)
        {
            FindStranger findStrangerDialog = new FindStranger(_curUser);
            findStrangerDialog.ShowDialog();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            ZolaService.User bar = _curUser;
            ZolaService.User foo = new User() { Username = txtblRequestUsername.Text, Name = txtblRequestName.Text };
            App.Proxy.AcceptFriendRequest(foo, bar);

        }
        #endregion

    }

    #region Display Classed

    class DisplayUser : INotifyPropertyChanged
    {
        private string _avatarUrl;
        private string _name;
        private string _username;
        private UserType _userType;
        private int? _unReadMessage = null;

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
        public UserType UserType
        {
            get { return this._userType; }
            set
            {
                if (value != this._userType)
                {
                    this._userType = value;
                    NotifyPropertyChanged("UserType");
                }
            }
        }
        public int? UnreadMessage
        {
            get { return this._unReadMessage; }
            set
            {
                //MessageBox.Show("new value = " + value);
                if (value != this._unReadMessage)
                {
                    this._unReadMessage = value;
                    NotifyPropertyChanged("UnreadMessage");
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

    enum UserType { Friends, Pendings, Requests }

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

    class MessageTimeCompare : IComparer<ZolaService.DataMessage>
    {
        public int Compare(DataMessage x, DataMessage y)
        {
            return x.SentTime.CompareTo(y.SentTime);
        }
    }
}
