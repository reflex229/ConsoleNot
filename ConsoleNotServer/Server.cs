using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static ConsoleNotLib.JsonWork;
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
                    int bytes;
                    var data = new byte[256];
                    
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (handler.Available>0);
                    
                    Console.WriteLine(builder);
                    Values = FromJson(builder.ToString());
                    Console.WriteLine(Values["Title"]);
                    
                    var message = "data";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    Thread.Sleep(1000);
                    handler.Send(data);
                    Thread.Sleep(1000);
                    handler.Send(data);
                    Thread.Sleep(1000);
                    message = "close";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);
                    
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