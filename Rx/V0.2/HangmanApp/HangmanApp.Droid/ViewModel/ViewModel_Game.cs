using ReactiveUI;

using System;
using System.Reactive.Linq;
//using System.Reactive.Linq;
using System.Linq;

using HangmanApp.Shared.Helper;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        private readonly string LetterFile = "letter_";
        private readonly string QuestionMarkFile = "question_mark";
        private static readonly string BlankFile = "hangman_blank";

        /// <summary>
        /// stores the hidden word
        /// </summary>
        public string hidden_word { get; private set; } = WordsHelper.GetNextWord();

        private string _slot01_letter = "question_mark";
        public string Slot01_Letter
        {
            get => _slot01_letter;
            set => this.RaiseAndSetIfChanged(ref _slot01_letter, LetterFile + value) ;
        }

        private string _slot02_letter = "question_mark";
        public string Slot02_Letter
        {
            get => _slot02_letter;
            set => this.RaiseAndSetIfChanged(ref _slot02_letter, LetterFile + value);
        }

        private string _slot03_letter = "question_mark";
        public string Slot03_Letter
        {
            get => _slot03_letter;
            set => this.RaiseAndSetIfChanged(ref _slot03_letter, LetterFile + value);
        }

        private string _slot04_letter = "question_mark";
        public string Slot04_Letter
        {
            get => _slot04_letter;
            set => this.RaiseAndSetIfChanged(ref _slot04_letter, LetterFile + value);
        }

        private string _slot05_letter = "question_mark";
        public string Slot05_Letter
        {
            get => _slot05_letter;
            set => this.RaiseAndSetIfChanged(ref _slot05_letter, LetterFile + value);
        }



        private string _btn01 = BlankFile;
        public string Btn01
        {
            get => _btn01;
            set => this.RaiseAndSetIfChanged(ref _btn01, LetterFile + value);
        }

        private string _btn02 = BlankFile;
        public string Btn02
        {
            get => _btn02;
            set => this.RaiseAndSetIfChanged(ref _btn02, LetterFile + value);
        }

        private string _btn03 = BlankFile;
        public string Btn03
        {
            get => _btn03;
            set => this.RaiseAndSetIfChanged(ref _btn03, LetterFile + value);
        }
        private string _btn04 = BlankFile;
        public string Btn04
        {
            get => _btn04;
            set => this.RaiseAndSetIfChanged(ref _btn04, LetterFile + value);
        }

        private string _btn05 = BlankFile;
        public string Btn05
        {
            get => _btn05;
            set => this.RaiseAndSetIfChanged(ref _btn05, LetterFile + value);
        }

        private string _btn06 = BlankFile;
        public string Btn06
        {
            get => _btn06;
            set => this.RaiseAndSetIfChanged(ref _btn06, LetterFile + value);
        }

        public ViewModel_Game()
        {
            ShowHiddenWord();

            SetupKeyBoard();
        }

        private void SetupKeyBoard()
        {
            string random_letter = WordsHelper.GenerateRandomLetter(hidden_word);
            for(int i=0; i < 15;i++)
            {
                string value = LetterFile + random_letter[i];
                switch (i)
                {
                    case 1: this.RaiseAndSetIfChanged(ref _btn01, value); break;
                    case 2: this.RaiseAndSetIfChanged(ref _btn02, value); break;
                    case 3: this.RaiseAndSetIfChanged(ref _btn03, value); break;
                    case 4: this.RaiseAndSetIfChanged(ref _btn04, value); break;
                    case 5: this.RaiseAndSetIfChanged(ref _btn05, value); break;
                    case 6: this.RaiseAndSetIfChanged(ref _btn06, value); break;
                }
            }
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