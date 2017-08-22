using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using HangmanApp.Droid.ViewModel;
using System.Reactive.Linq;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "HangmanApp", MainLauncher = true)]
    public class Activity_MainScreen : ReactiveActivity , IViewFor<ViewModel_MainScreen>
    {
        
#region Implementation of IViewFor<> Interface
        private ViewModel_MainScreen _model;
        public ViewModel_MainScreen ViewModel {
            get => _model ;
            set { this.RaiseAndSetIfChanged(ref _model, value); } 
        }
        object IViewFor.ViewModel {
            get => ViewModel;
            set { ViewModel = (ViewModel_MainScreen)value; }
        }
        #endregion
        
        Button btnStartGame;
        Button btnScores;
        Button btnProfile;
        Button btnCredits;

        private TextView textViewMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_MainScreen);

            Initializer();
        }
        
        private void Initializer()
        {
            /*
             * Create the Main Screen View Model
             */
            ViewModel = new ViewModel_MainScreen();

            btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);
            btnScores = FindViewById<Button>(Resource.Id.btnScores);
            btnProfile = FindViewById<Button>(Resource.Id.btnProfile);
            btnCredits = FindViewById<Button>(Resource.Id.btnCredits);

            /*
             * https://reactiveui.net/docs/handbook/commands/binding-commands
            */
            this.BindCommand(ViewModel, x => x.cmdStartGame, c => c.btnStartGame);
            ViewModel.txtStartGame = btnStartGame.Text ;

            this.BindCommand(ViewModel, x => x.cmdScores, c => c.btnScores);
            ViewModel.txtScores = btnScores.Text;

            this.BindCommand(ViewModel, x => x.cmdProfile, c => c.btnProfile);
            ViewModel.txtProfile = btnProfile.Text;

            this.BindCommand(ViewModel, x => x.cmdCredits, c => c.btnCredits);
            ViewModel.txtCredits = btnCredits.Text;

            textViewMessage = FindViewById<TextView>(Resource.Id.textViewMessage);

            this.Bind(this.ViewModel, x => x.Toast, x => x.textViewMessage.Text);
        }



    
    }
}