using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDataFromFile
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader text = new StreamReader(@"D:\all semesters\8th semester\Compiler Construction\Assignments\ReadDataFromFileUsingThreading\Test File.txt");
            Console.WriteLine(text.ToString());
        }
    }
}
