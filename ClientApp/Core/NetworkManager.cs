using Networking.NetTools;
using System.Net.Sockets;

namespace ClientApp.Core
{
    class NetworkManager
    {
        private static NetworkManager? instance;

        private NetworkManager()
        {
            Client = new TcpClient(IPAddressTools.GetLocalIP().ToString(), 20);
        }

        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkManager();
                }
                return instance;
            }
        }

        public TcpClient Client { get; private set; }
    }
}
