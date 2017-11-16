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
using Guess5.Droid.Helper;

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

        /* load hangman image */
        private string Hangman_Image {
            get => string.Empty;
            set => FontsHelper.SetImageView(this,Resource.Id.imageViewHangman, value);
        }

        /* ################################################################# */
        /* Declare all GUI control variables into property     */

        /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */

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

        public TextView textViewTitle { get; private set; }
        public TextView textViewTimer { get; private set; }
        public TextView textViewHighest { get; private set; }
        public TextView textViewScore { get; private set; }
        /* User Profile */
        public TextView textViewProfile { get; private set; }

        public ImageView imageViewHangman { get; private set; }

        /* ==================================================================================================== */
        /* Set the letter image for the 5 letter slot for the hidden word. 
           Because of the way I code the progarm , I could not directly bind the view model value to the ui control.
           The way I do is get the png image name from the view model, use the FontsHelper class to get the image,
           and the set it to the GUI Image controls.
         */

        private string Slot01_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.ImageSlot01, value);
        }

        private string Slot02_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.ImageSlot02, value);
        }

        private string Slot03_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.ImageSlot03, value);
        }

        private string Slot04_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.ImageSlot04, value);
        }

        private string Slot05_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.ImageSlot05, value);
        }
        /* ==================================================================================================== */

        /* For DEbugging */
        //public TextView textViewToast { get; private set; }

        /* ################################################################# */

        /* */
        //private bool run_flag = true;
        private bool _is_running_flag;
        public bool Is_Game_Still_Running
        {
            get => _is_running_flag;
            set => this.RaiseAndSetIfChanged(ref _is_running_flag, value);
        }

        private bool _restart_timer_flag;
        public bool Restart_Timer
        {
            get => _restart_timer_flag;
            set => this.RaiseAndSetIfChanged(ref _restart_timer_flag, value);
        }

        private bool _stop_flag;
        public bool Stop_Timer
        {
            get => _stop_flag;
            set => this.RaiseAndSetIfChanged(ref _stop_flag, value);
        }

        private int SelectButton { get; set; }
        public string Button_Text { get; set; }
        public string Button_Tag { get; set; }
        /* add check winning flag */
        public bool Is_Game_Won { get; set; }

        public ProfileModel Current_Profile { get; set; } = null;

        private string profile_id = string.Empty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Game);

            InitializeUIControl();

            InitializeModel();

            InitializeStream();

            /* https://developer.xamarin.com/recipes/android/fundamentals/activity/pass_data_between_activity/ */

            profile_id = Intent.GetStringExtra("Profile_ID") ?? string.Empty;

            if (profile_id != string.Empty)
            {
                int id = int.Parse(profile_id);
                Current_Profile = ProfileRepository.GetProfile(id);
                textViewProfile.Text = Current_Profile.Name;
            }
        }


        /// <summary>
        /// Initialize the View Model and bind the View Model to the (GUI) View
        /// </summary>
        private void InitializeModel()
        {
            ViewModel = new ViewModel_Game();

            ViewModel.CurrentActivity = this;

            /* bind "is game still running" flag */
            this.OneWayBind(ViewModel, x => x.Is_Game_Still_Running, c => c.Is_Game_Still_Running);

            /* bind "is game still running" flag */
            this.OneWayBind(ViewModel, x => x.Restart_Timer, c => c.Restart_Timer);

            /* bind "is game won" flag */
            this.OneWayBind(ViewModel, x => x.Is_Game_Won, c => c.Is_Game_Won);

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


        private void InitializeStream()
        {
            

            this.WhenAnyValue(x => x.Is_Game_Still_Running).Select(x => Is_Game_Still_Running==true).Subscribe( (x) => {
                // Debug
                System.Diagnostics.Debug.WriteLine($"Is Game Running ? {x}");
                //btnStartNew.Visibility = x ? ViewStates.Invisible : ViewStates.Visible;
                //IDisposable timer;
                //if (x)
                //{
                //    var interval = Observable.Interval(TimeSpan.FromMilliseconds(1000));
                //    timer = interval.Subscribe(
                //        i => { System.Diagnostics.Debug.WriteLine(i); });

                //    this.WhenAnyValue(z => z.Is_Game_Still_Running).Select(z => Is_Game_Still_Running == true).Subscribe(
                //        (z) => { if (!z) timer.Dispose();  });

                //}                

            });
            /*
            this.WhenAnyValue(x => x.Restart_Timer).Subscribe((x) => {
                // Debug
                System.Diagnostics.Debug.WriteLine($"Restart Timer ? {x}");
                //btnStartNew.Visibility = x ? ViewStates.Invisible : ViewStates.Visible;
                if (x)
                {
                    Stop_Timer = true;
                    this.RaisePropertyChanged("Stop_Timer");

                    //var interval = Observable.Interval(TimeSpan.FromSeconds(1));
                    //IDisposable timer = interval.Subscribe(i =>
                    //   { //System.Diagnostics.Debug.WriteLine(i);
                    //       var counter = (int)i % 10 + 1;
                    //       _timer = (int)i % MAX_COUNT;
                    //       System.Diagnostics.Debug.WriteLine($"timer counter : {counter}");
                    //       ViewModel.TimerTick2(counter);
                    //       RunOnUiThread(() => ViewModel.TimerTick2(counter));
                    //       this.RaisePropertyChanged("Timer");
                    //       Debug.WriteLine($"Timer counter : {_timer}");
                    //   });
                    int MAX_COUNT = 10;
                    Observable.Interval(TimeSpan.FromSeconds(1))
                            .Take(MAX_COUNT)
                            .Repeat()
                            .Subscribe( value => 
                        {
                            var counter = MAX_COUNT - (int)value;
                            RunOnUiThread(() => ViewModel.TimerTick2(counter));
                        });

                    

                    //this.WhenAnyValue(z => z.Stop_Timer)
                    //.Subscribe((z) =>
                    //{
                    //    timer.Dispose();
                    //});
                }

            });
    */
            //RunOnUiThread(() => ViewModel.TimerTick2(counter));
            /* v0.6 start a new game */

            //btnStartNew.Click += delegate
            //{
            //    if (!Is_Game_Still_Running)
            //    {
            //        //ViewModel.Reset();
            //        //RunApp();
            //        btnStartNew.Visibility = ViewStates.Invisible;

            //    }
            //};

            //this.WhenAnyValue(x => x.textViewScore.Text).Subscribe(_ => {

            //    if (int.TryParse(textViewScore.Text, out int highestscore))
            //    {
            //        if (Current_Profile != null)
            //        {
            //            int scores = Current_Profile.Scores;
            //            if (scores < highestscore)
            //            {
            //                Current_Profile.Scores = highestscore;
            //                ProfileRepository.SaveProfile(Current_Profile);
            //            }

            //        }
            //    }

            //});
        }












        private void CommentOut()
        {

            /* ####################################################################################### */
            /* The following code may not be the correct way of doing things. 
             * But just get it working and resolve it when I have a better understanding of ReactiveUI */

            bool flag = true;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while(flag)
                {
                    while (Is_Game_Still_Running)
                    {
                        Thread.Sleep(1000);
                        RunOnUiThread(() => ViewModel.TimerTick());
                    }

                    /*v0.6 setup the trigger for winning and losing the game */

                    if (Is_Game_Won)
                    {
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "You have won !!!\nLet's guess another hidden word.", ToastLength.Short).Show();
                            //Toast.MakeText(this, "Let's guess another hidden word.", ToastLength.Long).Show();
                            ViewModel.Reset();
                        });
                        Is_Game_Still_Running = true;
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "You did not guess the word!!!", ToastLength.Short).Show();
                            Toast.MakeText(this, "The hiden word is " + ViewModel.hidden_word, ToastLength.Long).Show();
                        });
                        flag = false;
                    }
                }



            });

        }

        /// <summary>
        /// 
        /// </summary>
        private void RunApp()
        {

            /* ####################################################################################### */
            /* The following code may not be the correct way of doing things. 
             * But just get it working and resolve it when I have a better understanding of ReactiveUI */

            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (Is_Game_Still_Running)
                {
                    Thread.Sleep(1000);
                    RunOnUiThread(() => ViewModel.TimerTick());
                }

                if (Is_Game_Won)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "You have won !!!", ToastLength.Short).Show();
                        Toast.MakeText(this, "Let's guess another hidden word.", ToastLength.Long).Show();
                    });

                    ViewModel.Reset();
                    RunApp();
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "You did not guess the word!!!", ToastLength.Short).Show();
                        Toast.MakeText(this, "The hiden word is " + ViewModel.hidden_word, ToastLength.Long).Show();
                    });
                }

            });
        }

        /* v0.6 refactor code : method to intitalise model and reactive component */


    }
}