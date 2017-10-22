using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ConsoleHost
{
    class Program
    {
        static ServiceHost host = null;

        static void Main(string[] args)
        {
            host = new ServiceHost(typeof(ServerLibrary.ChatService));

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
