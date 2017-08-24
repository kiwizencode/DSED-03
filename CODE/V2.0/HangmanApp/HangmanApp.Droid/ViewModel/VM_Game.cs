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

        public string hidden_word { get; private set; } = "heels";

        private string _slot01_letter;
        public string LetterInSlot01 {
            get => LetterFile + _slot01_letter;
            set => this.RaiseAndSetIfChanged(ref _slot01_letter, value) ;
        }
        private string _slot02_letter;
        public string LetterInSlot02
        {
            get => LetterFile + _slot02_letter;
            set => this.RaiseAndSetIfChanged(ref _slot02_letter, value);
        }
        private string _slot03_letter;
        public string LetterInSlot03
        {
            get => LetterFile + _slot03_letter;
            set => this.RaiseAndSetIfChanged(ref _slot03_letter, value);
        }
        private string _slot04_letter;
        public string LetterInSlot04
        {
            get => LetterFile + _slot04_letter;
            set => this.RaiseAndSetIfChanged(ref _slot04_letter, value);
        }
        private string _slot05_letter;
        public string LetterInSlot05
        {
            get => LetterFile + _slot05_letter;
            set => this.RaiseAndSetIfChanged(ref _slot05_letter, value);
        }
        private void SetSlotImage(string slot, string value)
        {
            _image = LetterFile + value;
            _slot_no = slot;
            this.RaisePropertyChanged(LetterImage);
            this.RaisePropertyChanged(SlotNo);
        }

        private string _slot_no;
        public string SlotNo
        {
            get => _slot_no;
            set => this.RaiseAndSetIfChanged(ref _slot_no, value);
        }

        //private  ObservableAsPropertyHelper<string> _image;
        private string _image;
        public string LetterImage
        {
            get => _image;
            set =>  this.RaiseAndSetIfChanged(ref _image, value);
        } 


        public ViewModel_Game()
        {
            ShowHiddenWord();
        }

        private object _lock = new Object() ;

        public void ShowHiddenWord()
        {
            string getString(char ch)
            {
                return ch.ToString();
            }

            LetterInSlot01 = getString(hidden_word[0]); 
            LetterInSlot02 = getString(hidden_word[1]);
            LetterInSlot03 = getString(hidden_word[2]);
            LetterInSlot04 = getString(hidden_word[3]);
            LetterInSlot05 = getString(hidden_word[4]);
        }
    }
}