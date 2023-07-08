// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Console.Write("First");
// Console.Write("Second"); // println() vs print() in java

using System;

namespace HelloWorld // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
        }
    }
}