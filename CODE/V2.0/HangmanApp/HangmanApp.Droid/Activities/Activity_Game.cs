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

using HangmanApp.Droid.ViewModel;
using System.Threading;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "Game Activity")]
    public class Activity_Game : ReactiveActivity, IViewFor<ViewModel_Game>
    {

        #region Implementation of IViewFor<> Interface
        private ViewModel_Game _model;
        public ViewModel_Game ViewModel {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        object IViewFor.ViewModel {
            get => ViewModel;
            set => ViewModel = (ViewModel_Game)value;
        }
        #endregion



        ImageView imageslot01;
        ImageView imageslot02;
        ImageView imageslot03;
        ImageView imageslot04;
        ImageView imageslot05;


        private string lot01_letter
        {
            get => null;
            set => imageslot01.SetImageResource((int)typeof(Resource.Drawable).GetField(value).GetValue(null));
        }

        private string lot02_letter
        {
            get => null;
            set => imageslot02.SetImageResource((int)typeof(Resource.Drawable).GetField(value).GetValue(null));
        }
        private string lot03_letter
        {
            get => null;
            set => imageslot03.SetImageResource((int)typeof(Resource.Drawable).GetField(value).GetValue(null));
        }

        private string lot04_letter
        {
            get => null;
            set => imageslot04.SetImageResource((int)typeof(Resource.Drawable).GetField(value).GetValue(null));
        }

        private string lot05_letter
        {
            get => null;
            set => imageslot05.SetImageResource((int)typeof(Resource.Drawable).GetField(value).GetValue(null));
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
            imageslot01 = FindViewById<ImageView>(Resource.Id.ImageSlot01);
            imageslot02 = FindViewById<ImageView>(Resource.Id.ImageSlot02);
            imageslot03 = FindViewById<ImageView>(Resource.Id.ImageSlot03);
            imageslot04 = FindViewById<ImageView>(Resource.Id.ImageSlot04);
            imageslot05 = FindViewById<ImageView>(Resource.Id.ImageSlot05);

            ViewModel = new ViewModel_Game();

            this.OneWayBind(ViewModel, x => x.LetterInSlot01, c => c.lot01_letter);
            this.OneWayBind(ViewModel, x => x.LetterInSlot02, c => c.lot02_letter);
            this.OneWayBind(ViewModel, x => x.LetterInSlot03, c => c.lot03_letter);
            this.OneWayBind(ViewModel, x => x.LetterInSlot04, c => c.lot04_letter);
            this.OneWayBind(ViewModel, x => x.LetterInSlot05, c => c.lot05_letter);
        }
    }
}