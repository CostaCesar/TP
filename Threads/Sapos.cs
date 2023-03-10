using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class A2X4 : Exec
{
    static private int raceDistance = 300;
    static private int nRacers = 10;
    static protected List<int> winners;

    internal class Frog
    {
        private int currentDist;
        private readonly int jumpDist;
        private readonly int id;
        public Thread thread;
        private Object locker = new Object();
        
        public Frog(int JumpDist, int id)
        {
            this.jumpDist = JumpDist;
            this.currentDist = 0;
            this.id = id;
            this.thread = new Thread(new ThreadStart(Jump));
        }
        
        public void Jump()
        {
            while(true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(this.id + ": jumped to " + this.currentDist);
                if(this.currentDist >= raceDistance)
                {
                    lock (locker)
                    {
                        Console.WriteLine("Frog" + this.id + " finished!");
                        winners.Add(this.id);
                    }
                    break;
                }
                this.currentDist += jumpDist;
            }
        }
    }
    
    public override void Execute()
    {
        Console.WriteLine("Super FROGRUN 2077 Deluxe");
        Frog[] racers = new Frog[nRacers];
        winners = new List<int>();
        for(int i = 0; i < nRacers; i++)
        {
            Random rng = new Random();
            racers[i] = new Frog(rng.Next(raceDistance / 5) + 10, i);
            racers[i].thread.Start();
        }
        foreach(Frog x in racers)
            x.thread.Join();
            
        Console.WriteLine("--Podio Top 10--");
        for(int i = 0; i < winners.Count && i < 10; i++)
            Console.WriteLine((i+1) + " => Frog" + winners[i]);
    }
}