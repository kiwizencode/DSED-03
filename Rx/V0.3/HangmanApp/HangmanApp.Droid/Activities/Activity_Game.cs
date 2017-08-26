﻿using System;
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


        private Button btn01;
        private Button btn02;
        private Button btn03;
        private Button btn04;
        private Button btn05;
        private Button btn06;
        private Button btn07;
        private Button btn08;
        private Button btn09;
        private Button btn10;
        private Button btn11;
        private Button btn12;
        private Button btn13;
        private Button btn14;
        private Button btn15;


        private TextView textViewTitle;

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

            /* */
            textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            Typeface typeface = Typeface.CreateFromAsset(Assets, "fonts/FFF_Tusj.ttf");
            textViewTitle.Typeface = typeface;


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


                /*
                 * 
                 */
                void SetupButton(out Button btn, int btn_id)
                {
                    btn = FindViewById<Button>(btn_id);
                    Typeface tf = Typeface.CreateFromAsset(Assets, "fonts/FFF_Tusj.ttf");
                    btn.Typeface = tf;
                }

            SetupButton(out this.btn01, Resource.Id.btn01);
            SetupButton(out this.btn02, Resource.Id.btn02);
            SetupButton(out this.btn03, Resource.Id.btn03);
            SetupButton(out this.btn04, Resource.Id.btn04);
            SetupButton(out this.btn05, Resource.Id.btn05);
            SetupButton(out this.btn06, Resource.Id.btn06);
            SetupButton(out this.btn07, Resource.Id.btn07);
            SetupButton(out this.btn08, Resource.Id.btn08);
            SetupButton(out this.btn09, Resource.Id.btn09);
            SetupButton(out this.btn10, Resource.Id.btn10);
            SetupButton(out this.btn11, Resource.Id.btn11);
            SetupButton(out this.btn12, Resource.Id.btn12);
            SetupButton(out this.btn13, Resource.Id.btn13);
            SetupButton(out this.btn14, Resource.Id.btn14);
            SetupButton(out this.btn15, Resource.Id.btn15);

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


                void ButtonTextUppercase(ref Button btn)
                {
                    var text = btn.Text;
                    btn.Tag = text;
                    btn.Text = text.ToUpper();
                }

            this.WhenAny(x => x.btn01.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn01); });
            this.WhenAny(x => x.btn02.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn02); });
            this.WhenAny(x => x.btn03.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn03); });
            this.WhenAny(x => x.btn04.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn04); });
            this.WhenAny(x => x.btn05.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn05); });
            this.WhenAny(x => x.btn06.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn06); });
            this.WhenAny(x => x.btn07.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn07); });
            this.WhenAny(x => x.btn08.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn08); });
            this.WhenAny(x => x.btn09.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn09); });
            this.WhenAny(x => x.btn10.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn10); });
            this.WhenAny(x => x.btn11.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn11); });
            this.WhenAny(x => x.btn12.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn12); });
            this.WhenAny(x => x.btn13.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn13); });
            this.WhenAny(x => x.btn14.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn14); });
            this.WhenAny(x => x.btn15.Text, _ => string.Empty).Subscribe(
                func => { ButtonTextUppercase(ref this.btn15); });



            //this.OneWayBind(ViewModel, x => x.Btn01, c => c.Btn01);
            //this.OneWayBind(ViewModel, x => x.Btn02, c => c.Btn02);
            //this.OneWayBind(ViewModel, x => x.Btn03, c => c.Btn03);
            //this.OneWayBind(ViewModel, x => x.Btn04, c => c.Btn04);
            //this.OneWayBind(ViewModel, x => x.Btn05, c => c.Btn05);
            //this.OneWayBind(ViewModel, x => x.Btn06, c => c.Btn06);

            //btn01 = FindViewById<Button>(Resource.Id.btn01);
            //btn02 = FindViewById<Button>(Resource.Id.btn02);
            //btn03 = FindViewById<Button>(Resource.Id.btn03);
            //btn04 = FindViewById<Button>(Resource.Id.btn04);
            //btn05 = FindViewById<Button>(Resource.Id.btn05);
            //btn06 = FindViewById<Button>(Resource.Id.btn06);
            //btn07 = FindViewById<Button>(Resource.Id.btn07);
            //btn08 = FindViewById<Button>(Resource.Id.btn08);
            //btn09 = FindViewById<Button>(Resource.Id.btn09);
            //btn10 = FindViewById<Button>(Resource.Id.btn10);
            //btn11 = FindViewById<Button>(Resource.Id.btn11);
            //btn12 = FindViewById<Button>(Resource.Id.btn12);
            //btn13 = FindViewById<Button>(Resource.Id.btn13);
            //btn14 = FindViewById<Button>(Resource.Id.btn14);
            //btn15 = FindViewById<Button>(Resource.Id.btn15);

        }

        //private void SetButton(int id, string text)
        //{
        //    var button = FindViewById<Button>(id);
        //    button.Text = text;
        //    //int image_id = Resources.GetIdentifier(text, "drawable", PackageName);
        //    //button.SetBackgroundResource(image_id);
        //    //button.SetImageResource(image_id);
        //    //button.SetScaleType(ImageView.ScaleType.FitXy);
        //}
        //private string Btn01
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn01, value);
        //}

        //private string Btn02
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn02, value);
        //}

        //private string Btn03
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn03, value);
        //}

        //private string Btn04
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn04, value);
        //}

        //private string Btn05
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn05, value);
        //}

        //private string Btn06
        //{
        //    get => string.Empty;
        //    set => SetButton(Resource.Id.btn06, value);
        //}
    }
}