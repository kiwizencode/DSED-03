using ReactiveUI;

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
using HangmanApp.Droid.Helper;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Activity_Profile")]
    public class Activity_Profile : ReactiveActivity
    {
        public TextView textViewTitle { get; private set; }
        public Button btnSave { get; private set; }
        public Button btnCancel { get; private set; }
        public Button btnCreate { get; private set; }
        public LinearLayout CreateProfileLayout { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Profile_v2);

            Initializer();
        }

        /* add in v0.4 */
        private void Initializer()
        {
            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // v0.4 => added this code

            /* Set the font for the activity title bar*/
            textViewTitle.Typeface =  FontsHelper.Title_Font;

            FontsHelper.SetupButton(this, btnCreate, Resource.Id.btnCreate);
            FontsHelper.SetupButton(this, btnSave, Resource.Id.btnSave);
            FontsHelper.SetupButton(this, btnCancel, Resource.Id.btnCancel);

            /* */
            btnCreate.Click += delegate
            {
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Visible;
            };

            btnSave.Click += delegate
            {
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Gone;
            };

            btnCancel.Click += delegate
            {
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Gone;
            };
        }
    }
}