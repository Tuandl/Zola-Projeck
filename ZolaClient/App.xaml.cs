using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.ServiceModel;

namespace ZolaClient
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static object _synObj;

        private static ZolaService.ChatServiceClient _proxy = null;

        public static ZolaService.ChatServiceClient Proxy { get { return _proxy; } }

        private static string _ip = "localhost";
        public static string IP { get { return _ip; } set { _ip = value; } }

        /// <summary>
        /// Create instance to connect to server through IP address
        /// </summary>
        /// <param name="ip"></param>
        public static void Connect(ZolaService.IChatServiceCallback callback)
        {
            if (_proxy == null)
            {
                InstanceContext context = new InstanceContext(callback);
                _proxy = new ZolaService.ChatServiceClient(context);
            }
            if (_proxy.State != System.ServiceModel.CommunicationState.Opened)
            {
                string servicePath = _proxy.Endpoint.ListenUri.AbsolutePath;
                string serviceListenerPort = _proxy.Endpoint.Address.Uri.Port.ToString();
                _proxy.Endpoint.Address = new EndpointAddress("net.tcp://" + IP +
                    ":" + serviceListenerPort + servicePath);

                _proxy.Open();
            }
        }

        /// <summary>
        /// Disconnect Proxy with server
        /// </summary>
        public static void Disconnect()
        {
            if(_proxy != null)
            {
                switch (_proxy.State)
                {
                    case CommunicationState.Closed: break;
                    case CommunicationState.Closing: break;
                    case CommunicationState.Created:
                        _proxy.Close();
                        break;
                    case CommunicationState.Faulted:
                        _proxy.Abort();
                        break;
                    case CommunicationState.Opened:
                        _proxy.Close();
                        break;
                    case CommunicationState.Opening:
                        _proxy.Close();
                        break;
                }
                _proxy = null;
            }
        }
    }
}
