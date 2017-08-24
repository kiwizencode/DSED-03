using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HangmanApp.Shared.Helper
{
    public class WordsHelper
    {
        private readonly string _filename = "5letters.txt";
        protected static WordsHelper _self;
        protected static List<string> _list;
        protected static int[] word_index;
        static int count;

        /// <summary>
        /// The synchronization lock.
        /// </summary>
        static readonly object _lock = new object();

        static WordsHelper()
        {
            _self = new WordsHelper();
        }
        protected WordsHelper()
        {
            GetWordList();

            GenerateRandomNumber();

            count = 0;
        }

        private void GenerateRandomNumber()
        {
            Random random = new Random();
            var range = Enumerable.Range(1, _list.Count).ToList();
            word_index = range.OrderBy(x => random.Next()).ToArray();
        }

        /// <summary>
        /// read words from a text file and update to a list
        /// </summary>
        private void GetWordList()
        {
            _list = new List<string>();

#if __IOS__
            var resourcePrefix = "HangmanApp.iOS.";
#endif
#if __ANDROID__
            var resourcePrefix = "HangmanApp.Droid.";
#endif
#if WINDOWS_PHONE
            var resourcePrefix = "HangmanApp.WinPhone.";
#endif

            var assembly = typeof(WordsHelper).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + _filename);

            using (StreamReader stream_reader = new StreamReader(stream))
            {
                //string line;
                //while ((line = stream_reader.ReadLine()) != null)
                //{
                //    _list.Add(line);
                //}
                for (string line;
                     (line = stream_reader.ReadLine()) != null;
                     _list.Add(line)) ;
            }
        }



        /// <summary>
        /// Return a list of words from a text file.
        /// </summary>
        /// <returns>A list of words</returns>
        static public string [] GetWordsList() { return _list.ToArray(); }
        /// <summary>
        /// generate a number of unqiue letters which include letters of the hidden words
        /// </summary>
        /// <param name="word">the hidden words</param>
        /// <param name="num">number of letters to be generated</param>
        /// <returns></returns>
        public static string GenerateRandomLetter(string hiddenword, int num=15)
        {

            void ProcessLetterBuilder(ref StringBuilder builder, string word)
            {
                for (int x = 0; x < word.Length; x++)
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

            lock (_lock)
            {
                var builder = new StringBuilder();

                ProcessLetterBuilder(ref builder, hiddenword);

                /* How can I generate random alphanumeric strings in C#? 
                 * https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c */ 
                string RandomString(int length = 26)
                {
                    Random random = new Random();
                    string chars = "abcdefghijklmnopqrstuvwxyz";
                    return new string(Enumerable.Repeat(chars, length)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                }

                string random_letters = RandomString();
                int i = 0;
                while (builder.Length < num)
                {
                    ProcessLetterBuilder(ref builder, random_letters[i++].ToString());
                }

                return Shuffle(builder.ToString());



                /* How to shuffle string
                 * https://stackoverflow.com/questions/4739903/shuffle-string-c-sharp */
                string Shuffle(string str)
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
            }

        }

        public static string GetNextWord() { return _list[word_index[count++]]; }

        /*
         *  https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string
         */
        //for (int i = 1; i < 16;i++)
        //{
        //    string i_str = i.ToString();
        //    string text = i_str.PadLeft(2,'0');
        //    Console.WriteLine("btn"+text);
        //}
    }
}
