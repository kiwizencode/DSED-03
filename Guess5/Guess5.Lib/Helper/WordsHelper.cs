using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;

namespace Guess5.Lib.Helper
{
    public class WordsHelper
    {
        private readonly string _filename = "5letters_v2.txt";
        
        protected static WordsHelper _self;
        protected static List<string> _list;
        protected static int[] word_index;

        static int counter;
        //static readonly int length = 26;

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
        /* 
         * Working with files in Xamarin multi-platform environment
         * https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/files/ 
         */ 
        private void GenerateWordsList()
        {
            _list = new List<string>();
            var resourcePrefix = "Guess5.Droid.";
            #if __IOS__
            resourcePrefix = "Guess5.iOS.";
            #endif
            #if __ANDROID__
            resourcePrefix = "Guess5.Droid.";
            #endif
            #if WINDOWS_PHONE
            resourcePrefix = "Guess5.WinPhone.";
            #endif

            var assembly = typeof(WordsHelper).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + _filename);
            using (StreamReader stream_reader = new StreamReader(stream))
            { 
                for ( string word; // declare a varable to store the word
                     // read word by word from the text file
                     ( word = stream_reader.ReadLine() ) != null; 
                     // until the end of the file. word == null => no more word
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
            lock (_lock)
            {
                string alphabet_chars = "abcdefghijklmnopqrstuvwxyz";
                List<char> list_letters = new List<char>();
                Random random = new Random();
                var builder = new StringBuilder();

                /* Not happy with the code found in folowing URI
                 *  How can I generate random alphanumeric strings in C#? 
                 * https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c 
                 * Problem is that it do not generate unique letter in the string each time
                 * 
                 * Come up with a solution using reactive :
                 * 
                 * 1. creat a empty list called builder.
                 * 2. add the letter from hidden word into the list, remove any duplicate 
                 * 3. randomize a list of alphbet and add it into the list called builder
                 * 4. randomize the list called builder. 
                 */

                /* http://introtorx.com/Content/v1.0.10621.0/05_Filtering.html#Distinct */

                /* Using the following to pick up unique letter in the stream. */
                var subject = new Subject<int>();
                var distinct = subject.Distinct();
                distinct.Subscribe(i => {

                    /* How to convert integer to char in C?
                     * https://stackoverflow.com/questions/2279379/how-to-convert-integer-to-char-in-c */

                    char ch = ((char)i); // 
                    list_letters.Add(ch);
                });


                    /* refactor the code such that the function will add a charactor to the stream at a time from string pass throught*/ 
                    void GenerateUniqueLetter(string list)
                    {
                        for (int i = 0; i < list.Length; i++)
                        {
                            char ch = list[i];
                            subject.OnNext(ch);
                        }
                    }

                /* add letter from hidden word to builder list */
                GenerateUniqueLetter(hiddenword);

                /* Best way to randomize an array with .NET
                 * https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
                   
                   string temp = chars.OrderBy(x => random.Next()).ToArray();  */

                /* add letter from a randomized alpahbet list into the builder list */
                GenerateUniqueLetter(new string(alphabet_chars.OrderBy(x => random.Next()).ToArray()));

                /* because the letter from the hidden word is added in the front of the builder list and the rest of letter at the back.
                   pick the first few charactor [0..num] from the builder list and randomize it. 
                   num => the total length of letters to be returned. */
                string random_letters = new string(list_letters.ToArray());

                return new string(random_letters.Substring(0, num).OrderBy(x => random.Next()).ToArray());

            }

        }
        /// <summary>
        /// return the next hidden word
        /// </summary>
        /// <returns></returns>
        public static string GetNextWord() {
            string text = _list[word_index[counter++]];
            /* The following code ensure that when the counter reach the last word in the list, 
               it will reset bask to zero and restart from the bigining of the list    */
            counter = counter % word_index.Length; 
            return text;
        }


        /// <summary>
        /// return the score value for the letter
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int GetScore(char ch)
        {
            switch(ch)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'l':
                case 'n':
                case 'o':
                case 'r':
                case 's':
                case 't':
                case 'u': return 1;

                case 'd':
                case 'g': return 2;

                case 'b':
                case 'c':
                case 'm':
                case 'p': return 3;

                case 'f':
                case 'h':
                case 'v':
                case 'w':
                case 'y': return 4;

                case 'k': return 5;

                case 'j':
                case 'x': return 8;

                case 'q':
                case 'z': return 10;

                default: throw new InvalidDataException("undefine score value in GetScore function");
            }
            
        }
    }
}
