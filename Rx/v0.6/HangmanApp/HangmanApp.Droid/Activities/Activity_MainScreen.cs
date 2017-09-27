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
using HangmanApp.Shared.DataAccessObject;
using HangmanApp.Shared.Model;

//using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Main Screen")] //, MainLauncher = true)]
    public class Activity_MainScreen : ReactiveActivity
    {
        public Button btnStartGame { get; private set; }
        public Button btnScores { get; private set; }
        public Button btnProfile { get; private set; }
        public Button btnCredits { get; private set; }

        public TextView textViewTitle { get; private set; }
        public TextView textViewProfile { get; private set; }


        private string profile_id = string.Empty;

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
            textViewTitle.Typeface =  FontsHelper.Title_Font;

            /* v0.6  change to new way to initialize the font on the button  */
            //FontsHelper.SetupButton(this, btnStartGame, Resource.Id.btn01);
            //FontsHelper.SetupButton(this, btnScores, Resource.Id.btn02);
            //FontsHelper.SetupButton(this, btnProfile, Resource.Id.btn03);
            //FontsHelper.SetupButton(this, btnCredits, Resource.Id.btn04);

            FontsHelper.SetupButtonFont(btnStartGame);
            FontsHelper.SetupButtonFont(btnScores);
            FontsHelper.SetupButtonFont(btnProfile);
            FontsHelper.SetupButtonFont(btnCredits);

            /*  */


            Intent activity;

            /* activate Start Game Activity when user click the button */
            btnStartGame.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Game));
                activity.PutExtra("Profile_ID", profile_id);
                //StartActivity(activity);
                /* v0.6 How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
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
                //StartActivity(activity);
                /* v0.6 How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            /* v0.6 How to pass data back from activity without new intent */
            /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */

            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                //string 
                profile_id = data.GetStringExtra("Profile_ID");
                //stringRetFromResult should hold now the value of 'Hello from the Second Activity!'
                Toast.MakeText(this, "ID" + profile_id, ToastLength.Short).Show();

                int id = int.Parse(profile_id);
                Model_Profile profile = ProfileRepository.GetProfile(id);

                textViewProfile.Text = profile.Name;
            }
        }
    }
}