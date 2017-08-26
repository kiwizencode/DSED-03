using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ReactiveUI;
//using HangmanApp.Droid.ViewModel;
using System.Reactive.Linq;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "HangmanApp")] //, MainLauncher = true)]
    public class Activity_MainScreen : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_MainScreen);

            Initializer();
        }

        private void Initializer()
        {
            Intent activity;

            /*
             * bring up Start Game Activity when user click the button
             */
            FindViewById<Button>(Resource.Id.btnStartGame).Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Game));
                StartActivity(activity);
            };
        }
    }
}