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
            //Test01();

            // Test02();

            Test03();
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


        /*
         * geneate sequence
         */
        static void Test02()
        {
            //var oddNumbers = Observable.Range(1, 10)
            //    .Subscribe(
            //    Console.WriteLine,
            //    () => Console.WriteLine("Completed"));

            /*
             * Count down from 10 to 1 and repeat infinite loop
             */


            //int start = 10;
            //var count_down = Observable.Generate(
            //    start,
            //    value => value >= 0,
            //    value => value - 1,
            //    value => value).Repeat().Subscribe(
            //    Console.WriteLine,
            //    () => Console.WriteLine("Completed"));

            //Console.ReadKey();

            /* Repeat count down from 20 to 0 repeatedly*/

            var interval = Observable.Interval(
                TimeSpan.FromMilliseconds(1000));
           interval.Subscribe(
                x => Console.WriteLine(20 - x%20),
                () => Console.WriteLine("completed"));

            Console.ReadKey();
        }

        static void Test03()
        {
            string hidden_word = "heels";
            char ch = 'e';
            /* count the number of a particular letter in hidden word */
            //int count = 0;
            //foreach (var letter in hidden_word)
            //    if (letter == ch) count++;

            //Console.WriteLine($"Count for {ch} is {count}");
            //Console.ReadKey();

            /* get the location of the letter in the hidden word */
            List<int> letter_index = new List<int>();
            for (int i = 0; i < hidden_word.Length; i++)
                if (hidden_word[i] == ch) letter_index.Add(i);

            Console.WriteLine($"Index for {ch} is {letter_index.Count}");
            Console.ReadKey();
        }
    }
}
