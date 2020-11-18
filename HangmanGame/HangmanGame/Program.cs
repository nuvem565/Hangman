using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string countriesFilePath = Directory.GetParent(workingDirectory).Parent.FullName + "\\" + "countries_and_capitals.txt";
            string[] countriesInput = File.ReadAllLines(countriesFilePath);

            //for(int i = 0; i < countriesInput.Length; i++)
            //    Console.Write(countriesInput[i] + "\r\n");
        }
    }
}
