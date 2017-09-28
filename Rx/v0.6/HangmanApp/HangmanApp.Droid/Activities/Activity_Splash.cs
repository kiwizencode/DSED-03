using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

using Android.Content;

using System.Threading.Tasks;

namespace HangmanApp.Droid.Activities // Guess5App.Droid
{
    //[Activity(Label = "Splast Activity", MainLauncher = true)]
    [Activity(Theme = "@style/MyTheme.Splash", NoHistory = true)] //, MainLauncher = true, NoHistory = true)]
    public class Activity_Splash : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(Activity_Splash).Name;


        /*
         https://alexdunn.org/2017/02/07/creating-a-splash-page-for-xamarin-forms-android/
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var startUp = new Task(() =>
            {
                Task.Delay(2000);
                var intent = new Intent(this, typeof(Activity_MainScreen));
                StartActivity(intent);
            });
            
            startUp.ContinueWith(t => Finish());            
            startUp.Start();
        }

        //public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        //{
        //    base.OnCreate(savedInstanceState, persistentState);
        //    Log.Debug(TAG, "SplashActivity.OnCreate");
        //}

        //// Launches the startup task
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Task startupWork = new Task(() => { SimulateStartup(); });
        //    startupWork.Start();
        //}

        //// Prevent the back button from canceling the startup process
        //public override void OnBackPressed() { }

        //// Simulates background work that happens behind the splash screen
        //async void SimulateStartup()
        //{
        //    Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
        //    //await Task.Delay(8000); // Simulate a bit of startup work.
        //    await Task.Delay(3000);
        //    Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
        //    StartActivity(new Intent(Application.Context, typeof(MainActivity)));

        //    //Finish();
        //}

    }
}

