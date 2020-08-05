using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static ConsoleNot.Properties;
using static ConsoleNotLib.JsonWork;

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
                var data = ToJson(Values);
                socket.Send(data);
                while (true)
                {
                    data = new byte[256];
                    var builder = new StringBuilder();
                    int bytes;
                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);

                    if (builder.ToString() == "close")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine(builder.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error. Exception: {e.Message}");
            }
        }
    }
}