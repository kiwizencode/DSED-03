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
            /*
             * Unit-test ScrambleString class
             */
            //string word = "heels";
            //Console.WriteLine();
            //Console.WriteLine($"Random word is {word}");
            //Console.WriteLine($"Random List is {ScrambleString.GenerateRandomLetter(word, 15)}");


            /*
             *  https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string
             */
            //for (int i = 1; i < 16;i++)
            //{
            //    string i_str = i.ToString();
            //    string text = i_str.PadLeft(2,'0');
            //    Console.WriteLine("btn"+text);
            //}

            /*
             * Retreive the index of a alphabet
             */
            for(int i=0; i< 26;i++)
            {
                const string chars = "abcdefghijklmnopqrstuvwxyz";
                Console.WriteLine($"{chars[i]} is "+ScrambleString.GetAlphbetIndex(chars[i]));
            }

            /*
             * pause the screen
             */
            Console.ReadKey();
        }
    }
}
