using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
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

        static int counter;
        static readonly int length = 26;

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
            GenerateWordsList();

            ShuffleWordsList();

            counter = 0;

        }
        /// <summary>
        /// reshuffle the words in the list by reshuffle the word index in another list called word_index and then retreive the word by word form the new list.
        /// </summary>
        private void ShuffleWordsList()
        {
            Random random = new Random();
            var range = Enumerable.Range(1, _list.Count).ToList();
            word_index = range.OrderBy(x => random.Next()).ToArray();
        }

        /// <summary>
        /// read words from a text file and update to a list
        /// </summary>
        ///
        /* Working with files in Xamarin multi-platform environment
         * https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/files/ */ 
        private void GenerateWordsList()
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
                for ( string word; // declare a varable to store the word
                     // read word by word from the text file
                     ( word = stream_reader.ReadLine() ) != null; // until the end of the file. word == null => no more word
                     _list.Add(word)) ; // add the word into the list
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
                        if (builder[i].Equals(ch))
                        {
                            flag = true;
                            break;
                        }

                    if (!flag)  builder.Append(ch);
                }
            }

            lock (_lock)
            {
                string chars = "abcdefghijklmnopqrstuvwxyz";

                Random random = new Random();
                var builder = new StringBuilder();

                ProcessLetterBuilder(ref builder, hiddenword);

                /* change in v0.3*/
                /* Not happy with the code found in folowing URI
                 *  How can I generate random alphanumeric strings in C#? 
                 * https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c 
                 * Problem does not generate unique letter in the string each time
                 * 
                 * Come up with a solution using reactive :
                 * 
                 * 1. create a temporary list of char.
                 * 2. generate a sequence of unique letter using Random seed generator
                 * 3. store into the temporary list.
                 * 4. convert the list of char into string object
                 * 
                 */
                    string GenerateRandomLetterSequence()
                    {
                        List<char> char_list = new List<char>();

                        /* http://introtorx.com/Content/v1.0.10621.0/05_Filtering.html#Distinct */
                        var subject = new Subject<int>();
                        var distinct = subject.Distinct();
                        distinct.Subscribe(i => {

                            /* How to convert integer to char in C?
                             * https://stackoverflow.com/questions/2279379/how-to-convert-integer-to-char-in-c */

                            char ch = ((char)i); // 
                            char_list.Add(ch);
                        });
                        int start = 'a', end = 'z';
                        for (int i = 0; char_list.Count <= num; i++)
                        {
                            int ch = random.Next(start, end + 1);
                            subject.OnNext(ch);
                        }

                        return new string(char_list.ToArray());
                    }


                string random_letters = GenerateRandomLetterSequence();

                for ( int i = 0; builder.Length < num; i++)
                {
                    ProcessLetterBuilder(ref builder, random_letters[i].ToString());
                }

                return Shuffle(builder.ToString());


                    /* How to shuffle string
                     * https://stackoverflow.com/questions/4739903/shuffle-string-c-sharp */
                    string Shuffle(string str)
                    {
                        char[] array = str.ToCharArray();
                        //Random rng = new Random();
                        for( int n = array.Length; n-- > 1;)
                        {
                            int k = random.Next(n + 1);
                            var value = array[k];
                            array[k] = array[n];
                            array[n] = value;
                        }
                        return new string(array);
                    }
            }

        }

        public static string GetNextWord() {
            string text = _list[word_index[counter++]];
            /* The following code ensure that when the counter reach the last word in the list, it will reset bask to zero and restart from the bigining of the list    */
            counter = counter % word_index.Length; 
            return text; }

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
