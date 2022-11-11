using Networking.NetTools;
using ServerApp;
using System.Net;

IPAddress localAddress = IPAddressTools.GetLocalIP();
int localPort = 20;
Server server = new Server(localAddress, localPort);
Task.Run(() => server.Run());
Console.WriteLine($"The server is listening on port {localPort}. To stop the server, either press Enter or Ctrl+C");
Console.ReadLine();
