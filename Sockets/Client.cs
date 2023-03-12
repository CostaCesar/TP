using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Aula4;

public class Client
{
    private IPAddress ipAddress;
    private IPEndPoint remotePoint;
    private int port;
    public Client(IPAddress address, int port)
    {
        this.ipAddress = address;
        this.port = port;
        this.remotePoint = new IPEndPoint(ipAddress, port);
    }

    public void Start()
    {
        try
        {
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);   
            sender.Connect(remotePoint);
            Console.WriteLine("Connected to " + (sender.RemoteEndPoint != null ? sender.RemoteEndPoint.ToString() : "NONE"));
        
            while(true)
            {
                Console.WriteLine("Message to server [\"ESC\" to exit]: ");
                string? data = Console.ReadLine();
                if(data == null)
                    break;

                byte[] bytes = Encoding.ASCII.GetBytes(data + " <EOF>");
                sender.Send(bytes);

                if(data == "ESC")
                    break;

                bytes = new byte[1024];
                int size = sender.Receive(bytes);
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, size));
            }
            Console.WriteLine("Client closed!");
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch(Exception e)
        { Console.WriteLine("Error: " + e.Message); }

        return;
    }

}