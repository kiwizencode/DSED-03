using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

using Android.Content;

using Guess5App.Droid.Activities;

using System.Threading.Tasks;

namespace Guess5App.Droid
{
    /*  Creating a Splash Page for Xamarin.Forms Android
        https://alexdunn.org/2017/02/07/creating-a-splash-page-for-xamarin-forms-android/ */

    //[Activity(Label = "Splast Activity", MainLauncher = true)]
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    //  The NoHistory flag which disables the back button from going back to this activity, it will destroy itself on leaving.
    public class SplashActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var startUp = new Task(() =>
            {
                var intent = new Intent(this, typeof(Activity_Game));
                StartActivity(intent);
            });
            startUp.ContinueWith(t => Finish());

            startUp.Start();
        }

    }
}

