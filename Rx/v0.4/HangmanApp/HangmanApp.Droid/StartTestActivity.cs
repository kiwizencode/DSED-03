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

using HangmanApp.Droid.Activities;

namespace HangmanApp.Droid
{
    /*
     * use this to test unctionality of individual activity
     */
    [Activity(Label = "HangmanApp")]
    //[Activity(Label = "HangmanApp", MainLauncher = true)]
    public class StartTestActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Intent activity = new Intent(this, typeof(Activity_Game));
            StartActivity(activity);

            this.Finish();
        }
    }
}