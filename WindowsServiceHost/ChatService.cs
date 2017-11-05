using ServerLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceHost
{
    public partial class ChatService : ServiceBase
    {
        static ServiceHost host = null;

        public ChatService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Avatar\");
            Console.WriteLine("Avatar location: " + dir.ToString());

            host = new ServiceHost(typeof(ServerLibrary.ChatService));
            ServerLibrary.ChatService.Init();
            try
            {
                host.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        protected override void OnStop()
        {
            if(host != null)
            {
                host.Close();
                host = null;
            }
        }
    }
}
