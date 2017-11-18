using System;
using System.Reactive.Linq;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using ReactiveUI;

using Guess5.Lib.DataAccessObject;
using Guess5.Lib.Model;

using Guess5.Droid.ViewModel;

namespace Guess5.Droid.Activities
{
    [Activity(Label = "Game Activity")] //, MainLauncher = true)]
    public class Activity_Game : ReactiveActivity, IViewFor<ViewModel_Game>
    {

        #region Implementation of IViewFor<> Interface
        private ViewModel_Game _model;
        public ViewModel_Game ViewModel
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ViewModel_Game)value;
        }
        #endregion

        #region Declaration for Image Slot Display Implementation
        
        /// <summary>
        /// set the image of ImageView based on the input resource
        /// 
        /// </summary>
        /// <param name="id">ImageView resource id</param>
        /// <param name="resource">Image file name</param>
        //private void SetImageView(int id, string resource)
        private void SetImageView(ReactiveActivity activity, int id, string resource)
        {
            ImageView image = activity.FindViewById<ImageView>(id);

            /* How to change the ImageView source dynamically from a string? (Xamarin Android) 
             * https://stackoverflow.com/questions/39938391/how-to-change-the-imageview-source-dynamically-from-a-string-xamarin-android  */

            int image_id = activity.Resources.GetIdentifier(resource, "drawable", activity.PackageName);
            image.SetImageResource(image_id);

            /* The following code will also work. */
            //image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
        }

        /* load hangman image */
        private string Hangman_Image {
            get => string.Empty;
            set => SetImageView(this,Resource.Id.imageViewHangman, value);
        }

        /* ==================================================================================================== */
        /* Set the letter image for the 5 letter slot for the hidden word. 
           Because of the way I code the progarm , I could not directly bind the view model value to the ui control.
           The way I do is get the png image name from the view model, use the FontsHelper class to get the image,
           and the set it to the GUI Image controls.
         */

        private string Slot01_Image
        {
            get => string.Empty;
            set => SetImageView(this, Resource.Id.ImageSlot01, value);
        }

        private string Slot02_Image
        {
            get => string.Empty;
            set => SetImageView(this, Resource.Id.ImageSlot02, value);
        }

        private string Slot03_Image
        {
            get => string.Empty;
            set => SetImageView(this, Resource.Id.ImageSlot03, value);
        }

        private string Slot04_Image
        {
            get => string.Empty;
            set => SetImageView(this, Resource.Id.ImageSlot04, value);
        }

        private string Slot05_Image
        {
            get => string.Empty;
            set => SetImageView(this, Resource.Id.ImageSlot05, value);
        }
        #endregion

        #region GUI Control Declaration
        /* ################################################################# */
        /* Use the instruction in the following URI to data-bind controls    */
        /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */
        /*                                                                   */
        public Button btn01 { get; private set; }
        public Button btn02 { get; private set; }
        public Button btn03 { get; private set; }
        public Button btn04 { get; private set; }
        public Button btn05 { get; private set; }
        public Button btn06 { get; private set; }
        public Button btn07 { get; private set; }
        public Button btn08 { get; private set; }
        public Button btn09 { get; private set; }
        public Button btn10 { get; private set; }
        public Button btn11 { get; private set; }
        public Button btn12 { get; private set; }
        public Button btn13 { get; private set; }
        public Button btn14 { get; private set; }
        public Button btn15 { get; private set; }
        public Button btnStartNew { get; private set; }
        public Button btnPause { get; private set; }

        public TextView textViewTitle { get; private set; }
        public TextView textViewTimer { get; private set; }
        public TextView textViewHighest { get; private set; }
        public TextView textViewScore { get; private set; }
        /* User Profile */
        public TextView textViewProfile { get; private set; }

        public ImageView imageViewHangman { get; private set; }
