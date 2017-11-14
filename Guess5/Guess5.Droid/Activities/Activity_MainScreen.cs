using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using System.Reactive.Linq;

using ReactiveUI;
//using Guess5.Droid.Helper;
using Guess5.Lib.DataAccessObject;
using Guess5.Lib.Model;

namespace Guess5.Droid.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
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

            this.WireUpControls(); // 

            Intent activity;

            /* activate Start Game Activity when user click the button */
            btnStartGame.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Game));
                activity.PutExtra("Profile_ID", profile_id);
                //StartActivity(activity);
                /* How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
            };

            /* activate Profile Activity when user click the button */
            btnProfile.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Profile));

                /* How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
            };

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            /* How to pass data back from activity without new intent */
            /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */

            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                profile_id = data.GetStringExtra("Profile_ID");

                // Debug
                // Toast.MakeText(this, "ID" + profile_id, ToastLength.Short).Show();

                int id = int.Parse(profile_id);
                ProfileModel profile = ProfileRepository.GetProfile(id);

                textViewProfile.Text = profile.Name;
            }
        }
    }
}