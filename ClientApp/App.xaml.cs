using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TcpClient? TcpClient { get; set; }

        /// <summary>
        /// Binds a TcpClient property of this instance to the specfied EP
        /// </summary>
        /// <param name="address">IP address to bind</param>
        /// <param name="port">Port to bind</param>
        public void BindTcpClient(IPAddress address, int port)
        {
            TcpClient = new TcpClient(address.ToString(), port);
        }

        /// <summary>
        /// Connects a TcpClient property of this instance to the speciefied remote EP
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void ConnectTcpClient(IPAddress address, int port)
        {
            if (TcpClient == null || TcpClient.Connected)
            {
                return;
            }
            TcpClient.Connect(address, port);
        }
    }
}
