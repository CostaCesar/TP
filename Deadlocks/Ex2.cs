using System;
using System.Threading;

public class Deadlock_Ex2 : Exec
{
    internal class Worker
    {
        private string product;
        private int time;
        private int cycles;
        private Random rng;
        public Thread thread;
        
        public Worker(string product, int time, int cycles)
        {
            this.product = product;
            this.time = time;
            this.cycles = cycles;
            this.rng = new Random();
            this.thread = new Thread(new ThreadStart(DoWork));
        }
        
        public void DoWork()
        {
            for(int i = 0; i < cycles; i++)
            {
                Console.WriteLine(i + " cycle:");
                
                try
                { Thread.Sleep(rng.Next(time * 1000)); }
                catch(ThreadInterruptedException e)
                { Console.WriteLine("Stopped!" + e); }
                
                Console.WriteLine("Finished " + product);
            }
        }
    }
    
    public override void Execute()
    {
        Worker alex = new Worker("Shoe", 4, 50);
        Worker john = new Worker("Shoe", 2, 75);
        Worker mike = new Worker("Box", 1, 150);
        alex.thread.Start();
        mike.thread.Start();
        john.thread.Start();
    }
}