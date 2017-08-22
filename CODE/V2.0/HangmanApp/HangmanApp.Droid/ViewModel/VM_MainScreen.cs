using ReactiveUI;
//using ReactiveUI.Legacy;

using System.Linq;
using System.Reactive.Linq;
using System.Reactive;

using Xamarin.Forms;
using System;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_MainScreen : ReactiveObject
    {
        private string _activity_text ;
        public string ActivityName
        {
            get => _activity_text;
            set => this.RaiseAndSetIfChanged(ref _activity_text, value);
        }

        public string txtStartGame;
        public ReactiveCommand cmdStartGame;

        public string txtScores;
        public ReactiveCommand cmdScores;

        public string txtProfile;
        public ReactiveCommand cmdProfile;

        public string txtCredits;
        public ReactiveCommand cmdCredits;

        //public ReactiveCommand cmdActivity { get; }        

        private string _msg;
        public string Toast {
            get => _msg;
            set { this.RaiseAndSetIfChanged(ref _msg, value); }
        }
        public ViewModel_MainScreen()
        {
            //cmdActivity = ReactiveCommand.Create<string>(StartActivity);

            cmdStartGame = ReactiveCommand.Create(() => Toast = txtStartGame);
            cmdScores = ReactiveCommand.Create(() => Toast = txtScores);
            cmdProfile = ReactiveCommand.Create(() => Toast = txtProfile);
            cmdCredits = ReactiveCommand.Create(() => Toast = txtCredits);
        }

        //private void StartActivity()
        //{
        //    //ActivityName = text;
        //    Toast = text;
        //}

    }
}