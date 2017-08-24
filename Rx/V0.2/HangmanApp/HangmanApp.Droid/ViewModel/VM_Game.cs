using ReactiveUI;

using System;
using System.Reactive.Linq;
//using System.Reactive.Linq;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        private readonly string LetterFile = "letter_";
        private readonly string QuestionMarkFile = "question_mark";

        /// <summary>
        /// stores the hidden word
        /// </summary>
        public string hidden_word { get; private set; } = "apple";

        private string _slot01_letter;
        public string Slot01_Letter
        {
            get => _slot01_letter;
            set => this.RaiseAndSetIfChanged(ref _slot01_letter, LetterFile + value) ;
        }

        private string _slot02_letter;
        public string Slot02_Letter
        {
            get => _slot02_letter;
            set => this.RaiseAndSetIfChanged(ref _slot02_letter, LetterFile + value);
        }

        private string _slot03_letter;
        public string Slot03_Letter
        {
            get => _slot03_letter;
            set => this.RaiseAndSetIfChanged(ref _slot03_letter, LetterFile + value);
        }

        private string _slot04_letter;
        public string Slot04_Letter
        {
            get => _slot04_letter;
            set => this.RaiseAndSetIfChanged(ref _slot04_letter, LetterFile + value);
        }

        private string _slot05_letter;
        public string Slot05_Letter
        {
            get => _slot05_letter;
            set => this.RaiseAndSetIfChanged(ref _slot05_letter, LetterFile + value);
        }

        public ViewModel_Game()
        {
            //ShowHiddenWord();
        }

        private string getString(char ch)
        {
            return ch.ToString();
        }

        private void ShowHiddenWord()
        {
            Slot01_Letter = getString(hidden_word[0]);
            Slot02_Letter = getString(hidden_word[1]);
            Slot03_Letter = getString(hidden_word[2]);
            Slot04_Letter = getString(hidden_word[3]);
            Slot05_Letter = getString(hidden_word[4]);
        }
    }
}