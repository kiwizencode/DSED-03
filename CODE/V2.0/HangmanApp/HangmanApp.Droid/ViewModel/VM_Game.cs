using ReactiveUI;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        private readonly string LetterFile = "word_";
        private readonly string QuestionMarkFile = "question_mark";

        private char _letter01;
        public string LetterInSlot01
        {
            get => LetterFile + _letter01;
            set => this.RaiseAndSetIfChanged(ref _letter01, value);
        }

        private char _letter02;

        public string LetterInSlot02
        {
            get => LetterFile + _letter02;
            set => this.RaiseAndSetIfChanged(ref _letter02, value);
        }

        private char _letter03;
        public string LetterInSlot03
        {
            get => LetterFile + _letter03;
            set => this.RaiseAndSetIfChanged(ref _letter03, value);
        }

        private char _letter04;
        public string LetterInSlot04
        {
            get => LetterFile + _letter04;
            set => this.RaiseAndSetIfChanged(ref _letter04, value);
        }

        private char _letter05;
        public string LetterInSlot05
        {
            get => LetterFile + _letter05;
            set => this.RaiseAndSetIfChanged(ref _letter05, value);
        }
    }
}