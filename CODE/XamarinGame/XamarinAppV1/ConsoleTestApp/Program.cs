using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAppV1.Models;

namespace ConsoleTestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string word = "heels";
            
            Console.WriteLine();

            Console.WriteLine($"Random word is {word}");

            Console.WriteLine($"Random List is {ScrambleString.GenerateRandomLetter(word, 15)}");

            Console.ReadKey();
        }
    }
}
