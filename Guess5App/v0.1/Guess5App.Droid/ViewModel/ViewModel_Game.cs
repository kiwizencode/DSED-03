using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using Guess5App.Shared.Helper;
using Guess5App.Shared.Model;
using Guess5App.Shared.DataAccessObject;
using System.Reactive.Concurrency;
using System.Reactive;

namespace Guess5App.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        private static string LetterFile { get; set; } = "letter_";
        private static string QuestionMarkImage { get; set; } = "question_mark";
        private static string HangmanImage { get; set; } = "hangman";

        /* v0.4 added to load hangman image */
        private string _hangman_image = "hangman00"; // set the default image to load when activity start
        public string Hangman_Image
        {
            get => _hangman_image;
            set => this.RaiseAndSetIfChanged(ref _hangman_image, HangmanImage + value.PadLeft(2, '0'));
        }


        public bool Run_Flag { get; set; } = false;

        private int _score = 0;
        public string Score { get => _score.ToString(); set { } }

        private static int MAX_TICK { get; set; } = 20;
        public void TimerTick()
        {
            _timer = (_timer == 0) ? MAX_TICK : _timer - 1;
            this.RaisePropertyChanged("Timer");
        }

        private int _timer = 0;
        public string Timer
        {
            /*  How to add zero-padding to a string
                https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string */
            get => _timer.ToString().PadLeft(2, '0');
            set
            {
                if (int.TryParse(value, out int i)) this.RaiseAndSetIfChanged(ref _timer, i);
            }
        }

        public ReactiveCommand TickCommand { get; private set; }

        public ViewModel_Game()
        {

            /* https://stackoverflow.com/questions/41161896/how-to-create-a-reactivecommand-that-receives-a-string-in-reactiveui-7-with-xama  */
            TickCommand = ReactiveCommand.Create(new Action( delegate
            {
                Run_Flag = !Run_Flag;
                if(Run_Flag)
                {
                    /* reset the game */
                    _timer = 20;
                    this.RaisePropertyChanged("Timer");
                }
                this.RaisePropertyChanged("Run_Flag");
            }));
        }
    }
}