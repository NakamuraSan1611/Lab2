using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab2
{
    partial class Program
    {
        static void smoke()
        {
            Console.Clear();
            Console.WriteLine("чет я устал...");
            Thread.Sleep(2000);
            Console.WriteLine("Пойду покурю...");
            Thread.Sleep(3000);
            for (int i = 0; i < 50; i++)
            {
                using (StreamReader sr = new StreamReader("smoke.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while (!(line = sr.ReadLine()).Contains("a"))
                    {
                        Console.WriteLine(line);
                    }
                    Thread.Sleep(50);
                    Console.Clear();
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                    Thread.Sleep(50);
                    Console.Clear();
                }
            }
            Console.WriteLine("докурил :p");
        }
    }
}