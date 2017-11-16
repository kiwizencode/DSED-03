using System;
using System.Collections.Generic;
using System.Text;

using ReactiveUI;

using Android.App;
using Android.Graphics;
using Android.Widget;

using Guess5.Droid.Activities;

namespace Guess5.Droid.Helper
{
    public class FontsHelper
    {
        protected static FontsHelper _self;

        public static Typeface Title_Font { get; set; }

        public static Typeface Digital_Font { get; set; }

        static FontsHelper()
        {
            _self = new FontsHelper();

        }

        protected FontsHelper() {

            /* https://stackoverflow.com/questions/43279971/get-current-activity-xamarin-android */
            //var activity = ((Activity)Xamarin.Forms.Forms.Context);
            //(Activity_MainScreen)Forms.Context;

            /* https://forums.xamarin.com/discussion/11102/what-is-equivalent-to-getapplicationcontext-in-xamarin-android  */
            var activity = Application.Context;

            //Title_Font = Typeface.CreateFromAsset(activity.Assets, "fonts/FFF_Tusj.ttf");
            //Digital_Font = Typeface.CreateFromAsset(activity.Assets, "fonts/Digital-Dismay.otf");
        }

       public static void SetupButtonFont(Button btn)
        {
            btn.Typeface = Title_Font;
        }
    }
}
