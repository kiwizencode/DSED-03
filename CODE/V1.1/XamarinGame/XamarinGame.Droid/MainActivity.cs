using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace XamarinGame.Droid
{
    [Activity(Label = "XamarinGame.Droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button btnStartGame;
        Button btnScores;
        Button btnProfile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main_layout);

            btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);
            btnStartGame.Click += delegate
            {
                var newactivity = new Intent(this, typeof(GameActivity));

                newactivity.PutExtra("Activity_Name", "Game Activity");
                StartActivity(newactivity);

            };

            btnProfile = FindViewById<Button>(Resource.Id.btnProfile);
            btnProfile.Click += delegate
            {
                var newactivity = new Intent(this, typeof(ProfileActivity));

                newactivity.PutExtra("Activity_Name", "Profile Activity");
                StartActivity(newactivity);
            };


            btnScores = FindViewById<Button>(Resource.Id.btnScores);
            btnScores.Click += delegate
            {
                var newactivity = new Intent(this, typeof(ScoresActivity));

                newactivity.PutExtra("Activity_Name", "Scores Activity");
                StartActivity(newactivity);
            };
        }
    }
}

