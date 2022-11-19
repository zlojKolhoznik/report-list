using System.Net;
using System.Net.Sockets;

namespace Networking.NetTools
{
    public static class IPAddressTools
    {
        public static IPAddress GetLocalIP()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            try
            {
                socket.Connect(remoteEP);
            }
            catch (Exception ex)
            {
                socket.Close();
                throw new Exception(ex.Message);
            }
            IPEndPoint localEP = socket.LocalEndPoint as IPEndPoint;
            return localEP!.Address;
        }
    }
}
