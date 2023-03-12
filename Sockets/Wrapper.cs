using System;
using System.Net;
using System.Net.Sockets;
public class A4 : Exec
{
    public override void Execute()
    {
        Console.WriteLine("Start <client> or <server>?");
        string? choice = Console.ReadLine();
        if(choice == null) return;

        if(choice == "client")
        {
            Aula4.Client client = new Aula4.Client(IPAddress.Parse("127.0.0.1"), 12345);
            client.Start();
        }
        else if(choice == "server")
        {
            Aula4.Server server = new Aula4.Server(IPAddress.Parse("127.0.0.1"), 12345);
            server.Start();
        }
        else Console.WriteLine("INVALID!");
        return;
    }
}