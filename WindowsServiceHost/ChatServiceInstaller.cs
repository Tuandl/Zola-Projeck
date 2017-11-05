using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace WindowsServiceHost
{
    [RunInstaller(true)]
    public class ChatServiceInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ChatServiceInstaller()
        {
            process = new ServiceProcessInstaller
            {
                Account = ServiceAccount.NetworkService
            };
            service = new ServiceInstaller
            {
                ServiceName = "ZolaChatService",
                DisplayName = "Zola Chat Service",
                Description = "Host service for Zola Projeck",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
