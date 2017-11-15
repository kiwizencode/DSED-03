using System;
using System.Reactive.Linq;

using ReactiveUI;

using Guess5.Lib.Helper;
using Guess5.Lib.Model;
using System.Reactive;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Guess5.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        /// <summary>
        /// This is part of the alphabet letter png filenames.
        /// The naming convention for the png file is "letter_?.png" where ? is the alphabet
        /// </summary>
        private static string LetterFile { get; set; } = "letter_";
        
        /// <summary>
        /// This is part of the hangman image png filenames.
        /// The naming convention for the png file is "hangman_0n.png" where n is a number
        /// </summary>
        private static string HangmanImage { get; set; } = "hangman";

        private static string QuestionMarkImage { get; set; } = "question_mark";

        /// <summary>
        /// variable stores the hidden word.
        /// </summary>
        public string hidden_word { get; private set; }

        /* ==== Declare variables to store hiddent letter image. I have called them Slot Image =================*/
        private string _slot01_Image = QuestionMarkImage;
        public string Slot01_Image {
            get => _slot01_Image;
            set => this.RaiseAndSetIfChanged(ref _slot01_Image, LetterFile + value);
        }

        private string _slot02_Image = QuestionMarkImage;
        public string Slot02_Image {
            get => _slot02_Image;
            set => this.RaiseAndSetIfChanged(ref _slot02_Image, LetterFile + value);
        }

        private string _slot03_Image = QuestionMarkImage;
        public string Slot03_Image {
            get => _slot03_Image;
            set => this.RaiseAndSetIfChanged(ref _slot03_Image, LetterFile + value);
        }

        private string _slot04_Image = QuestionMarkImage;
        public string Slot04_Image {
            get => _slot04_Image;
            set => this.RaiseAndSetIfChanged(ref _slot04_Image, LetterFile + value);
        }

        private string _slot05_Image = QuestionMarkImage;
        public string Slot05_Image {
            get => _slot05_Image;
            set => this.RaiseAndSetIfChanged(ref _slot05_Image, LetterFile + value);
        }

        /* ==== End of Slot Image Declaration ===================================================================*/

        /* ==== Declare variable to store hangman image  ========================================================*/
        private string _hangman_image = "hangman00"; // set the default image to load when activity start
        public string Hangman_Image {
            get => _hangman_image;
            set => this.RaiseAndSetIfChanged(ref _hangman_image, HangmanImage + value.PadLeft(2, '0'));
        }
        /* ======================================================================================================*/


        /* ==== Declare variable to store letter for each button on the view/screen =============================*/

        /* There are total 15 buttons. Hence have to declare 15 variables to store letter for each button */

        private string _btn01 = string.Empty;
        public string Btn01 {
            get => _btn01.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn01, value);
        }

        private string _btn02 = string.Empty;
        public string Btn02 {
            get => _btn02.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn02, value);
        }

        private string _btn03 = string.Empty;
        public string Btn03 {
            get => _btn03.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn03, value);
        }

        private string _btn04 = string.Empty;
        public string Btn04 {
            get => _btn04.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn04, value);
        }

        private string _btn05 = string.Empty;
        public string Btn05 {
            get => _btn05.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn05, value);
        }

        private string _btn06 = string.Empty;
        public string Btn06 {
            get => _btn06.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn06, value);
        }

        private string _btn07 = string.Empty;
        public string Btn07 {
            get => _btn07.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn07, value);
        }

        private string _btn08 = string.Empty;
        public string Btn08 {
            get => _btn08.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn08, value);
        }

        private string _btn09 = string.Empty;
        public string Btn09 {
            get => _btn09.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn09, value);
        }

        private string _btn10 = string.Empty;
        public string Btn10 {
            get => _btn10.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn10, value);
        }

        private string _btn11 = string.Empty;
        public string Btn11 {
            get => _btn11.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn11, value);
        }

        private string _btn12 = string.Empty;
        public string Btn12 {
            get => _btn12.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn12, value);
        }

        private string _btn13 = string.Empty;
        public string Btn13 {
            get => _btn13.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn13, value);
        }

        private string _btn14 = string.Empty;
        public string Btn14 {
            get => _btn14.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn14, value);
        }

        private string _btn15 = string.Empty;
        public string Btn15 {
            get => _btn15.ToUpper();
            set => this.RaiseAndSetIfChanged(ref _btn15, value);
        }

        /* ==== End of declaration of button letter variables ================================================ */

        /* =================================================================================================== */

        /* Current Score */
        private int _score;
        public string Score {
            get => _score.ToString();
            set { } // add this bit of code so that the Reactive UI data bind will work 
        }

        /* Highest Score */
        private int _highestscore = 0;
        public string HighestScore  {
            get => _highestscore > 0 ? _highestscore.ToString() : "";
            set { } // add this bit of code so that the Reactive UI data bind will work 
        }

        /* ==================================================================================================== */

        /* ===== Declare variable to store the Timer Counter value. ============================= */
        private int _timer;
        public string Timer  {
            /*  How to add zero-padding to a string
                https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string */
            get => _timer.ToString().PadLeft(2, '0');
            set {
                if (int.TryParse(value, out int i)) this.RaiseAndSetIfChanged(ref _timer, i);
            }
        }
        /* ===== End of decaration for Timer Counter variable  =================================== */

        /*
            This flag check whether the "game" is still running.
            The flag is set to true whenever a new game started.
            It will remain true during the game session.
            The flag is set to false when the user lose a game
         */
        private bool _is_running_flag;
        public bool Is_Game_Still_Running {
            get => _is_running_flag ;
            set => this.RaiseAndSetIfChanged(ref _is_running_flag, value);
        }

        private static int MAX_TICK { get; set; } = 20;
        public void TimerTick()
        {
            if (Is_Game_Still_Running)
            {
                _timer = (_timer == 0) ? MAX_TICK : _timer - 1;
                this.RaisePropertyChanged("Timer");
            }
        }

        /* ======================================================= */


        /* the winning/lossing flag */
        private bool _winning_flag = false;
        public bool Is_Game_Won {
            get => _winning_flag;
            set => this.RaiseAndSetIfChanged(ref _winning_flag, value);
        }

        /* store the button number in 0n format (where n is a number) */
        public string Btn_Tag { get; set; }

        /* store the letter value on the button */
        private static string QuestionMark { get; set; } = "?";
        private string _button_letter = QuestionMark;
        public string Btn_Text {
            get => _button_letter;
            set => this.RaiseAndSetIfChanged(ref _button_letter, value);
        }

        private string _toast;
        public string Toast { get => _toast; set => this.RaiseAndSetIfChanged(ref _toast, value); }
        private static int MAX_GUESS { get; set; } = 6;
        private static int MAX_COUNT { get; set; } = 5;

        private int _worng_guess = 0;
        private int _correct_guess = 0;
        private int _hangman_count = 6;

        /// <summary>
        /// function that return a random 5 letters hidden words
        /// </summary>
        private void GenerateHiddenWord() { hidden_word = WordsHelper.GetNextWord(); }

        public ReactiveCommand<Unit, Unit> commandStart;

        public ViewModel_Game()
        {
            GenerateHiddenWord();

            ButtonLetterInitializer();

            //Is_Game_Still_Running = false;


            /* 
             * http://dotnetpattern.com/csharp-action-delegate 
             */
            void doStartButton()
            {
                /* check whether any game is running*/
                if (!Is_Game_Still_Running)
                {
                    Is_Game_Still_Running = true;

                    Debug.WriteLine("Is_Game_Still_Running has been set");
                }
                else
                {
                    // do nothing
                    Debug.WriteLine("Click === doing nothing");
                }
            }

            Action doStartAction = new Action(doStartButton);
            commandStart = ReactiveCommand.Create(doStartAction);

            /* for debug purpose */
            //hidden_word = "heels";
            //ShowHiddenWord();
            //SetTimer();
        }




        private void CommentOut()
        {
            this.WhenAny(x => x.Btn_Text, _ => string.Empty).Subscribe(Func => {

                /* get the letter on the button */
                char ch = Btn_Text.ToLower()[0]; /* convert string to char */
                if (hidden_word.LastIndexOf(ch) != -1)
                {
                    /* Count the number of time the letter appear in the hidden words*/
                    int count = CountLetter(ch);

                    /* Calculate the score and update to total score*/
                    int score = WordsHelper.GetScore(ch);
                    _score += (_timer + score) * count;
                    this.RaisePropertyChanged("Score"); /* Trigger Property Changed */


                    /* Since the user has found letter/letters in the hidden word.
                       reset the timer to MAX_TICK*/
                    //_timer = MAX_TICK;

                    /* v0.6 update the highest score */
                    if (_correct_guess == MAX_COUNT)
                    {
                        _highestscore += _score;
                        this.RaisePropertyChanged("HighestScore"); /* Trigger Property Changed */

                        _winning_flag = true;
                        this.RaisePropertyChanged("IsWinning"); /* Trigger Property Changed */
                    }
                }
                else
                {
                    SetHangmanImage(); /* set the hangmen image*/
                                       //_timer = MAX_TICK; /* reset the timer counter */
                                       /* User has so amny guesses based on  MAX_GUESS */
                }

                if (Is_Game_Still_Running)
                    _timer = MAX_TICK; /* reset the timer counter */
                this.RaisePropertyChanged("Timer"); /* Trigger Property Changed */

                /* Not a best way of doing */
                _button_letter = QuestionMark;
            });

            this.WhenAny(x => x.Btn_Text, _ => string.Empty).Subscribe( Func =>  {

                /* get the letter on the button */
                char ch = Btn_Text.ToLower()[0]; /* convert string to char */
                if (hidden_word.LastIndexOf(ch) != -1)
                {
                    /* Count the number of time the letter appear in the hidden words*/
                    int count = CountLetter(ch);

                    /* Calculate the score and update to total score*/
                    int score = WordsHelper.GetScore(ch);
                    _score += (_timer + score ) * count ;
                    this.RaisePropertyChanged("Score"); /* Trigger Property Changed */


                    /* Since the user has found letter/letters in the hidden word.
                       reset the timer to MAX_TICK*/
                    //_timer = MAX_TICK;

                    /* v0.6 update the highest score */
                    if (_correct_guess==MAX_COUNT)
                    {
                        _highestscore += _score;
                        this.RaisePropertyChanged("HighestScore"); /* Trigger Property Changed */

                        _winning_flag = true;
                        this.RaisePropertyChanged("IsWinning"); /* Trigger Property Changed */
                    }
                }
                else
                {
                    SetHangmanImage(); /* set the hangmen image*/
                                       //_timer = MAX_TICK; /* reset the timer counter */
                                       /* User has so amny guesses based on  MAX_GUESS */
                }

                if(Is_Game_Still_Running)
                    _timer = MAX_TICK; /* reset the timer counter */
                this.RaisePropertyChanged("Timer"); /* Trigger Property Changed */

                /* Not a best way of doing */
                _button_letter = QuestionMark;
            });

            this.WhenAny(x => x.Timer, _ => string.Empty).Subscribe( Func =>  {
                if(_timer==0) {
                    SetHangmanImage();
                }
            });
            
            int CountLetter(char letter)
            {
                int count = 0;
                for (int i = 0; i < hidden_word.Length; i++)
                    if (hidden_word[i] == letter)
                    {
                        ShowHiddenWord(i + 1);
                        _correct_guess++;
                        if (_correct_guess == MAX_COUNT)
                        {
                            Is_Game_Still_Running = false;
                            this.RaisePropertyChanged("Run_Flag");
                        }
                        count++;
                    }
                return count;
            }

        }

        public void SetHangmanImage()
        {

            _hangman_count = ++_hangman_count % 7;
            Hangman_Image = _hangman_count.ToString();
            this.RaisePropertyChanged("Hangman_Image");

            /* increment wrong guess counter 
               but not during intialization of button when the letter is question mark */
            if (Hangman_Image != "hangman00")
                _worng_guess++;

            if (_worng_guess == MAX_GUESS)
            {
                _highestscore += _score;
                this.RaisePropertyChanged("HighestScore"); /* Trigger Property Changed */

                _winning_flag = false;
                this.RaisePropertyChanged("IsWinning"); /* Trigger Property Changed */

                Is_Game_Still_Running = false; /* set the flag to start the timer */
                this.RaisePropertyChanged("Run_Flag"); /* Trigger Property Changed */
            }

        }

        public void Reset()
        {
            Is_Game_Still_Running = true;
            this.RaisePropertyChanged("Run_Flag");

            _hangman_count = 0;
            _hangman_image = "hangman00";
            this.RaisePropertyChanged("Hangman_Image");

            _slot01_Image = QuestionMarkImage;
            this.RaisePropertyChanged("Slot01_Image");

            _slot02_Image = QuestionMarkImage;
            this.RaisePropertyChanged("Slot02_Image");

            _slot03_Image = QuestionMarkImage;
            this.RaisePropertyChanged("Slot03_Image");

            _slot04_Image = QuestionMarkImage;
            this.RaisePropertyChanged("Slot04_Image");

            _slot05_Image = QuestionMarkImage;
            this.RaisePropertyChanged("Slot05_Image");

            _score = 0;
            this.RaisePropertyChanged("Score");

            _timer = MAX_TICK;
            this.RaisePropertyChanged("Timer");

            _correct_guess = 0;
            _worng_guess = 0;
            _winning_flag = false;
            
            GenerateHiddenWord();

            ButtonLetterInitializer();
        }

        /// <summary>
        /// Initialize all the 15 letters buttons "keyboard" at the start of a new game.
        /// It will contain
        /// </summary>
        private void ButtonLetterInitializer()
        {
            string letter_list = WordsHelper.GenerateRandomLetter(hidden_word);
            for (int i = 0; i < 15; i++)
            {
                string value = "" + letter_list[i];
                switch (i + 1)
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




        private void ShowHiddenWord(int i)
        {
            string getString(char ch) =>  ch.ToString();

            switch (i)
            {
                case 1: Slot01_Image = getString(hidden_word[0]); break;
                case 2: Slot02_Image = getString(hidden_word[1]); break;
                case 3: Slot03_Image = getString(hidden_word[2]); break;
                case 4: Slot04_Image = getString(hidden_word[3]); break;
                case 5: Slot05_Image = getString(hidden_word[4]); break;
            }
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