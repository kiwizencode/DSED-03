using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace HangmanApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test01();
        }

        /*
         * Generate list of random letters which include letter in the hidden word
         */
        static void Test01()
        {
            string hidden_word = "heels";
            string chars = "abcdefghijklmnopqrstuvwxyz";
            List<char> char_list = new List<char>();

            Random random = new Random();
            var builder = new StringBuilder();

            /* http://introtorx.com/Content/v1.0.10621.0/05_Filtering.html#Distinct */

            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            distinct.Subscribe(i => {

                /* How to convert integer to char in C?
                 * https://stackoverflow.com/questions/2279379/how-to-convert-integer-to-char-in-c */

                char ch = ((char)i); // 
                char_list.Add(ch);
            });

                void GenerateUniqueLetter(string list)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        char ch = list[i];
                        subject.OnNext(ch);
                    }
                }

            GenerateUniqueLetter(hidden_word);

            /* Best way to randomize an array with .NET
             * https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net */
            //string temp = chars.OrderBy(x => random.Next()).ToArray();
            GenerateUniqueLetter(new string(chars.OrderBy(x => random.Next()).ToArray()));

            string temp_list = new string(char_list.ToArray());
            Console.WriteLine(temp_list.Substring(0,15).OrderBy(x => random.Next()).ToArray());
            Console.Read();

        }
    }
}
