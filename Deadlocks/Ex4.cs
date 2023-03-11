using System;
using System.Threading;
using System.Collections.Concurrent;

public class A3X4 : Exec
{
    ConcurrentStack<int> items = new ConcurrentStack<int>();
    internal class Producer
    {
        private ConcurrentStack<int> stack;
        public Thread process;
        private CancellationTokenSource tokenSource;
        private static int cycle;
        private Object cycle_lock = new Object();

        public Producer(ref ConcurrentStack<int> stack)
        { 
            this.stack = stack;
            this.tokenSource = new CancellationTokenSource();
            this.process = new Thread(() => Make(tokenSource.Token));
            cycle = 0;
        }

        public void Stop()
        {
            this.tokenSource.Cancel();
            Console.WriteLine("Stopping the production");
            this.process.Join();
            this.tokenSource.Dispose();
        }

        public void Make(CancellationToken order)
        {
            while(!order.IsCancellationRequested)
            {
                lock(cycle_lock)
                {
                    Console.WriteLine("Produced " + ++cycle);
                }
                this.stack.Push(cycle);
                try
                { Thread.Sleep(1000); }
                catch(ThreadInterruptedException e)
                { Console.WriteLine("Stopped!" + e); }
            }
        }
    }
    internal class Consumer
    {
        private ConcurrentStack<int> stack;
        public Thread process;
        private CancellationTokenSource tokenSource;

        public Consumer(ref ConcurrentStack<int> stack)
        { 
            this.stack = stack;
            this.tokenSource = new CancellationTokenSource();
            this.process = new Thread(() => Consume(tokenSource.Token));
        }

        public void Stop()
        {
            this.tokenSource.Cancel();
            Console.WriteLine("Stopping the consumption");
            this.process.Join();
            this.tokenSource.Dispose();
        }

        public void Consume(CancellationToken order)
        {
            int consumed;
            while(!order.IsCancellationRequested)
            {
                if(this.stack.Count > 0)
                {
                    this.stack.TryPop(out consumed);
                    Console.WriteLine("Consumed " + consumed);
                }
                try
                { Thread.Sleep((new Random().Next() % 3 + 1) * 1000); }
                catch(ThreadInterruptedException e)
                { Console.WriteLine("Stopped!" + e); }
            }
        }
    }
    public override void Execute()
    {
        Producer A = new Producer(ref items);
        Consumer B = new Consumer(ref items);
        Producer C = new Producer(ref items);
        Consumer D = new Consumer(ref items);
        
        Console.WriteLine("Commencing production!");
        A.process.Start();
        Thread.Sleep(200);
        C.process.Start();
        Thread.Sleep(5000);

        Console.WriteLine("Commencing consupmton!");
        B.process.Start();
        Thread.Sleep(300);
        D.process.Start();

        Thread.Sleep(15000);

        A.Stop();
        C.Stop();
        Thread.Sleep(3000);
        B.Stop();
        D.Stop();

        Console.WriteLine("Produtos esquecidos: " + items.Count);
    }
}