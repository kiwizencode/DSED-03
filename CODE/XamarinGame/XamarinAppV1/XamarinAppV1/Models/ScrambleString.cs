using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAppV1.Models
{

    /*
     * This class scramble the letter randomly.
     * The code has been modified from above URL
     */

    public class ScrambleString
    {
        /// <summary>
        /// The synchronization lock.
        /// </summary>
        static private object _lock = new object();

        private static Random random = new Random();
        public static string GenerateRandomLetter(string word, int num)
        {
            lock (_lock)
            {
                var builder = new StringBuilder();

                ProcessWordBuilder(ref builder, word);

                string random_letters = RandomString();
                int i = 0;
                while(builder.Length < num)
                {
                    ProcessWordBuilder(ref builder, random_letters[i++].ToString());
                }

                return Shuffle(builder.ToString());
            }
        }

        private static void ProcessWordBuilder(ref StringBuilder builder, string word)
        {
            for(int x=0; x< word.Length;x++)
            {
                char ch = word[x];
                bool flag = false;
                for (int i = 0; i < builder.Length; i++)
                {
                    if (builder[i].Equals(ch))
                    {
                        flag = true;
                        break;
                    }

                }
                if (!flag)
                {
                    builder.Append(ch);
                }
            }
        }

        // https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        private static string RandomString(int length=26)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // https://stackoverflow.com/questions/4739903/shuffle-string-c-sharp

        private static string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

        public static int GetAlphbetIndex(char ch)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return chars.Contains(ch.ToString()) ? chars.IndexOf(ch) : -1;
        }
    }
}
