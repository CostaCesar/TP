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
        Dictionary<string, Exec> programs = new Dictionary<string, Exec>
        {
            {"aula2_x1", new A2X1()},
            {"aula2_x2", new A2X2()},
            {"aula2_x4", new A2X4()}
        };

        Console.WriteLine("TP 2023: Caio Cesar Moraes Costa");
        Console.WriteLine("Exercicios prontos: ");
        foreach(string index in programs.Keys)
            Console.WriteLine("-> " + index);
        Console.Write("Escolha: ");
        string choice = Console.ReadLine();
        if(programs.ContainsKey(choice))
            programs[choice].Execute();
        else Console.WriteLine("# PROGRAMA INEXISTENTE! #");

        Console.WriteLine("# PROGRAMA ENCERRADO. APERTE ENTER #");
        Console.ReadLine();
    }
}