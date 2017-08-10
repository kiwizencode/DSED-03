using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinAppV1.Droid
{
    [Activity(Label = "XamarinAppV1.Droid")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
        }
    }
}

