using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class FrogRun : Exec
{
    static private int raceDistance = 300;
    static private int nRacers = 10;

    internal class Frog
    {
        private int currentDist;
        private static int finishers;
        private readonly int jumpDist;
        private readonly int id;
        public Thread thread;
        
        public Frog(int JumpDist, int id)
        {
            finishers = 1;
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
                if(this.currentDist > raceDistance)
                {
                    Console.WriteLine("Frog " + this.id + " finished in " + finishers + " place!");
                    finishers++;
                    break;
                }
                this.currentDist += jumpDist;
            }
        }
    }
    
    public override void Execute()
    {
        
        Frog[] racers = new Frog[nRacers];
        for(int i = 0; i < nRacers; i++)
        {
            racers[i] = new Frog((i * i) + (-6 *i) + 30, i);
            racers[i].thread.Start();
        }
    }
}