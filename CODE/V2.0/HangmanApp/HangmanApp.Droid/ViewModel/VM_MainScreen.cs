using ReactiveUI;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_MainScreen : ReactiveObject
    {
        //private ReactiveCommand cmdStartGame;
        //private ReactiveCommand cmdScores;
        //private ReactiveCommand cmdProfile;
        //private ReactiveCommand cmdCredits;

        private string _msg;
        public string Toast {
            get => _msg;
            set { this.RaiseAndSetIfChanged(ref this._msg, value); }
        }
        public ViewModel_MainScreen()
        {

        }
    }
}