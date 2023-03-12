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
            // Criar o sentinela
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(this.serverPoint);
            listener.Listen(this.maxConnectionQueue);
            Console.WriteLine("Server started in " + this.ipAddress.ToString() + ":" + this.port);
            
            // Esperar conexão e estabelecer
            Console.WriteLine("Waiting connection...");
            Socket conection = listener.Accept();
            Console.WriteLine("Connected to " + (conection.RemoteEndPoint != null ? conection.RemoteEndPoint.ToString() : "NONE"));

            // Loop para mensagens enquanto não receber "ESC" do client
            while(true)
            {
                string? data = null;
                byte[] bytes;
                
                // Receber mensagem do cliente
                // Unica parte que é "segura" para ser assíncrona
                while(true)
                {
                    bytes = new byte[1024];
                    int received_Bytes = conection.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, received_Bytes);
                    if(data.IndexOf("<EOF>") > -1)
                        break;
                }
                // Se receber "ESC", sair
                if(data.IndexOf("ESC") == 0)
                    break;

                // Mostrar recepção
                data = data.Substring(0, data.Length - 5);
                Console.WriteLine("Received: " + data);

                // Enviar resposta para o cliente
                bytes = Encoding.ASCII.GetBytes(String.Format("<Server received message>: {0}", data));
                conection.Send(bytes);
            }
            // Fechar o servidor
            Console.WriteLine("Server closed!");
            conection.Shutdown(SocketShutdown.Both);
            conection.Close();
        }
        catch (Exception e)
        { Console.WriteLine("Error: " + e.Message); }
        return;
    }
}