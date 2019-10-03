using System;
using CommandLine;

namespace ToGit
{


    class Program
    {
        public class Options
        {
            [Option('s', "setupfile", Required = true, HelpText ="Path to setup file")]
            public string SetupFile { get; set; }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
