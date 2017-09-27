using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using HangmanApp.Droid.Helper;
using HangmanApp.Shared.Model;
using HangmanApp.Shared.DataAccessObject;
using static Android.Widget.AdapterView;
using Android.Text;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Activity_Profile")]
    public class Activity_Profile : ReactiveActivity
    {

        /* ################################################################# */
        /* Use the instruction in the following URI to data-bind controls    */
        /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
        /*                                                                   */

        public TextView textViewTitle { get; private set; }
        public TextView textViewProfile { get; private set; } /* v0.6 */
        public Button btnSave { get; private set; }
        public Button btnCancel { get; private set; }
        public Button btnCreate { get; private set; }
        public LinearLayout CreateProfileLayout { get; private set; }
        public ListView ListProfile { get; private set; } /* v0.6 */
        public EditText editTextProfile { get; private set; } /* v0.6 */

        /* End of data-binding control */

        //private ArrayAdapter<Model_Profile> adaptor = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Profile_v2);

            InitializeUIControl();

        }

        /* add in v0.4 */
        private void InitializeUIControl()
        {
            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // v0.4 => added this code

            /* Set the font for the activity title bar*/
            textViewTitle.Typeface = FontsHelper.Title_Font;

            /* v0.6  change to new way to initialize the font on the button  */
            //FontsHelper.SetupButton(this, btnCreate, Resource.Id.btnCreate);
            //FontsHelper.SetupButton(this, btnSave, Resource.Id.btnSave);
            //FontsHelper.SetupButton(this, btnCancel, Resource.Id.btnCancel);

            FontsHelper.SetupButtonFont(btnCreate);
            FontsHelper.SetupButtonFont(btnSave);
            FontsHelper.SetupButtonFont(btnCancel);

            



            /* */
            btnCreate.Click += delegate
            {
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Visible;
                btnCreate.Visibility = ViewStates.Invisible; /* v0.6 */
                ListProfile.Visibility = ViewStates.Invisible; /* v0.6 */
                textViewProfile.Visibility = ViewStates.Invisible; /* v0.6 */
            };

            btnSave.Click += delegate
            {
                string text = editTextProfile.Text;
                if (text != string.Empty)
                {
                    /* Create a Game Profile */

                    Model_Profile profile = new Model_Profile();
                    profile.Name = text;

                    DateTime theTime = DateTime.Now.ToLocalTime();
                    profile.Timestamp = theTime;

                    profile.ID = ProfileRepository.SaveProfile(profile);

                    PopulateViewList();
                    //ListProfile.Adapter.Add
                    //adaptor.Add(profile);
                    // RunOnUiThread(() => { adaptor.NotifyDataSetChanged(); });


                }
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Gone;
                btnCreate.Visibility = ViewStates.Visible; /* v0.6 */
                ListProfile.Visibility = ViewStates.Visible; /* v0.6 */
                textViewProfile.Visibility = ViewStates.Visible; /* v0.6 */
            };

            btnCancel.Click += delegate
            {
                //activity = new Intent(this, typeof(Activity_Game));
                CreateProfileLayout.Visibility = ViewStates.Gone;
                btnCreate.Visibility = ViewStates.Visible; /* v0.6 */
                ListProfile.Visibility = ViewStates.Visible; /* v0.6 */
                textViewProfile.Visibility = ViewStates.Visible; /* v0.6 */
            };


            //ListProfile.DividerHeight = "25dp";
            PopulateViewList();
            ListProfile.ItemClick +=
                (object sender, ItemClickEventArgs e) =>
                {
                    ListView obj = sender as ListView;
                    Object item = obj.GetItemAtPosition(e.Position);

                    /* https://stackoverflow.com/questions/6594250/type-cast-from-java-lang-object-to-native-clr-type-in-monodroid  */

                    Model_Profile profile = ((ArrayAdapter<Model_Profile>)obj.Adapter).GetItem(e.Position);

                    //Toast.MakeText(this, "ID" + profile.ID.ToString(), ToastLength.Short).Show();

                    /* v0.6 How to pass data back from activity without new intent */
                    /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */

                    Intent myIntent = new Intent(this, typeof(Activity_MainScreen));
                    myIntent.PutExtra("Profile_ID", profile.ID.ToString());
                    SetResult(Result.Ok, myIntent);
                    Finish();

                };


            /* Android EditText Set Max Length */
            /* https://stackoverflow.com/questions/25424646/android-edittext-set-max-length  */
            editTextProfile.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(10) });
        }

        private void PopulateViewList()
        {
            Model_Profile[] items = ProfileRepository.GetProfiles().OrderBy( x => x.Name.ToLower()).ToArray();
            //adaptor = new ArrayAdapter<Model_Profile>(this, Android.Resource.Layout.SimpleListItem1, items);
            ListProfile.Adapter = new ArrayAdapter<Model_Profile>(this, Android.Resource.Layout.SimpleListItem1, items);
        }
    }
}