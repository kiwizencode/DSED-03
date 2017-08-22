using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace HangmanApp.Droid
{
    [Activity(Label = "HangmanApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button button1;
        Button button2;

        TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Initialize();
        }

        private void Initialize()
        {
            string text = Intent.GetStringExtra("ActivityName") ?? "Activity 1";

            int num = int.Parse(text.Substring(text.Length - 1));

            textView = FindViewById<TextView>(Resource.Id.textView);
            textView.Text = text;

            button1 = FindViewById<Button>(Resource.Id.button1);
            button2 = FindViewById<Button>(Resource.Id.button2);


            int count = 1;
            for (int i = 1; i <= 3; i++ )
                if( i != num)
                    switch(count++)
                    {
                        case 1: SetButtonClick(button1, "Activity "+i); break;
                        case 2: SetButtonClick(button2, "Activity " + i); break;
                    }

        }

        private void SetButtonClick(Button button, string text)
        {
            int num = int.Parse(text.Substring(text.Length-1));

            button.Text = "Start " + text;
            button.Click += delegate {
                var activity = new Intent(this, typeof(MainActivity));
                activity.PutExtra("ActivityName", text);

                StartActivity(activity);
                this.Finish();
            };
        }
    }
}

