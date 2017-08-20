using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace XamarinGame.Droid
{
    [Activity(Label = "ScreenActivity")] // , MainLauncher = true)]
    public class ScreenActivity : Activity
    {
        private string activity_title=string.Empty;

        private TextView textViewTitle;
        private TextView textViewMessage;

        private Button button1;
        private Button button2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.screen_layout);

            /*
             * https://developer.xamarin.com/recipes/android/fundamentals/activity/pass_data_between_activity/
             */
            //if (activity_title == string.Empty)
            //    activity_title = "Activity 1";
            activity_title = Intent.GetStringExtra("Activity_Data") ?? "Activity 1";

            textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);

            /*
             * Where to get font
             * https://www.fontsquirrel.com/fonts/list/popular
             */


            /*
             * https://developer.xamarin.com/guides/android/application_fundamentals/resources_in_android/part_6_-_using_android_assets/
             */

            /*
 * Include external font ttf in android project in Xamarin C#
 * 
 * https://stackoverflow.com/questions/27247289/include-external-font-ttf-in-android-project-in-xamarin-c-sharp
 */
            var typeface = Typeface.CreateFromAsset(Assets, "FFF_Tusj.ttf");

            /*
             * https://forums.xamarin.com/discussion/8674/change-font-of-a-textview-at-layout-main-axml
             */
            textViewTitle.SetTypeface(typeface, TypefaceStyle.Normal);
            textViewTitle.Text = activity_title;


            /*
             * Display message on the textViewMessage on the bottom of the screen
             */
            textViewMessage = FindViewById<TextView>(Resource.Id.textViewMessage);
            string num = activity_title.Substring(activity_title.Length - 1);
            textViewMessage.Text = "Activity " + num + " has started";


            /*
             * Android moving back to first activity on button click
             * https://forums.xamarin.com/discussion/94898/android-moving-back-to-first-activity-on-button-click
             */
            button1 = FindViewById<Button>(Resource.Id.button1);
            button2 = FindViewById<Button>(Resource.Id.button2);

            List<string> choice = new List<string>(new string[] { "1", "2", "3" });

            foreach (var ch in choice)
            {
                if (ch == num)
                {
                    choice.Remove(ch);
                    break;
                }

            }

            //button1.Text = "Start Activity " + choice[0];
            //button2.Text = "Start Activity " + choice[1];

            StartNewActivity(button1,  choice[0]);
            StartNewActivity(button2,  choice[1]);
        }

        private void StartNewActivity(Button btn, string text)
        {
            btn.Text = "Start Activity " + text;

            /*
             * https://developer.xamarin.com/recipes/android/fundamentals/activity/pass_data_between_activity/
             */

            btn.Click += delegate
            {
                var newactivity = new Intent(this, typeof(ScreenActivity));

                newactivity.PutExtra("Activity_Data", "Activity " + text);
                StartActivity(newactivity);

                this.Finish();
            };
        }
    }
}