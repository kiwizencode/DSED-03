using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;
using HangmanApp.Droid.Activities;
using ReactiveUI;
using Android.Graphics;

namespace HangmanApp.Droid.Helper
{
    /* added in v0.4 */
    public class FontsHelper
    {
        //private readonly string _Game_Font = "fonts/FFF_Tusj.ttf";
        // private readonly string _Digital_Font = "fonts/Digital-Dismay.otf";

        protected static FontsHelper _self;

        public static string Title_Font { get; set; } = "fonts/FFF_Tusj.ttf";

        public static string Digital_Font { get; set; } = "fonts/Digital-Dismay.otf";

        static FontsHelper()
        {
            _self = new FontsHelper();
        }

        protected FontsHelper() { }

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


        public static void SetupButton(ReactiveActivity activity, Button btn, int btn_id)
        {
            btn.Typeface = Typeface.CreateFromAsset(activity.Assets, FontsHelper.Title_Font);
        }
    }
}
