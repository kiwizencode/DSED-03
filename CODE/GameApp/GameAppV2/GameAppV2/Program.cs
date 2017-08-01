using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;


namespace GameAppV2
{
    public class Program
    {
        private static Random random = new Random();
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        static void Main(string[] args)
        {
            string[] lines = ReadFile("5letters.txt");
            Console.WriteLine($"count : {lines.Length}\n");

            //string text = RandomString(5);
            //Console.WriteLine(text);

            if(lines.Length > 0)
                Console.WriteLine($"Random number is {GenerateRandomNumber(lines)[0]}");
            Console.ReadKey();
        }

        /*
         *  Read in a file
         */
        private static string [] ReadFile(string fileName)
        {
            string[] lines;
            var list = new List<string>();
            var file_stream = new FileStream(@fileName, FileMode.Open, FileAccess.Read);
            using (var stream_reader = new StreamReader(file_stream, Encoding.UTF8))
            {
                string line;
                while ((line = stream_reader.ReadLine()) != null)
                {
                    list.Add(line);
                    //Console.WriteLine($"{line}");
                }
            }
            lines = list.ToArray();
            return lines;
            //
        }

        public static int[] GenerateRandomNumber(string[] array)
        {
            var range = Enumerable.Range(1, array.Length).ToList();
            int [] result = range.OrderBy(x => random.Next()).ToArray();
            return result;
            //return array[random.Next(array.Length)];

        }

        public static string RandomWord(string [] array)
        {
            return array[random.Next(array.Length)];
        }

        public static byte RandomByteNum(byte MaxCount)
        {
            byte[] num = new byte[1];

            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(num);
            }

            while (!IsFairRoll(num[0], MaxCount));
            // Return the random number mod the number
            // of sides.  The possible values are zero-
            // based, so we add one.
            return (byte)((num[0] % MaxCount) + 1);
        }

        private static bool IsFairRoll(byte roll, byte maxCount)
        {
            // There are MaxValue / numSides full sets of numbers that can come up
            // in a single byte.  For instance, if we have a 6 sided die, there are
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
            int fullSetsOfValues = Byte.MaxValue / maxCount;

            // If the roll is within this range of fair values, then we let it continue.
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
            // < rather than <= since the = portion allows through an extra 0 value).
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
            // to use.
            return roll < maxCount * fullSetsOfValues;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
