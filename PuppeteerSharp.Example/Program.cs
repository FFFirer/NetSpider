using System;

namespace PuppeteerSharp.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ClearScriptDemo demo = new ClearScriptDemo();
            demo.Execute();


            Console.ReadKey();
        }
    }
}
