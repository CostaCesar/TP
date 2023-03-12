using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Aula4;

public class Server
{
    private IPAddress ipAddress;
    private IPEndPoint serverPoint;
    private int port;
    private int maxConnectionQueue;

    public Server(IPAddress address, int port, int queueSize = 4)
    {
        this.ipAddress = address;
        this.port = port;
        this.serverPoint = new IPEndPoint(this.ipAddress, this.port);
        this.maxConnectionQueue = queueSize;
    }

    public void Start()
    {
        try
        {
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(this.serverPoint);
            listener.Listen(this.maxConnectionQueue);

            Console.WriteLine("Server started in " + this.ipAddress.ToString() + ":" + this.port);
            Console.WriteLine("Waiting connection...");
            Socket conection = listener.Accept();
            Console.WriteLine("Connected with " + (conection.RemoteEndPoint != null ? conection.RemoteEndPoint.ToString() : "NONE"));

            string? data;
            byte[] bytes;

            while(true)
            {
                data = null;
                while(true)
                {
                    bytes = new byte[1024];
                    int size = conection.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, size);
                    if(data.IndexOf("<EOF>") > -1)
                        break;
                }
                if(data.IndexOf("ESC") == 0)
                    break;

                data = data.Substring(0, data.Length - 5);
                Console.WriteLine("Received: " + data);

                bytes = Encoding.ASCII.GetBytes(String.Format("<Server received message>: {0}", data));
                conection.Send(bytes);
            }
            Console.WriteLine("Server closed!");
            conection.Shutdown(SocketShutdown.Both);
            conection.Close();
        }
        catch (Exception e)
        { Console.WriteLine("Error: " + e.Message); }
        return;
    }
}