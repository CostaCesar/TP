using System;
using System.Threading;

public class Exex2 : Exec
{
    private int time;
    private bool flag;
    private void IncrementTime()
    {
        while(true)
        {
            Console.Write(time + "... ");
            time++;
            if(time > 6)
            {
                flag = true;
                return;
            }
            Thread.Sleep(1000);
        }
    }
    public override void Execute()
    {
        time = 1;
        flag = false;

        Thread timer = new Thread(new ThreadStart(IncrementTime));
        Console.WriteLine("Qual a capital da Europa?");
        timer.Start();
        Thread.Sleep(1000);
        Console.WriteLine("A) Roma");
        Thread.Sleep(1000);
        Console.WriteLine("B) Berlim");
        Thread.Sleep(1000);
        Console.WriteLine("C) Moskow");
        Thread.Sleep(1000);
        Console.WriteLine("D) Constantinopla");
        while(true)
        {
            if(flag == true)
            {
                timer.Join();
                Console.WriteLine("Tempo esgotado!");
                return;
            }
        }
    }
}