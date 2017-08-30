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

//using HangmanApp.Droid.ViewModel;
using System.Reactive.Linq;
using Android.Graphics;

using ReactiveUI;
using HangmanApp.Droid.Helper;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "HangmanApp", MainLauncher = true)]
    public class Activity_MainScreen : ReactiveActivity
    {
        public Button btnStartGame { get; private set; }
        public Button btnScores { get; private set; }
        public Button btnProfile { get; private set; }
        public Button btnCredits { get; private set; }

        public TextView textViewTitle { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_MainScreen);

            Initializer();
        }

        private void Initializer()
        {

            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // v0.4 => added this code

            /* Set the font for the activity title bar*/
            textViewTitle.Typeface = Typeface.CreateFromAsset(Assets, FontsHelper.Title_Font);

            FontsHelper.SetupButton(this, btnStartGame, Resource.Id.btn01);
            FontsHelper.SetupButton(this, btnScores, Resource.Id.btn02);
            FontsHelper.SetupButton(this, btnProfile, Resource.Id.btn03);
            FontsHelper.SetupButton(this, btnCredits, Resource.Id.btn04);

            Intent activity;

            /* activate Start Game Activity when user click the button */
            btnStartGame.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Game));
                StartActivity(activity);
            };

            /* activate Profile Activity when user click the button */
            btnScores.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Scores));
                StartActivity(activity);
            };

            /* activate Profile Activity when user click the button */
            btnProfile.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Profile));
                StartActivity(activity);
            };
        }
    }
}