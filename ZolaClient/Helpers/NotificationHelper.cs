using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZolaClient.Notification;

namespace ZolaClient.Helpers
{
    public static class NotificationHelper
    {
        public static void Notify(string title, string message)
        {
            new NotificationWindow(title, message).Show();
        }

        public static void NotifyGotAMessage(string sender, string message)
        {
            if(message.Length > 21)
            {
                message = message.Substring(0, 21) + "...";
            }
            Notify(sender, message);
        }

        public static void NotifyInfo(string message)
        {
            Notify("Info", message);
        }

        public static void NotifyError(string message)
        {
            Notify("Error", message);
        }
    }
}
