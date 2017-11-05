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

namespace ZolaClient.Notification
{
    /// <summary>
    /// Interaction logic for NotificationWindowError.xaml
    /// </summary>
    public partial class NotificationWindowError : Window
    {
        public NotificationWindowError(string message)
        {
            InitializeComponent();
            this.txtblTitle.Text = "Error";
            this.txtblMessage.Text = message;
            this.Loaded += NotificationWindow_Loaded;
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void Storyboard_Completed(object sender, EventArgs e) { this.Close(); }
    }
}
