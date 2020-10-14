using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static Main.Properties;
using Lib;
// ReSharper disable ObjectCreationAsStatement

namespace Main
{
    public class Client
    {
        public Client(int port, string address)
        {
            try
            {
                var ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
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
                    else if (builder.ToString().Contains("{"))
                    {
                        TitleAndDesc[0] = JsonWork.FromJson(builder.ToString())["Title"];
                        TitleAndDesc[1] = JsonWork.FromJson(builder.ToString())["Description"];
                    }
                    else
                    {
                        new Notification();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ResourceManagerProp.GetString("Client_Exception", CultureInfoProp), e.Message);
            }
        }
    }
}

/*TODO:
var client = new RestClient("https://localhost:5001/notContr?Id=123&Name=321");
client.Timeout = -1;
var request = new RestRequest(Method.POST);
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);
*/