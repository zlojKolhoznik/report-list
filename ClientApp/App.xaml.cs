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
        private TcpClient? tcpClient;
        public TcpClient TcpClient 
        {
            get
            {
                if (tcpClient == null)
                {
                    tcpClient = new TcpClient(Address.ToString(), Port);
                }
                if (!tcpClient.Connected)
                {
                    tcpClient.Connect("192.168.0.102", Port);
                }
                return tcpClient!;
            }
            set => tcpClient = value;
        }

        public IPAddress Address { get; set; }
        public int Port { get; set; }
    }
}
