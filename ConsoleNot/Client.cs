using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ConsoleNot.Resource;
using static ConsoleNot.Properties;
using ConsoleNotLib;

namespace ConsoleNot
{
    public class Client
    {
        public Client(int _port, string _address)
        {
            try
            {
                var ipPoint = new IPEndPoint(IPAddress.Parse(_address), _port);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                
                var data = JsonWork.ToJson(Values);
                socket.Send(data);
                
                while (true)
                {
                    data = new byte[256];
                    var builder = new StringBuilder();
                    
                    do
                    {
                        var bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);

                    if (builder.ToString() == "close")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        CallNot.CallNotification(TitleAndDesc,
                            ResourceManager.GetString("Sorry_OS", CultureInfo));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ResourceManager.GetString("Client_Exception", CultureInfo));
            }
        }
    }
}