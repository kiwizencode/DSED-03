using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;

using System.Reflection;
using System.IO;
using System.Linq;

namespace GameAppV3
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private string[] words;
        private int[] result;
        private int count;

        ImageView ImageWordSlot01;
        ImageView ImageWordSlot02;
        ImageView ImageWordSlot03;
        ImageView ImageWordSlot04;
        ImageView ImageWordSlot05;


        private Button btnPressMe;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            btnPressMe = FindViewById<Button>(Resource.Id.btnPressMe);

            ImageWordSlot01 = FindViewById<ImageView>(Resource.Id.ImageWordSlot01);
            ImageWordSlot02 = FindViewById<ImageView>(Resource.Id.ImageWordSlot02);
            ImageWordSlot03 = FindViewById<ImageView>(Resource.Id.ImageWordSlot03);
            ImageWordSlot04 = FindViewById<ImageView>(Resource.Id.ImageWordSlot04);
            ImageWordSlot05 = FindViewById<ImageView>(Resource.Id.ImageWordSlot05);

            count = 0;
            words = null;

            btnPressMe.Click += (sender, e) =>
            {
                // read the text file and fill a list
                if (words is null)
                {
                    words = ReadFile("5letters.txt");

                    if (words.Length > 0)
                    {
                        //Toast.MakeText(this, "The Text file is loaded", ToastLength.Short).Show();
                        
                        // generate the random number
                        result = GenerateRandomNumber();
                    }

                }

                if (count + 1 == words.Length) count = 0;


                // display the word in the list each time

                string word = words[result[count++]];
                Toast.MakeText(this, $"The Hidden Word is {word}", ToastLength.Short).Show();

                string[] Letters_List = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


                for(int i = 0; i < word.Length;i++)
                {
                    char ch = word[i];
                    ImageView image;
                    switch(i+1)
                    {
                        case 1: image = ImageWordSlot01; break;
                        case 2: image = ImageWordSlot02; break;
                        case 3: image = ImageWordSlot03; break;
                        case 4: image = ImageWordSlot04; break;
                        case 5: image = ImageWordSlot05; break;
                        default: throw new InvalidDataException("ImageView not defined!!!");
                    }

                    int index = Array.FindIndex(Letters_List, x => x.Contains(ch.ToString().ToUpper()));
                    string resource = "word_" + Letters_List[index];
                    image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
                }



            };
        }

        private int[] GenerateRandomNumber()
        {
            Random random = new Random();
            var range = Enumerable.Range(1, words.Length).ToList();
            int[] result = range.OrderBy(x => random.Next()).ToArray();
            return result;
        }

        private string[] ReadFile(string file_name)
        {
            string[] lines;
            var list = new List<string>();

            Stream stream = Assets.Open("5letters.txt");
            //var file_stream = new FileStream(@file_name, FileMode.Open, FileAccess.Read);
            using (var stream_reader = new StreamReader(stream))
            {
                string line;
                while ((line = stream_reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            lines = list.ToArray();
            return lines;
        }
    }
}

