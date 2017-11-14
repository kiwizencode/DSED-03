using ReactiveUI;

using System;
using System.Linq;
using System.Reactive.Linq;
using Android.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

//using HangmanApp.Droid.Helper;
using Guess5.Lib.Model;
using Guess5.Lib.DataAccessObject;
using static Android.Widget.AdapterView;


namespace Guess5.Droid.Activities
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

        private void InitializeUIControl()
        {
            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); 

            void setCreateProfile(ViewStates layoutState)
            {
                var otherControlState = (layoutState == ViewStates.Visible) ? ViewStates.Invisible : ViewStates.Visible;
                CreateProfileLayout.Visibility = layoutState;
                btnCreate.Visibility = otherControlState;
                ListProfile.Visibility = otherControlState;
                textViewProfile.Visibility = otherControlState;
            }


            /* */
            btnCreate.Click += delegate
            {
                //CreateProfileLayout.Visibility = ViewStates.Visible;
                //btnCreate.Visibility = ViewStates.Invisible; 
                //ListProfile.Visibility = ViewStates.Invisible; 
                //textViewProfile.Visibility = ViewStates.Invisible;
                setCreateProfile(ViewStates.Visible);
            };

            btnSave.Click += delegate
            {
                string text = editTextProfile.Text;
                if (text != string.Empty)
                {
                    /* Create a Game Profile */
                    ProfileModel profile = new ProfileModel();
                    profile.Name = text;

                    DateTime theTime = DateTime.Now.ToLocalTime();
                    profile.Timestamp = theTime;

                    profile.ID = ProfileRepository.SaveProfile(profile);

                    PopulateViewList();
                }
                //CreateProfileLayout.Visibility = ViewStates.Gone;
                //btnCreate.Visibility = ViewStates.Visible; 
                //ListProfile.Visibility = ViewStates.Visible; 
                //textViewProfile.Visibility = ViewStates.Visible; 
                setCreateProfile(ViewStates.Gone);
            };

            btnCancel.Click += delegate
            {
                //CreateProfileLayout.Visibility = ViewStates.Gone;
                //btnCreate.Visibility = ViewStates.Visible; 
                //ListProfile.Visibility = ViewStates.Visible; 
                //textViewProfile.Visibility = ViewStates.Visible;
                setCreateProfile(ViewStates.Gone);
            };

            PopulateViewList();
            ListProfile.ItemClick +=
                (object sender, ItemClickEventArgs e) =>
                {
                    ListView obj = sender as ListView;
                    Object item = obj.GetItemAtPosition(e.Position);

                    /* https://stackoverflow.com/questions/6594250/type-cast-from-java-lang-object-to-native-clr-type-in-monodroid  */

                    ProfileModel profile = ((ArrayAdapter<ProfileModel>)obj.Adapter).GetItem(e.Position);

                    // Debug
                    //Toast.MakeText(this, "ID" + profile.ID.ToString(), ToastLength.Short).Show();

                    /* How to pass data back from activity without new intent */
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
            ProfileModel[] items = ProfileRepository.GetProfiles().OrderBy( x => x.Name.ToLower()).ToArray();
            //
            ListProfile.Adapter = new ArrayAdapter<ProfileModel>(this, Android.Resource.Layout.SimpleListItem1, items);
        }
    }
}