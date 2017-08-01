using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace GameAppV1
{
    [Activity(Label = "DSED-02 Assignment", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        ImageView ImageWordSlot01;

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

            index = 0;

            btnPressMe.Click += (send, e) =>
            {
                string resource = "word_" + Letter_List[index];

                ImageWordSlot01.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));

                if (++index == Letter_List.Length)
                    index = 0;
            };
        }
    }
}

