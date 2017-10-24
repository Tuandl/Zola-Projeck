using System;
using System.Collections.Generic;
using System.ServiceModel;
using ServerLibrary;
using System.IO;

namespace ConsoleHost
{
    class Program
    {
        static ServiceHost host = null;

        static void Main(string[] args)
        {

            //Dictionary<User, string> _allUsers = new Dictionary<User, string>();
            //_allUsers.Add(new User() { Username = "tuan", Name = "tuan" }, "abc");
            //_allUsers.Add(new User() { Username = "tuanv", Name = "123" }, "tmp");
            //Console.WriteLine(_allUsers.Count);
            //Console.WriteLine(_allUsers.ContainsKey(new User() { Username = "tuansd" }));
            //Console.WriteLine(_allUsers.ContainsKey(new User() { Username = "tuanv" }));
            //Console.WriteLine(_allUsers[new User() { Username = "tuanv"}] == null);
            //Console.WriteLine(_allUsers[new User() { Username = "tuan" }]);

            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + @"\Resources\Avatar\");
            Console.WriteLine("Avatar location: " + dir.ToString());

            host = new ServiceHost(typeof(ServerLibrary.ChatService));
            ChatService.Init();
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
    }
}
