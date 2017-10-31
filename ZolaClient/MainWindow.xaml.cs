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

namespace ZolaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ZolaService.IChatServiceCallback
    {
        private ZolaService.User _curUser = null;
        private List<ZolaService.User> _friends = null;
        private List<ListViewItem> _displayFriends = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init(ZolaService.User user)
        {
            this._curUser = user;
            InitUser();
            InitFriends();
        }

        private void txtSearchStranger_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #region private methods

        private void InitUser()
        {
            txtblCurUserName.Text = _curUser.Name;
            AvatarHelper.LoadAvatar(imgCurUserAvatar, _curUser.Username);

        }

        private void InitFriends()
        {
            _displayFriends = new List<ListViewItem>();
            _friends = App.Proxy.GetFriends(_curUser.Username);
            foreach (ZolaService.User friend in _friends)
            {
                DisplayUser user = new DisplayUser()
                {
                    AvatarUrl = Environment.CurrentDirectory + @"\Resources\img\avatar-default.png",
                    Name = friend.Name,
                    Username = friend.Username,
                    IsOnline = friend.IsOnline
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
            lvFriends.ItemsSource = _displayFriends;
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
            ListViewItem item = _displayFriends.Find(x => (x.Content as DisplayUser).Username == onlineFriend.Username);
            item.ContentTemplate = (DataTemplate)this.FindResource("OnlineTemplate");
            //MessageBox.Show("complete online change");

        }

        public void FriendOffline(User offlineFriend)
        {
            ZolaService.User friend = _friends.Find(x => x.Username == offlineFriend.Username);
            friend.IsOnline = true;
            ListViewItem item = _displayFriends.Find(x => (x.Content as DisplayUser).Username == offlineFriend.Username);
            item.ContentTemplate = (DataTemplate)this.FindResource("OfflineTemplate");
            //MessageBox.Show("complete online change");
        }

        public void FriendOnlineListChangeUnexpectedly()
        {
            throw new NotImplementedException();
        }
        public bool ReceiveMessage(DataMessage message)
        {
            throw new NotImplementedException();
        }
        public void FriendChangeAvatar(User friend)
        {
            throw new NotImplementedException();
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

    }

    #region Display Classed
    class DisplayUser
    {
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsOnline { get; set; }
    }

    class DisplayMessage
    {
        public string AvatarUrl { get; set; }
        public string MessageContent { get; set; }
    }

    #endregion
}
