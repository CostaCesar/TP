using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public abstract class Exec
{
    public abstract void Execute();
}

public class Program
{
    public static void Main()
    {
        List<Exec> programs = new List<Exec>()
        {
            new Exex1(),
            new Exex2()
        };

        foreach(Exec X in programs)
            X.Execute();

        Console.ReadLine();
    }
}