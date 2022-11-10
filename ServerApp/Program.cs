using Networking;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ServerApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in Enum.GetValues<RequestType>())
            {
                sb.AppendLine($"{(int)val} -\t{val}");
            }
            Console.WriteLine(sb.ToString());
            RequestType rt = (RequestType)int.Parse(Console.ReadLine()!);
            Server server = new Server(IPAddress.Parse("1.1.1.1"), 50);
            RequestOptions ro = new RequestOptions() { RequestType = rt, GroupId = 5 };
            var response = server.Test(ro);
            string json = JsonConvert.SerializeObject(response, Formatting.Indented);
            File.WriteAllText("C:/Users/Roman/Desktop/teftfile.txt", json);
        }
    }
}