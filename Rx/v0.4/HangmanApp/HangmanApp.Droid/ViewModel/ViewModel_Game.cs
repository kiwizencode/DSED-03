using ReactiveUI;

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;

using HangmanApp.Shared.Helper;
using Android.Widget;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        private readonly string LetterFile = "letter_";
        private static readonly string QuestionMarkImage = "question_mark";
        private static readonly string BlankFile = "hangman_blank";

        /// <summary>
        /// stores the hidden word
        /// </summary>
        public string hidden_word { get; private set; } 

        private string _slot01_Image = QuestionMarkImage ;
        public string Slot01_Image
        {
            get => _slot01_Image;
            set => this.RaiseAndSetIfChanged(ref _slot01_Image, LetterFile + value) ;
        }

        private string _slot02_Image = QuestionMarkImage;
        public string Slot02_Image
        {
            get => _slot02_Image;
            set => this.RaiseAndSetIfChanged(ref _slot02_Image, LetterFile + value);
        }

        private string _slot03_Image = QuestionMarkImage;
        public string Slot03_Image
        {
            get => _slot03_Image;
            set => this.RaiseAndSetIfChanged(ref _slot03_Image, LetterFile + value);
        }

        private string _slot04_Image = QuestionMarkImage;
        public string Slot04_Image
        {
            get => _slot04_Image;
            set => this.RaiseAndSetIfChanged(ref _slot04_Image, LetterFile + value);
        }

        private string _slot05_Image = QuestionMarkImage;
        public string Slot05_Image
        {
            get => _slot05_Image;
            set => this.RaiseAndSetIfChanged(ref _slot05_Image, LetterFile + value);
        } 


        private string _btn01 = string.Empty;
        public string Btn01
        {
            get => _btn01.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn01, value);
        }

        private string _btn02 = string.Empty;
        public string Btn02
        {
            get => _btn02.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn02, value);
        }

        private string _btn03 = string.Empty;
        public string Btn03
        {
            get => _btn03.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn03, value);
        }
        private string _btn04 = string.Empty;
        public string Btn04
        {
            get => _btn04.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn04, value);
        }

        private string _btn05 = string.Empty;
        public string Btn05
        {
            get => _btn05.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn05, value);
        }

        private string _btn06 = string.Empty;
        public string Btn06
        {
            get => _btn06.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn06, value);
        }

        private string _btn07 = string.Empty;
        public string Btn07
        {
            get => _btn07.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn07, value);
        }

        private string _btn08 = string.Empty;
        public string Btn08
        {
            get => _btn08.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn08, value);
        }

        private string _btn09 = string.Empty;
        public string Btn09
        {
            get => _btn09.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn09, value);
        }
        private string _btn10 = string.Empty;
        public string Btn10
        {
            get => _btn10.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn10, value);
        }

        private string _btn11 = string.Empty;
        public string Btn11
        {
            get => _btn11.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn11, value);
        }

        private string _btn12 = string.Empty;
        public string Btn12
        {
            get => _btn12.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn12, value);
        }

        private string _btn13 = string.Empty;
        public string Btn13
        {
            get => _btn13.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn13, value);
        }
        private string _btn14 = string.Empty;
        public string Btn14
        {
            get => _btn14.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn14, value);
        }

        private string _btn15 = string.Empty;
        public string Btn15
        {
            get => _btn15.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn15, value);
        }

        private int _timer;
        public string Timer
        {
            get => _timer.ToString();
            set
            {
                int i;
                if (int.TryParse(value, out i))
                {
                    this.RaiseAndSetIfChanged(ref _timer, i);
                }
            }
        }
        private string _button_letter;
        public string Btn_Text
        {
            get => _button_letter ;
            set
            {
                char ch = value.ToLower()[0];
                if (hidden_word.LastIndexOf(ch) != -1)
                    Toast = "Letter Found !!!";
                else
                    Toast = "Wrong Letter !!!";
                this.RaiseAndSetIfChanged(ref _button_letter, value);
            }
        }
        public string Btn_Tag { get; set; }

        private string _toast;
        public string Toast
        {
            get => _toast;
            set => this.RaiseAndSetIfChanged(ref _toast, value);
        }

        public ViewModel_Game()
        {
            GenerateHiddenWord();

            ShowHiddenWord();

            ButtonLetterInitializer();

            //SetTimer();
        }

        private void GenerateHiddenWord() { hidden_word = WordsHelper.GetNextWord(); }


        /// <summary>
        /// Initialize all the 15 letters buttons "keyboard" at the start of a new game.
        /// It will contain
        /// </summary>
        private void ButtonLetterInitializer()
        {
            string letter_list = WordsHelper.GenerateRandomLetter(hidden_word);
            for(int i=0; i < 15;i++)
            {
                string value = "" + letter_list[i];
                switch (i+1)
                {
                    case 1: Btn01 = value; break;
                    case 2: Btn02 = value; break;
                    case 3: Btn03 = value; break;
                    case 4: Btn04 = value; break;
                    case 5: Btn05 = value; break;
                    case 6: Btn06 = value; break;
                    case 7: Btn07 = value; break;
                    case 8: Btn08 = value; break;
                    case 9: Btn09 = value; break;
                    case 10: Btn10 = value; break;
                    case 11: Btn11 = value; break;
                    case 12: Btn12 = value; break;
                    case 13: Btn13 = value; break;
                    case 14: Btn14 = value; break;
                    case 15: Btn15 = value; break;
                }
            }
        }


        private string getString(char ch)
        {
            return ch.ToString();
        }

        private void ShowHiddenWord()
        {
            Slot01_Image = getString(hidden_word[0]);
            Slot02_Image = getString(hidden_word[1]);
            Slot03_Image = getString(hidden_word[2]);
            Slot04_Image = getString(hidden_word[3]);
            Slot05_Image = getString(hidden_word[4]);
        }

        //private void SetTimer()
        //{
        //    /* http://www.introtorx.com/Content/v1.0.10621.0/04_CreatingObservableSequences.html#ObservableTimer  */
        //    var interval = Observable.Interval(TimeSpan.FromMilliseconds(1000));

        //    interval.ObserveOn(Scheduler.TaskPool)
        //        .Subscribe(i => Timer = (i % 10).ToString());
        //}
    }
}