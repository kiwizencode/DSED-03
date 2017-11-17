using System;
using System.Linq;
using System.Reactive.Linq;

using Android.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using static Android.Widget.AdapterView;

using ReactiveUI;

using Guess5.Lib.Model;
using Guess5.Lib.DataAccessObject;

using Guess5.Droid.ViewModel;

namespace Guess5.Droid.Activities
{
    [Activity(Label = "Activity_Profile")]
    public class Activity_Profile : ReactiveActivity, IViewFor<ViewModel_Profile>
    {
        #region Implementation of IViewFor<> Interface
        private ViewModel_Profile _model;
        public ViewModel_Profile ViewModel
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ViewModel_Profile)value;
        }
        #endregion

        #region GUI Control Declaration
        /* ################################################################# */
        /* Use the instruction in the following URI to data-bind controls    */
        /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
        /*                                                                   */

        public TextView textViewTitle { get; private set; }
        public TextView textViewProfile { get; private set; }
        public Button btnSave { get; private set; }
        public Button btnCancel { get; private set; }
        public Button btnCreate { get; private set; }
        public LinearLayout CreateProfileLayout { get; private set; }
        public ListView ListProfile { get; private set; } 
        public EditText editTextProfile { get; private set; }

        /* End of data-binding control */
        #endregion

        private string _profileID = string.Empty;
        public string ProfileID
        {
            get => _profileID;
            set => this.RaiseAndSetIfChanged(ref _profileID, value);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Profile_v2);

            InitializeModel();

            InitializeButtonClickEvent();

        }


        private void InitializeModel()
        {
            ViewModel = new ViewModel_Profile();
            ViewModel.CurrentActivity = this; 

            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls();

            this.OneWayBind(ViewModel, x => x.Profile_Array, c => c.ListProfile.Adapter);
            this.Bind(ViewModel, x => x.ProfileName, c => c.editTextProfile.Text);
            //this.Bind(ViewModel, x => x.ProfileID, c => c.ProfileID);
            this.BindCommand(ViewModel, x => x.CommandSave, c => c.btnSave, "Click");
            this.BindCommand(ViewModel, x => x.CommandItemClicked, c => c.ListProfile, "ItemClick");
        }

        private void InitializeButtonClickEvent()
        {
            /* */
            btnCreate.Click += delegate
            {
                editTextProfile.Text = string.Empty;
                setCreateProfile(ViewStates.Visible);
            };

            btnSave.Click += delegate
            {
                setCreateProfile(ViewStates.Gone);
            };

            btnCancel.Click += delegate
            {
                setCreateProfile(ViewStates.Gone);
            };

            //PopulateViewList();
            ListProfile.ItemClick +=
                (object sender, ItemClickEventArgs e) =>
                {
                    ListView obj = sender as ListView;
                    Object item = obj.GetItemAtPosition(e.Position);

                    /* https://stackoverflow.com/questions/6594250/type-cast-from-java-lang-object-to-native-clr-type-in-monodroid  */

                    ProfileModel profile = ((ArrayAdapter<ProfileModel>)obj.Adapter).GetItem(e.Position);

                    ProfileID = profile.ID.ToString();

                    // Debug
                    //Toast.MakeText(this, "ID" + profile.ID.ToString(), ToastLength.Short).Show();

                    /* How to pass data back from activity without new intent */
                    /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */

                    Intent myIntent = new Intent(this, typeof(Activity_MainScreen));
                    myIntent.PutExtra("Profile_ID", ProfileID);
                    SetResult(Result.Ok, myIntent);
                    Finish();
                };

            /* Android EditText Set Max Length */
            /* https://stackoverflow.com/questions/25424646/android-edittext-set-max-length  */
            editTextProfile.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(10) });
        }

        private void setCreateProfile(ViewStates layoutState)
        {
            var otherControlState = (layoutState == ViewStates.Visible) ? ViewStates.Invisible : ViewStates.Visible;
            CreateProfileLayout.Visibility = layoutState;
            btnCreate.Visibility = otherControlState;
            ListProfile.Visibility = otherControlState;
            textViewProfile.Visibility = otherControlState;
        }

    }
}