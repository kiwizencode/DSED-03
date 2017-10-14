using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Threading;

using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using ReactiveUI;


using Guess5App.Droid.ViewModel;

using Guess5App.Droid.Helper;

using Guess5App.Shared.DataAccessObject;
using Guess5App.Shared.Model;


namespace Guess5App.Droid.Activities
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


        /* v0.4 */
        /* load hangman image */
        private string Hangman_Image
        {
            get => string.Empty;
            set => FontsHelper.SetImageView(this, Resource.Id.imageViewHangman, value);
        }



        public TextView textViewTitle { get; private set; }
        public TextView textViewTimer { get; private set; }
        public TextView textViewHighest { get; private set; }
        public TextView textViewScore { get; private set; }


        /* v0.6 add User Profile */
        public TextView textViewProfile { get; private set; }
        public ImageView imageViewHangman { get; private set; }

        /* add Guess5s v0.1 */
        public Button btnStartNew { get; private set; }




        public bool Run_Flag { get; set; } 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Game);

            InitializeUIControl();

            InitializeModel();

            /* https://msdn.microsoft.com/en-us/library/hh211607(v=vs.103).aspx  */
            //var mainSequence = Observable.Interval(TimeSpan.FromSeconds(1));

            //var run_is_true = from i in mainSequence
            //                  where Run_Flag == true
            //                  select i;

            //run_is_true.Subscribe(x =>
            //{
            //    //ThreadPool.QueueUserWorkItem(_ =>
            //    //{
            //    RunOnUiThread(() => ViewModel.TimerTick());
            //    //});
            //});


            /* https://reactiveui.net/docs/handbook/commands/invoking-commands  */


            
            var interval = TimeSpan.FromSeconds(1);
            Observable.Timer(interval, interval)
                        .Where(i => (Run_Flag?1:0) == 1 )
                           .Subscribe(delegate { RunOnUiThread(() => ViewModel.TimerTick()); });
            

            /*
            btnStartNew.Click += delegate
            {
                //Run_Flag = !Run_Flag;
                //ViewModel.TickCommand().;
            };
            */

            this.BindCommand(this.ViewModel, v => v.TickCommand, c => c.btnStartNew,"Click");

        }


        private void InitializeUIControl()
        {

            /* v0.4 */
            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */

            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // v0.4 => added this code

            /* Set the font for the activity title bar*/
            textViewTitle.Typeface = FontsHelper.Title_Font;

            /* Set the font for the count-down timer */
            var digital_font = FontsHelper.Digital_Font;
            textViewTimer.Typeface = digital_font;
            textViewHighest.Typeface = digital_font;
            textViewScore.Typeface = digital_font;
        }

        private void InitializeModel()
        {
            ViewModel = new ViewModel_Game();

            this.OneWayBind(ViewModel, x => x.Run_Flag, c => c.Run_Flag);
            this.OneWayBind(ViewModel, x => x.Score, c => c.textViewScore.Text);
            this.OneWayBind(ViewModel, x => x.Timer, c => c.textViewTimer.Text);

            /* v0.4 display hangman image onto screen*/
            this.OneWayBind(ViewModel, x => x.Hangman_Image, c => c.Hangman_Image);
        }

    }
}