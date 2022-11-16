using Networking.DataViews;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
                if (tcpClient == null || !tcpClient.Connected)
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

        public (byte[], string) DownloadFile(byte[] request)
        {
            int buffSize = 1024;
            byte[] buff = new byte[buffSize];
            byte[] result;
            string ext;
            using (NetworkStream ns = TcpClient.GetStream())
            {
                ns.Write(request, 0, request.Length);
                ns.Read(buff, 0, buff.Length);
                string json = Encoding.UTF8.GetString(buff);
                Dictionary<string, string> headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
                int len = int.Parse(headers["Content-Length"]);
                ext = headers["File-Extension"];
                result = new byte[len];
                using (MemoryStream ms = new MemoryStream(result))
                {
                    while (len > 0)
                    {
                        byte[] buffer = new byte[buffSize];
                        int size = ns.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, size);
                        len -= size;
                    }
                }
            }
            return (result, ext);
        }

        public byte[] AddHomework(byte[] requestBody, byte[] file, string fileExt)
        {
            string len = file.Length.ToString();
            int buffSize = 1024;
            int buffCount = (int)Math.Ceiling(Convert.ToSingle(len) / buffSize);
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Content-Length", len },
                { "File-Extension", fileExt}
            };
            string json = JsonConvert.SerializeObject(headers);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            byte[] result = new byte[buffSize];
            using (NetworkStream ns = TcpClient.GetStream())
            {
                ns.Write(requestBody);
                Thread.Sleep(350);
                ns.Write(bytes);
                Thread.Sleep(50);
                using (MemoryStream ms = new MemoryStream(file))
                {
                    for (int i = 0; i < buffCount; i++)
                    {
                        byte[] buffer = new byte[buffSize];
                        int size = ms.Read(buffer, 0, buffSize);
                        ns.Write(buffer, 0, size);
                    }
                }
                ns.Read(result, 0, buffSize);
            }
            return result;
        }
    }
}
