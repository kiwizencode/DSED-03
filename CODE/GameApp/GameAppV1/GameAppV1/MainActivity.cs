using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace GameAppV1
{
    [Activity(Label = "DSED-02 Assignment GameAppV1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        ImageView ImageWordSlot01;

        ImageView ImageTile01;
        ImageView ImageTile02;
        ImageView ImageTile03;
        ImageView ImageTile04;
        ImageView ImageTile05;
        ImageView ImageTile06;

        Button btnPressMe;


        private string[] Letter_List = { "A", "B", "C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        private int index;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //
            InitializeControl();
        }


        private void InitializeControl()
        {
            ImageWordSlot01 = FindViewById<ImageView>(Resource.Id.ImageWordSlot01);

            btnPressMe = FindViewById<Button>(Resource.Id.btnPressMe);
            ImageTile01 = FindViewById<ImageView>(Resource.Id.ImageTile01);
            ImageTile02 = FindViewById<ImageView>(Resource.Id.ImageTile02);
            ImageTile03 = FindViewById<ImageView>(Resource.Id.ImageTile03);
            ImageTile04 = FindViewById<ImageView>(Resource.Id.ImageTile04);
            ImageTile05 = FindViewById<ImageView>(Resource.Id.ImageTile05);
            ImageTile06 = FindViewById<ImageView>(Resource.Id.ImageTile06);

            index = 0;
            char ch = 'B';
            btnPressMe.Click += (send, e) =>
            {
                string resource = "word_" + Letter_List[index];

                ImageWordSlot01.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));

                //if (++index == Letter_List.Length)
                //    index = 0;

                index = ++index % 26;
            };
            //{
            //    if(index == 6)
            //    {
            //        index = 0;
            //        ch = (ch == 'B' ? 'A' : 'B');
            //    }

            //    ImageView image;

            //    switch(index + 1)
            //    {
            //        case 1: image = ImageTile01;  break;
            //        case 2: image = ImageTile02; break;
            //        case 3: image = ImageTile03; break;
            //        case 4: image = ImageTile04; break;
            //        case 5: image = ImageTile05; break;
            //        case 6: image = ImageTile06; break;
            //        default: throw new InvalidOperationException("ImageView not defined !!!");
            //    }

            //    string resource = "hangman_tile_0"+ ++index + "_" + ch;
            //    image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
            //};
        }
    }
}

