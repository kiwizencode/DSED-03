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

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Game Activity")]
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

        private void SetImageView(int id, string resource)
        {
            ImageView image = FindViewById<ImageView>(id);

            /* How to change the ImageView source dynamically from a string? (Xamarin Android)
             * https://stackoverflow.com/questions/39938391/how-to-change-the-imageview-source-dynamically-from-a-string-xamarin-android */

            int image_id = Resources.GetIdentifier(resource, "drawable", PackageName);
            image.SetImageResource(image_id);

            // or the following code will also work.
            //image.SetImageResource((int)typeof(Resource.Drawable).GetField(resource).GetValue(null));
        }

        private string Slot01_letter
        {
            get => string.Empty;
            set => SetImageView(Resource.Id.ImageSlot01,value);
        }

        private string Slot02_letter
        {
            get => string.Empty;
            set => SetImageView(Resource.Id.ImageSlot02,value);
        }

        private string Slot03_letter
        {
            get => string.Empty;
            set => SetImageView(Resource.Id.ImageSlot03, value);
        }

        private string Slot04_letter
        {
            get => string.Empty;
            set => SetImageView(Resource.Id.ImageSlot04,value);
        }

        private string Slot05_letter
        {
            get => string.Empty;
            set => SetImageView(Resource.Id.ImageSlot05,value);
        }


        private void SetButton(int id, string resource)
        {
            var button = FindViewById<ImageButton>(id);
            int image_id = Resources.GetIdentifier(resource, "drawable", PackageName);

            //button.SetBackgroundResource(image_id);
            button.SetImageResource(image_id);
            button.SetScaleType(ImageView.ScaleType.FitXy);
            
        }
        private string Btn01
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn01, value);
        }

        private string Btn02
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn02, value);
        }

        private string Btn03
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn03, value);
        }

        private string Btn04
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn04, value);
        }

        private string Btn05
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn05, value);
        }

        private string Btn06
        {
            get => string.Empty;
            set => SetButton(Resource.Id.btn06, value);
        }

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

            /*
             * The following code bind the hidden wWord display slot to the View Model
             */
            this.OneWayBind(ViewModel, x => x.Slot01_Letter, c => c.Slot01_letter);
            this.OneWayBind(ViewModel, x => x.Slot02_Letter, c => c.Slot02_letter);
            this.OneWayBind(ViewModel, x => x.Slot03_Letter, c => c.Slot03_letter);
            this.OneWayBind(ViewModel, x => x.Slot04_Letter, c => c.Slot04_letter);
            this.OneWayBind(ViewModel, x => x.Slot05_Letter, c => c.Slot05_letter);

            /*
             * The following code bind the keyboard to the View Model
             */
            this.OneWayBind(ViewModel, x => x.Btn01, c => c.Btn01);
            this.OneWayBind(ViewModel, x => x.Btn02, c => c.Btn02);
            this.OneWayBind(ViewModel, x => x.Btn03, c => c.Btn03);
            this.OneWayBind(ViewModel, x => x.Btn04, c => c.Btn04);
            this.OneWayBind(ViewModel, x => x.Btn05, c => c.Btn05);
            this.OneWayBind(ViewModel, x => x.Btn06, c => c.Btn06);
        }
    }
}