using System;
using System.Threading;
public class Exex1 : Exec
{
    internal class Run1
    {
        public readonly int ID;
        public Run1(int id)
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
        Run1 one = new Run1(1);
        Thread t1 = new Thread(new ThreadStart(one.Run));
        Run1 two = new Run1(2);
        Thread t2 = new Thread(new ThreadStart(two.Run));
        Run1 three = new Run1(3);
        Thread t3 = new Thread(new ThreadStart(three.Run));
        
        t1.Start();
        t2.Start();
        t3.Start();
        t1.Join();
        t2.Join();
        t3.Join();
    }
}