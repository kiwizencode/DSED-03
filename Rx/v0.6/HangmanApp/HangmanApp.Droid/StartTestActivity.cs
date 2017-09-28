using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

using HangmanApp.Droid.Activities;

namespace HangmanApp.Droid
{
    /*
     * use this to test unctionality of individual activity
     */
    //[Activity(Label = "HangmanApp")]
    [Activity(Label = "HangmanApp", MainLauncher = true, Theme = "@style/MyTheme")]
    public class StartTestActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            //Intent activity = new Intent(this, typeof(Activity_Game));
            Intent activity = new Intent(this, typeof(Activity_Splash));
            StartActivity(activity);

            this.Finish();
        }
    }
}