using System;
using System.Threading;
public class A2X1 : Exec
{
    internal class Counter
    {
        public readonly int ID;
        public Counter(int id)
        {
            this.ID = id;
        }
        public void Run()
        {
            for(int i = 0; i < 1000; i++)
                Console.WriteLine("<Programa " + ID + ">: " + i);
            Console.WriteLine("<Programa " + ID + ">: Finished!");
        }
    }
    public override void Execute()
    {
        Counter one = new Counter(1);
        Thread t1 = new Thread(new ThreadStart(one.Run));
        Counter two = new Counter(2);
        Thread t2 = new Thread(new ThreadStart(two.Run));
        Counter three = new Counter(3);
        Thread t3 = new Thread(new ThreadStart(three.Run));
        
        t1.Start();
        t2.Start();
        t3.Start();
        t1.Join();
        t2.Join();
        t3.Join();
    }
}