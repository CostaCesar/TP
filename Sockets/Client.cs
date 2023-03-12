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
            // Criar o enviador
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);   
            sender.Connect(remotePoint);
            Console.WriteLine("Connected to " + (sender.RemoteEndPoint != null ? sender.RemoteEndPoint.ToString() : "NONE"));
        
            // Mandar mensagens enquanto não digitarem "ESC"
            while(true)
            {
                // Pegar mensagem
                Console.WriteLine("Message to server [\"ESC\" to exit]: ");
                string? data = Console.ReadLine();
                if(data == null)
                    break;

                // Converter em bytes e enviar
                byte[] bytes = Encoding.ASCII.GetBytes(data + " <EOF>");
                sender.Send(bytes);

                // Quebre aqui para mandar o ESC para o servidor tambem
                if(data == "ESC")
                    break;

                // Preparar e receber mensagem do servidor
                bytes = new byte[1024];
                int size = sender.Receive(bytes);
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, size));
            }
            // Fechar o cliente
            Console.WriteLine("Client closed!");
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch(Exception e)
        { Console.WriteLine("Error: " + e.Message); }

        return;
    }
}