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

using ReactiveUI;

using System.Threading;
using HangmanApp.Droid.ViewModel;
using Android.Graphics;
using System.Reactive.Linq;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Game Activity")]
    public class Activity_Game : ReactiveActivity, IViewFor<ViewModel_Game>
    {
        readonly string Game_Font = "fonts/FFF_Tusj.ttf";
        readonly string Digital_Font = "fonts/Digital-Dismay.otf";

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

        /// <summary>
        /// set the image of ImageView based on the input resource
        /// 
        /// </summary>
        /// <param name="id">ImageView resource id</param>
        /// <param name="resource">Image file name</param>
        private void SetImageView(int id, string resource)
        {
            ImageView image = FindViewById<ImageView>(id);

            /* How to change the ImageView source dynamically from a string? (Xamarin Android) 
             * https://stackoverflow.com/questions/39938391/how-to-change-the-imageview-source-dynamically-from-a-string-xamarin-android  */

            int image_id = Resources.GetIdentifier(resource, "drawable", PackageName);
            image.SetImageResource(image_id);

            /* The following code will also work. */
            //image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
        }

        /* ==================================================================================================== */
        /* Set the letter image for the 5 letter slot for the hidden word */

        /* v0.4 changes - rename the variable from Slotxx_Image to Slotxx_Image*/
        /* The get operator in the following 15 properties are not being used.
           But have to be added somehow so that the reactive bind function will work.*/
        private string Slot01_Image { get => string.Empty; set => SetImageView(Resource.Id.ImageSlot01, value); }
        private string Slot02_Image { get => string.Empty; set => SetImageView(Resource.Id.ImageSlot02, value); }
        private string Slot03_Image { get => string.Empty; set => SetImageView(Resource.Id.ImageSlot03, value); }
        private string Slot04_Image { get => string.Empty; set => SetImageView(Resource.Id.ImageSlot04,value); }
        private string Slot05_Image { get => string.Empty; set => SetImageView(Resource.Id.ImageSlot05,value); }

        /* ==================================================================================================== */


        /* */
        private bool run_flag = false;

        private int SelectButton { get; set; }
        public string Button_Text { get; set; }
        public string Button_Tag { get; set; }

        /* ############################## */

        /* v0.4 change all the following controls variable into property */
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


        public TextView textViewTitle { get; private set; }
        public TextView textViewTimer { get; private set; }

        /* For DEbugging */
        public TextView textViewToast { get; private set; }

        /* ############################## */



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_Game);

            Initializer();
        }

        private void Initializer()
        {
            ViewModel = new ViewModel_Game();

            
            /* https://reactiveui.net/docs/handbook/data-binding/xamarin-android */

            // WireUpControls looks through your layout file, finds all controls 
            // with an id defined, and binds them to the controls defined in this class
            // This is basically the same functionality as http://jakewharton.github.io/butterknife/ provides

            this.WireUpControls(); // v0.4 => added this code


            /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ */

            /* The following code may not be the correct way of doing things. But just get it working and resolve it when I have a better understanding of ReactiveUI */

            this.OneWayBind(ViewModel, x => x.Timer, c => c.textViewTimer.Text);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                int i = 0;
                int max = 20;
                while (run_flag)
                {
                    Thread.Sleep(1000);
                    RunOnUiThread(() =>
                    {
                        ViewModel.Timer = (max - i).ToString();
                        i = ++i % max;
                    });
                }
            });
            /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ */



            /* Set the font for the activity title bar*/
            textViewTitle.Typeface = Typeface.CreateFromAsset(Assets, Game_Font);

            /* Set the font for the count-down timer */
            textViewTimer.Typeface = Typeface.CreateFromAsset(Assets, Digital_Font);


                /* Setup the font for the button  */
                void SetupButton(Button btn, int btn_id)
                {
                    btn.Typeface = Typeface.CreateFromAsset(Assets, Game_Font);
                }

            SetupButton(btn01, Resource.Id.btn01);
            SetupButton(btn02, Resource.Id.btn02);
            SetupButton(btn03, Resource.Id.btn03);
            SetupButton(btn04, Resource.Id.btn04);
            SetupButton(btn05, Resource.Id.btn05);
            SetupButton(btn06, Resource.Id.btn06);
            SetupButton(btn07, Resource.Id.btn07);
            SetupButton(btn08, Resource.Id.btn08);
            SetupButton(btn09, Resource.Id.btn09);
            SetupButton(btn10, Resource.Id.btn10);
            SetupButton(btn11, Resource.Id.btn11);
            SetupButton(btn12, Resource.Id.btn12);
            SetupButton(btn13, Resource.Id.btn13);
            SetupButton(btn14, Resource.Id.btn14);
            SetupButton(btn15, Resource.Id.btn15);


            /*
             * The following code bind the hidden word display slot to the View Model
             */
            this.OneWayBind(ViewModel, x => x.Slot01_Image, c => c.Slot01_Image);
            this.OneWayBind(ViewModel, x => x.Slot02_Image, c => c.Slot02_Image);
            this.OneWayBind(ViewModel, x => x.Slot03_Image, c => c.Slot03_Image);
            this.OneWayBind(ViewModel, x => x.Slot04_Image, c => c.Slot04_Image);
            this.OneWayBind(ViewModel, x => x.Slot05_Image, c => c.Slot05_Image);


            /*
             * The following code bind the keyboard to the View Model
             */

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


            /* Following code is to capture button information when user click a button */

            this.Bind(ViewModel, x => x.Btn_Text, c => c.Button_Text);
            this.Bind(ViewModel, x => x.Btn_Tag, c => c.Button_Tag);
            this.OneWayBind(ViewModel, x => x.Toast, c => c.textViewToast.Text);

                void SetButtonID(Button btn)
                {
                    Button_Text = btn.Text ; this.RaisePropertyChanged("Button_Text");
                    Button_Tag = btn.Tag.ToString(); this.RaisePropertyChanged("Button_Tag");
                    //btn.Enabled = false;
                    btn.Visibility = ViewStates.Invisible;
                }

            /* btn01.Click += (sender, e) => SetButtonID(btn01); }; */
            btn01.Click += (sender, e) => SetButtonID(btn01); 
            btn02.Click += (sender, e) => SetButtonID(btn02);
            btn03.Click += (sender, e) => SetButtonID(btn03);
            btn04.Click += (sender, e) => SetButtonID(btn04);
            btn05.Click += (sender, e) => SetButtonID(btn05);
            btn06.Click += (sender, e) => SetButtonID(btn06);
            btn07.Click += (sender, e) => SetButtonID(btn07);
            btn08.Click += (sender, e) => SetButtonID(btn08);
            btn09.Click += (sender, e) => SetButtonID(btn09);
            btn10.Click += (sender, e) => SetButtonID(btn10);
            btn11.Click += (sender, e) => SetButtonID(btn11);
            btn12.Click += (sender, e) => SetButtonID(btn12);
            btn13.Click += (sender, e) => SetButtonID(btn13);
            btn14.Click += (sender, e) => SetButtonID(btn14);
            btn15.Click += (sender, e) => SetButtonID(btn15);

        }

    }
}