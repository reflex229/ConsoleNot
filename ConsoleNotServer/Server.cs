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
        public Server(int _port)
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port);
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);
                
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                
                while (true)
                {
                    var handler = listenSocket.Accept();
                    // получаем сообщение
                    var builder = new StringBuilder();
                    var data = new byte[256];
                    
                    do
                    {
                        var bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (handler.Available>0);
                    
                    Console.WriteLine(builder);
                    Values = JsonWork.FromJson(builder.ToString());
                    Console.WriteLine(Values["Title"]);
                    
                    for (var i = 0; i < Convert.ToInt32(Values["Count"]); i++)
                    {
                        Thread.Sleep(Convert.ToInt32(Values["IterationTime"]));
                        handler.Send(Encoding.Unicode.GetBytes("call"));
                    }
                    
                    handler.Send(Encoding.Unicode.GetBytes("close"));
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error. Exception: {e.Message}");
            }
        }
    }
}