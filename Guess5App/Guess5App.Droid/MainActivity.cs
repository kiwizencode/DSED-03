using Android;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace Guess5App.Droid
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof (MainActivity).Name;
        Button _button;
        int _clickCount;

        protected override void OnCreate(Bundle savedInstanceState)//Bundle bundle)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _button = FindViewById<Button>(Resource.Id.MyButton);

            _button.Click += (sender, args) =>
                             {
                                 string message = string.Format("You clicked {0} times.", ++_clickCount);
                                 _button.Text = message;
                                 Log.Debug(TAG, message);
                             };

            Log.Debug(TAG, "MainActivity is loaded.");
        }
    }
}