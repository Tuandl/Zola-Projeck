using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServerLibrary;

namespace ConsoleHost
{
    class Program
    {
        static ServiceHost host = null;

        static void Main(string[] args)
        {

            Dictionary<User, IChatServiceCallback> _allUsers = new Dictionary<User, IChatServiceCallback>(new UserEqualityComparer());
            _allUsers.Add(new User() { Username = "tuan", Name = "tuan" }, null);
            _allUsers.Add(new User() { Username = "tuanv", Name = "123" }, null);
            Console.WriteLine(_allUsers.Count);
            Console.WriteLine(_allUsers.ContainsKey(new User() { Username = "tuansd" }));
            Console.WriteLine(_allUsers.ContainsKey(new User() { Username = "tuanv" }));
            Console.WriteLine(_allUsers[new User() { Username = "tuanv"}] == null);


            //host = new ServiceHost(typeof(ServerLibrary.ChatService));

            //try
            //{
            //    host.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.WriteLine("Press enter to exit");
            //Console.ReadLine();
        }
    }
}
