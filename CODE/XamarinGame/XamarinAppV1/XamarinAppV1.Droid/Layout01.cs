using System;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinAppV1.Models;

namespace XamarinAppV1.Droid
{
    [Activity(Label = "@string/App_Name", MainLauncher = true, Icon = "@drawable/icon")]
    public class Layout01 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout01);

            /*
             * Unit-testing
             */
            string word = GetGuessWord();

            InitalizeImageWordSlot(word);

            GenerateKeyBoard(word);
        }

        private void InitalizeImageWordSlot(string word)
        {
            for (int i = 0; i < word.Length; )
            {
                char ch = word[i++];
                
                /*
                 * Get the button using the resource id
                 */
                string res_name = "ImageWordSlot" + i.ToString().PadLeft(2,'0');
                int res_id = Resources.GetIdentifier(res_name, "id", PackageName);
                ImageView image = FindViewById<ImageView>(res_id);

                int index = ScrambleString.GetAlphbetIndex(ch);
                string resource = "word_" + ch.ToString().ToUpper();
                image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
            }
        }

        private string GetGuessWord()
        {
            string [] words = GuessWords.ReadFile();
            return words[GenerateRandomNumber(words.Length)];
        }

        private int GenerateRandomNumber(int count)
        {
            Random random = new Random();
            var range = Enumerable.Range(1, count).ToList();
            int[] result = range.OrderBy(x => random.Next()).ToArray();
            return result[0];
        }

        private void GenerateKeyBoard(string word)
        {
            //string word = "heels";
            string scarble_word = ScrambleString.GenerateRandomLetter(word, 15);

            /*
             *  https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string
             */
            // Iterate through all 15 button
            for (int i = 1; i < 16; i++)
            {
                //string i_str = i.ToString();
                //string text = i_str.PadLeft(2, '0');
                string text = i.ToString().PadLeft(2, '0');
                SetButtonImage(i, text, scarble_word[i-1].ToString());
            }
        }

        private void SetButtonImage(int i, string text, string letter)
        {
            /*
             * Get the button using the resource id
             */
            string resource = "btn" + text;
            int res_id  = Resources.GetIdentifier(resource, "id", PackageName);
            var button = FindViewById<Button>(res_id);

            /*
             * Get the image base on the words
             */
            string image = "word_" + letter.ToUpper();
            button.SetBackgroundResource((int)typeof(Resource.Drawable).GetField(image).GetValue(null));
        }


    }
}