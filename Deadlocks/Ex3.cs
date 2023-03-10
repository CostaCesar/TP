using System;
using System.Threading;

public class A3X3 : Exec
{
    internal class Worker
    {
        private string product;
        private int time;
        private Random rng;
        public Thread thread;
        private CancellationTokenSource tokenSource;
        
        public Worker(string product, int time)
        {
            this.product = product;
            this.time = time;
            this.rng = new Random();
            this.tokenSource = new CancellationTokenSource();
            this.thread = new Thread(() => DoWork(tokenSource.Token));
        }
        
        public void stopWork()
        {
            this.tokenSource.Cancel();
            Console.WriteLine("Stopping the production of " + this.product);
            this.thread.Join();
            this.tokenSource.Dispose();
        }

        public void DoWork(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
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
        Worker alex = new Worker("Shoe", 4);
        Worker john = new Worker("Paper", 2);
        Worker mike = new Worker("Box", 1);
        alex.thread.Start();
        mike.thread.Start();
        john.thread.Start();
        Thread.Sleep(10000);
        alex.stopWork();
        mike.stopWork();
        john.stopWork();
    }
}