using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using ReactiveUI;

using Guess5.Lib.DataAccessObject;
using Guess5.Lib.Model;
using Guess5.Droid.ViewModel;

namespace Guess5.Droid.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class Activity_MainScreen : ReactiveActivity, IViewFor<ViewModel_Profile>
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

        /* ################################################################# */
        /* Use the instruction in the following URI to data-bind controls    */
        /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
        /*                                                                   */
        public Button btnStartGame { get; private set; }
        public Button btnScores { get; private set; }
        public Button btnProfile { get; private set; }
        public Button btnCredits { get; private set; }

        public TextView textViewTitle { get; private set; }
        public TextView textViewProfile { get; private set; }

        private string _profileID = string.Empty;
        public string ProfileID {
            get => _profileID;
            set => this.RaiseAndSetIfChanged(ref _profileID, value);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_MainScreen);

            InitializeModel();

            InitializeButtonClickEvent();

        }

        private void InitializeModel()
        {
            ViewModel = new ViewModel_Profile();
            ViewModel.CurrentActivity = this;

            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // 

            this.Bind(ViewModel, x => x.ProfileID, c => c.ProfileID);
            this.OneWayBind(ViewModel, x => x.ProfileName, c => c.textViewProfile.Text);
        }

        private void InitializeButtonClickEvent()
        {
            Intent activity;

            /* activate Start Game Activity when user click the button */
            btnStartGame.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Game));
                activity.PutExtra("Profile_ID", ProfileID);
                
                /* How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
            };

            /* activate Profile Activity when user click the button */
            btnProfile.Click += delegate
            {
                activity = new Intent(this, typeof(Activity_Profile));

                /* How to pass data back from activity without new intent */
                /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */
                StartActivityForResult(activity, 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            /* How to pass data back from activity without new intent */
            /* https://stackoverflow.com/questions/44691611/xamarin-android-c-how-to-pass-data-back-from-activity-without-new-intent */

            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                ProfileID = data.GetStringExtra("Profile_ID");
            }
        }
    }
}