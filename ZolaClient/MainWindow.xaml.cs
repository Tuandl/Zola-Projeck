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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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
                if(i%2 == 0)
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("MyMessageTemplate");
                } else
                {
                    item.ContentTemplate = (DataTemplate)this.FindResource("FriendMessageTemplate");
                }
                messages.Add(item);
            }
            lvChatMessages.ItemsSource = messages;
        }

        private void txtSearchStranger_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

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
}
