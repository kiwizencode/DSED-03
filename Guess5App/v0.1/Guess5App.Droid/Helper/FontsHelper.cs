using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;

using ReactiveUI;
using Android.Graphics;
using Android.App;


using Guess5App.Droid.Activities;

namespace Guess5App.Droid.Helper
{
    /* added in v0.4 */
    public class FontsHelper
    {
        //private readonly string _Game_Font = "fonts/FFF_Tusj.ttf";
        // private readonly string _Digital_Font = "fonts/Digital-Dismay.otf";

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

            Title_Font = Typeface.CreateFromAsset(activity.Assets, "fonts/FFF_Tusj.ttf");
            Digital_Font = Typeface.CreateFromAsset(activity.Assets, "fonts/Digital-Dismay.otf");
        }

        /* changed in v0.4*/
        /// <summary>
        /// set the image of ImageView based on the input resource
        /// 
        /// </summary>
        /// <param name="id">ImageView resource id</param>
        /// <param name="resource">Image file name</param>
        //private void SetImageView(int id, string resource)
        public static void SetImageView(ReactiveActivity activity, int id, string resource)
        {
            ImageView image = activity.FindViewById<ImageView>(id);

            /* How to change the ImageView source dynamically from a string? (Xamarin Android) 
             * https://stackoverflow.com/questions/39938391/how-to-change-the-imageview-source-dynamically-from-a-string-xamarin-android  */

            int image_id = activity.Resources.GetIdentifier(resource, "drawable", activity.PackageName);
            image.SetImageResource(image_id);

            /* added in v0.4 */
            /* set the ImageView to invisible intially */
            //image.Visibility =  (image.Visibility == ViewStates.Visible) ?   ViewStates.Invisible : ViewStates.Visible;

            /* The following code will also work. */
            //image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
        }

        /* v0.6 SetupButtonFont */
        public static void SetupButtonFont(Button btn)
        {
            btn.Typeface = Title_Font;
        }
    }
}
