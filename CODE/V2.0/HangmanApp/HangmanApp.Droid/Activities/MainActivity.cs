using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Runtime.Remoting.Contexts;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "HangmanApp.Droid")] // , MainLauncher = true)]
    public class MainActivity : Activity
    {

        private TextView textViewTitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.layout_main);

            /* using custom fonts in android https://forums.xamarin.com/discussion/62164/using-custom-fonts-in-android  */
            // load custom fonts
            //Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/FFF_Tusj.ttf");

            //textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            //textViewTitle.Typeface = tf;
        }
    }
}

