﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive;
using System.Reactive.Subjects;

namespace HangmanApp.Test
{
    class Program
    {

        static void Main(string[] args)
        {

            /* http://www.introtorx.com/Content/v1.0.10621.0/04_CreatingObservableSequences.html#ObservableTimer  */
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(1000));
            interval.Subscribe(
               i =>
               {
                   long num = i % 10;
                   Console.WriteLine(num);
               },
                () => Console.WriteLine("completed"));
            //interval.Subscribe(
            //            Console.WriteLine,
            //            () => Console.WriteLine("completed"));

            Console.ReadKey();
        }

        static void Main2(string[] args)
        {
            /* generate a string of unique letter, which will include letter of the hidden words  */

            /* Test whether able to use reactive to achive such task */

            // test whether program ignore duplicate letter in the hidden word
            //string hidden_word = "heels";

            //var range = Observable.Range(10, 15);
            //range.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));

            //var range = MyRange(10, 15);
            var range = AlphabetGenerator();
            //range.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));

            Random random = new Random();

            /* Do 5 times */


            List<char> list = new List<char>();

            /* http://introtorx.com/Content/v1.0.10621.0/05_Filtering.html#Distinct */

            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            distinct.Subscribe(i => {
                char ch = ((char)i);
                //Console.WriteLine($"{ch}");
                list.Add(ch);
            });

            for (int i = 0; list.Count < 16; i++)
            {
                int start = 'a';
                int end = 'z';
                int ch = random.Next(start, end + 1);
                //Console.WriteLine($"{((char)ch)}");
                subject.OnNext(ch);
            }
            //subject.OnCompleted();

            string random_letters = new string(list.ToArray());
            //Console.WriteLine();
            //
            //for(int i = 0; i < list.Count; i++)
            //    Console.Write(list[i] + " ");
            //Console.WriteLine();

            Console.WriteLine(random_letters);


            Console.ReadKey();

        }

        public static IObservable<char> AlphabetGenerator()
        {
            char start = 'a';
            int count = start + 26;
            char max =  ((char)count);
            return Observable.Generate( start,
                                        value => value < max,
                                        value => ((char)(value + 1)),
                                        value => value);
        }

        public static IObservable<int> MyRange(int start, int count)
        {
            var max = start + count;
            return Observable.Generate(
            start,
            value => value < max,
            value => value + 1,
            value => value);
        }
    }
}