using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ConsoleNotLib;
using static ConsoleNotServer.Properties;

namespace ConsoleNotServer
{
    public class Server
    {
        public Server(int port)
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine(ResourceManager.GetString("Server_Started", CultureInfo));

                while (true)
                {
                    var handler = listenSocket.Accept();
                    var builder = new StringBuilder();
                    var data = new byte[256];

                    do
                    {
                        var bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (handler.Available > 0);

                    Values = JsonWork.FromJson(builder.ToString());

                    handler.Send(JsonWork.ToJson(Values));
                    Thread.Sleep(100);
                    
                    for (var i = 0; i < Convert.ToInt32(Values["Count"]); i++)
                    {
                        Thread.Sleep(Convert.ToInt32(Values["IterationTime"]));
                        handler.Send(Encoding.Unicode.GetBytes("call"));
                    }

                    Thread.Sleep(100);
                    handler.Send(Encoding.Unicode.GetBytes("close"));

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ResourceManager.GetString("Error_Exception", CultureInfo), e);
            }
        }
    }
}