using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace GameAppV4
{
    /*
     * https://stackoverflow.com/questions/36625891/create-a-unique-5-character-alphanumeric-string
     */
    public sealed class Program
    {
        /// <summary>
        /// The synchronization lock.
        /// </summary>
        static private object _lock = new object();

        /// <summary>
        /// The cryptographically-strong random number generator.
        /// </summary>
        static private RNGCryptoServiceProvider _crypto = new RNGCryptoServiceProvider();


        static void Main(string[] args)
        {
            string word = "heels";

            Console.WriteLine();

            Console.WriteLine($"Random word is {word}");

            Console.WriteLine($"Random List is {GenerateRandomLetter(word,15)}");

            Console.ReadKey();
        }

        private static string GenerateRandomLetter(string word, int num)
        {
            
            lock (_lock)
            {
                Random rnd = new Random();
                var builder = new StringBuilder(word);

                while (builder.Length < num)
                {
                    char ch = Char.ToLower(GetRandomUppercaseAphanumericCharacter());
                    bool flag = false;
                    for(int i = 0; i < builder.Length;i++)
                    {
                        if (builder[i].Equals(ch))
                        {
                            flag = true;
                            break;
                        }
                            
                    }
                    if(!flag)
                    {
                        builder.Append(ch);
                    }
                }

                //return builder.ToString().ToLower();

                return new string(builder.ToString().ToLower().OrderBy(x => rnd.Next()).ToArray());
            }
        }

        /// <summary>
        /// Return a string of the provided length comprised of only uppercase alpha-numeric characters each of which are
        /// selected randomly.
        /// </summary>
        /// <param name="ofLength">The length of the string which will be returned.</param>
        /// <returns>Return a string of the provided length comprised of only uppercase alpha-numeric characters each of which are
        /// selected randomly.</returns>
        static public string GetRandomUppercaseAlphaNumericValue(int ofLength)
        {
            lock (_lock)
            {
                var builder = new StringBuilder();

                for (int i = 1; i <= ofLength; i++)
                {
                    builder.Append(GetRandomUppercaseAphanumericCharacter());
                }

                return builder.ToString().ToLower();
            }
        }


        /// <summary>
        /// Return a randomly-generated uppercase alpha-numeric character (A-Z or 0-9).
        /// </summary>
        /// <returns>Return a randomly-generated uppercase alpha-numeric character (A-Z or 0-9).</returns>
        static private char GetRandomUppercaseAphanumericCharacter()
        {
            //var possibleAlphaNumericValues =
            //    new char[]{'A','B','C','D','E','F','G','H','I','J','K','L',
            //    'M','N','O','P','Q','R','S','T','U','V','W','X','Y',
            //    'Z','0','1','2','3','4','5','6','7','8','9'};

            var possibleAlphaNumericValues =
                new char[]{'A','B','C','D','E','F','G','H','I','J','K','L',
                'M','N','O','P','Q','R','S','T','U','V','W','X','Y',
                'Z'};
            return possibleAlphaNumericValues[GetRandomInteger(0, possibleAlphaNumericValues.Length - 1)];
        }


        /// <summary>
        /// Return a random integer between a lower bound and an upper bound.
        /// </summary>
        /// <param name="lowerBound">The lower-bound of the random integer that will be returned.</param>
        /// <param name="upperBound">The upper-bound of the random integer that will be returned.</param>
        /// <returns> Return a random integer between a lower bound and an upper bound.</returns>
        static private int GetRandomInteger(int lowerBound, int upperBound)
        {
            uint scale = uint.MaxValue;

            // we never want the value to exceed the maximum for a uint, 
            // so loop this until something less than max is found.
            while (scale == uint.MaxValue)
            {
                byte[] fourBytes = new byte[4];
                _crypto.GetBytes(fourBytes); // Get four random bytes.
                scale = BitConverter.ToUInt32(fourBytes, 0); // Convert that into an uint.
            }

            var scaledPercentageOfMax = (scale / (double)uint.MaxValue); // get a value which is the percentage value where scale lies between a uint's min (0) and max value.
            var range = upperBound - lowerBound;
            var scaledRange = range * scaledPercentageOfMax; // scale the range based on the percentage value
            return (int)(lowerBound + scaledRange);
        }
    }
}
