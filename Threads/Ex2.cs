using System;
using System.Threading;
public class A2X2 : Exec
{
    private int time;
    private void IncrementTime()
    {
        while(true)
        {
            Console.Write(time + "... ");
            time++;
            if(time > 4)
            {
                Console.WriteLine("Tempo esgotado!");
                return;
            }
            Thread.Sleep(1000);
        }
    }
    public override void Execute()
    {
        time = 1;
        string? choice;
        Thread timer = new Thread(new ThreadStart(IncrementTime));
        Console.WriteLine("Qual a capital da Europa?");
        Thread.Sleep(1000);
        Console.WriteLine("A) Roma");
        Thread.Sleep(1000);
        Console.WriteLine("B) Berlim");
        Thread.Sleep(1000);
        Console.WriteLine("C) Moskow");
        Thread.Sleep(1000);
        Console.WriteLine("D) Constantinopla");
        Thread.Sleep(1000);
        timer.Start();

        while(timer.IsAlive)
        {
            choice = Console.ReadLine();
            if((choice != null) && choice == "a")
            {
                Console.WriteLine("Resposta Correta");
                break;
            }
            else
                Console.WriteLine("Tente novamente");
        }

    }
}