#endregion

        /* ==================================================================================================== */

        /* For DEbugging */
        //public TextView textViewToast { get; private set; }

        /* ################################################################# */

        private int SelectButton { get; set; }
        public string Button_Text { get; set; }
        public string Button_Tag { get; set; } 

        public ProfileModel Current_Profile { get; set; } = null;

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
            SetContentView(Resource.Layout.Layout_Game);

            InitializeUIControl();

            InitializeModel();

            SetupObservable();

            /* https://developer.xamarin.com/recipes/android/fundamentals/activity/pass_data_between_activity/ */

            ProfileID = Intent.GetStringExtra("Profile_ID") ?? string.Empty;

            //if (profile_id != string.Empty)
            //{
            //    int id = int.Parse(profile_id);
            //    Current_Profile = ProfileRepository.GetProfile(id);
            //    textViewProfile.Text = Current_Profile.Name;
            //}
        }


        /// <summary>
        /// Initialize the View Model and bind the View Model to the (GUI) View
        /// </summary>
        private void InitializeModel()
        {
            ViewModel = new ViewModel_Game();
            ViewModel.CurrentActivity = this;

            /* flag indicate whether the timer is ticking */
            this.OneWayBind(ViewModel, x => x.Timer_Flag, c => c.Timer_Flag);

            /* Show the timer counter */
            this.OneWayBind(ViewModel, x => x.Timer, c => c.textViewTimer.Text);

            /* Show the user current score */
            this.OneWayBind(ViewModel, x => x.Score, c => c.textViewScore.Text);

            /* Show user highest score in the current game */
            this.OneWayBind(ViewModel, x => x.HighestScore, c => c.textViewHighest.Text);

            /* Display different hangman image when the user makes a wrong guess */
            this.OneWayBind(ViewModel, x => x.Hangman_Image, c => c.Hangman_Image);

            /* bind the slot image from View to View Model
             * Initialy show a question mark image.
             * When the user guess the correct letter of the hidden word, 
             *   that letter will appear on the respective slot image.
             */
            this.OneWayBind(ViewModel, x => x.Slot01_Image, c => c.Slot01_Image);
            this.OneWayBind(ViewModel, x => x.Slot02_Image, c => c.Slot02_Image);
            this.OneWayBind(ViewModel, x => x.Slot03_Image, c => c.Slot03_Image);
            this.OneWayBind(ViewModel, x => x.Slot04_Image, c => c.Slot04_Image);
            this.OneWayBind(ViewModel, x => x.Slot05_Image, c => c.Slot05_Image);

            /* */
            this.Bind(ViewModel, x => x.Btn_Text, c => c.Button_Text);
            this.Bind(ViewModel, x => x.Btn_Tag, c => c.Button_Tag);
            //this.OneWayBind(ViewModel, x => x.Toast, c => c.textViewToast.Text);

            /* bind the buttons on the View to the view model */
            this.OneWayBind(ViewModel, x => x.Btn01, c => c.btn01.Text);
            this.OneWayBind(ViewModel, x => x.Btn02, c => c.btn02.Text);
            this.OneWayBind(ViewModel, x => x.Btn03, c => c.btn03.Text);
            this.OneWayBind(ViewModel, x => x.Btn04, c => c.btn04.Text);
            this.OneWayBind(ViewModel, x => x.Btn05, c => c.btn05.Text);
            this.OneWayBind(ViewModel, x => x.Btn06, c => c.btn06.Text);
            this.OneWayBind(ViewModel, x => x.Btn07, c => c.btn07.Text);
            this.OneWayBind(ViewModel, x => x.Btn08, c => c.btn08.Text);
            this.OneWayBind(ViewModel, x => x.Btn09, c => c.btn09.Text);
            this.OneWayBind(ViewModel, x => x.Btn10, c => c.btn10.Text);
            this.OneWayBind(ViewModel, x => x.Btn11, c => c.btn11.Text);
            this.OneWayBind(ViewModel, x => x.Btn12, c => c.btn12.Text);
            this.OneWayBind(ViewModel, x => x.Btn13, c => c.btn13.Text);
            this.OneWayBind(ViewModel, x => x.Btn14, c => c.btn14.Text);
            this.OneWayBind(ViewModel, x => x.Btn15, c => c.btn15.Text);

            this.BindCommand(ViewModel, x => x.commandStart, c => c.btnStartNew, "Click");
            //this.BindCommand(ViewModel, x => x.commandPause, c => c.btnPause, "Click");

            this.Bind(ViewModel, x => x.ProfileID, c => c.ProfileID);
            this.OneWayBind(ViewModel, x => x.ProfileName, c => c.textViewProfile.Text);
        }

        /* method to intitalise UI Controls */
        /// <summary>
        /// 
        /// </summary>
        private void InitializeUIControl()
        {

            /* ========================================================================================= */
            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */

            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); 
            /* ========================================================================================= */

            /* ========================================================================================== */
            /* Setup button click event for all 15 buttons                                                */

            /* The inner function to set the Click Event for a button */
            void SetButtonClickEvent(Button new_btn)
            {
                new_btn.Click += (sender, e) => {
                    Button btn = sender as Button;
                    /* The text store the letter on the button */
                    Button_Text = btn.Text; this.RaisePropertyChanged("Button_Text");
                    /* Inside each button Tag is the correspondence button number i.e 
                       Button 01 is 01, Button 02 is 02 ......
                       This is my way to tell View model which button has been clicked */
                    Button_Tag = btn.Tag.ToString(); this.RaisePropertyChanged("Button_Tag");
                    /* Since the button button has been clicked set it to invisible 
                         so that user could not click the second time. */
                    btn.Visibility = ViewStates.Invisible;
                };
            }

            SetButtonClickEvent(btn01);
            SetButtonClickEvent(btn02);
            SetButtonClickEvent(btn03);
            SetButtonClickEvent(btn04);
            SetButtonClickEvent(btn05);
            SetButtonClickEvent(btn06);
            SetButtonClickEvent(btn07);
            SetButtonClickEvent(btn08);
            SetButtonClickEvent(btn09);
            SetButtonClickEvent(btn10);
            SetButtonClickEvent(btn11);
            SetButtonClickEvent(btn12);
            SetButtonClickEvent(btn13);
            SetButtonClickEvent(btn14);
            SetButtonClickEvent(btn15);

            /* End of Setup Button Event */
            /* ========================================================================================== */

            /* The following code is triggered whenever the button text has been updated. */

            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn01.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn02.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn03.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn04.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn05.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn06.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn07.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn08.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn09.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn10.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn11.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn12.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn13.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn14.Visibility = ViewStates.Visible);
            this.WhenAnyValue(x => x.btn01.Text).Subscribe(_ => btn15.Visibility = ViewStates.Visible);
            /* ========================================================================================== */
        }

        private bool _timer_flag = false;
        public bool Timer_Flag
        {
            get => _timer_flag;
            set => this.RaiseAndSetIfChanged(ref _timer_flag, value);
        }

        private void SetupObservable()
        {
            this.WhenAnyValue(x => x.Timer_Flag)
                .Select(flag => (Timer_Flag == true) )
                .Subscribe( (flag) => {
                    // Debug
                    System.Diagnostics.Debug.WriteLine($"Is Timer ticking ? {flag}");

                    //btnPause.Text = flag ? "Pause" : "Restart";

                    /* when the timer is ticking/running , make button invisiable so that user could not click again
                        until end of the game, which is the timer will be stop */
                    btnStartNew.Visibility = flag ? ViewStates.Invisible : ViewStates.Visible;
                    //btnPause.Visibility = flag ? ViewStates.Visible : ViewStates.Invisible;

                    //btnStartNew.Enabled = !flag;
                    btn01.Enabled = flag;
                    btn02.Enabled = flag;
                    btn03.Enabled = flag;
                    btn04.Enabled = flag;
                    btn05.Enabled = flag;
                    btn06.Enabled = flag;
                    btn07.Enabled = flag;
                    btn08.Enabled = flag;
                    btn09.Enabled = flag;
                    btn10.Enabled = flag;
                    btn11.Enabled = flag;
                    btn12.Enabled = flag;
                    btn13.Enabled = flag;
                    btn14.Enabled = flag;
                    btn15.Enabled = flag;
                });
        }

#region following code deal with the situation where Game Acitivity goes into inactive mode and finishing mode
        protected override void OnResume()
        {
            base.OnResume();
            this.ViewModel.Resume();
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.ViewModel.Stop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ViewModel.Dispose();
        }
#endregion
    }
}