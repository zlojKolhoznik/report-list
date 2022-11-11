using Networking.DataViews;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
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
                return tcpClient!;
            }
            set => tcpClient = value;
        }

        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public UserDataView? User { get; set; }

        public byte[] SendRequestAndReceiveResponse(byte[] request)
        {
            byte[] result;
            using (var ns = TcpClient.GetStream())
            {
                ns.Write(request, 0, request.Length);
                while (TcpClient.Available <= 0)
                { }
                result = new byte[TcpClient.Available];
                ns.Read(result);
            }
            tcpClient!.Close();
            tcpClient = null;
            return result;
        }

        public async Task<bool> CanConnect()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var check = TcpClient;
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
