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
            socket.Connect(remoteEP);
            IPEndPoint localEP = socket.LocalEndPoint as IPEndPoint;
            return localEP!.Address;
        }
    }
}
