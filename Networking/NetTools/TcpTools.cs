using System.Net.Sockets;
using System.Text;

namespace Networking.NetTools
{
    public static class TcpTools
    {
        /// <summary>
        /// Reads binary data from TCP connection
        /// </summary>
        /// <param name="receiver">TCP socket to read data from</param>
        /// <returns>An array of bytes containing data from specified TCP socket</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ReadBytes(TcpClient receiver)
        {
            byte[] bytes;
            using (var ns = receiver.GetStream())
            {
                bytes = new byte[ns.Length];
                ns.Read(bytes, 0, receiver.Available);
            }
            return bytes;
        }

        /// <summary>
        /// Sends binary data via TCP conneciton
        /// </summary>
        /// <param name="bytes">Binaty data to send</param>
        /// <param name="sender">TCP socket to send data with</param>
        public static void SendBytes(byte[] bytes, TcpClient sender)
        {
            using (var ns = sender.GetStream())
            {
                ns.Write(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// Sends UTF-8 encoded text data via TCP connection
        /// </summary>
        /// <param name="str">Text to send</param>
        /// <param name="sender">TCP socket to send data with</param>
        public static void SendString(string str, TcpClient sender)
        {
            SendBytes(Encoding.UTF8.GetBytes(str), sender);
        }

        /// <summary>
        /// Reads UTF-8 encoded text data from TCP connection
        /// </summary>
        /// <param name="receiver">TCP socket to read from</param>
        /// <returns>A string of UTF-8 encoded text, received from TCP connection</returns>
        public static string ReadString(TcpClient receiver)
        {
            return Encoding.UTF8.GetString(ReadBytes(receiver));
        }
    }
}
