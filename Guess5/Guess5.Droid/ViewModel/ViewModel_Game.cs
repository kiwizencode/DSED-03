using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Diagnostics;  // used for debugging purpose

using ReactiveUI;

using Guess5.Lib.Helper;

namespace Guess5.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        /// <summary>
        /// point to the current Activity which is bind to this View Model
        /// for updating android GUI purpose
        /// </summary>
        public ReactiveActivity CurrentActivity { get; set; }

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
        /// <summary>
        /// This is the name of the question mark png file name
        /// </summary>
        private static string QuestionMarkImage { get; set; } = "question_mark";

        /// <summary>
        /// variable stores the hidden word.
        /// </summary>
        public string hidden_word { get; private set; }

#region Define Slot Image Variables
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
        #endregion

#region Define Buttons Variables
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
        #endregion

#region Define Variables to retreive Button Number and Letter on the Button
        /* ======================================================================================================*/

        /* store the letter value on the button */
        private static string QuestionMark { get; set; } = "?";
        private string _button_letter = QuestionMark;
        public string Btn_Text
        {
            get => _button_letter;
            set => this.RaiseAndSetIfChanged(ref _button_letter, value);
        }

        /* store the button number in 0n format (where n is a number) */
        public string Btn_Tag { get; set; }

        /* ====================================================================================================== */

        #endregion

#region Define Ticking Meachanism

        /* ===== Declare variable to store the Timer Counter value. ============================= */
        private static int MAX_TICK { get; set; } = 10;
        private int _timer;
        public string Timer
        {
            /*  How to add zero-padding to a string
                https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string */
            get => _timer.ToString().PadLeft(2, '0');
            set
            {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _timer, i);
            }
        }
        /* ===== End of decaration for Timer Counter variable  =================================== */
        #endregion

        private static int MAX_GUESS { get; set; } = 6;
        private static int MAX_COUNT { get; set; } = 5;

        private int _hangman_count { get; set; } = MAX_GUESS;

        /* ==== Declare variable to store hangman image  ========================================================*/
        private string _hangman_image = "hangman00"; // set the default image to load when activity start
        public string Hangman_Image
        {
            get => _hangman_image;
            set => this.RaiseAndSetIfChanged(ref _hangman_image, HangmanImage + value.PadLeft(2, '0'));
        }
        
        /* Correct Answer */
        private int _correct_guess = 0;
        public int Correct_Answer
        {
            get => _correct_guess;
            set => this.RaiseAndSetIfChanged(ref _correct_guess, value);
        }

        /* Current Score */
        private int _score;
        public string Score {
            get => _score.ToString();
            set {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _score, i);
            } // add this bit of code so that the Reactive UI data bind will work 
        }

        /* Highest Score */
        private int _highestscore = 0;
        public string HighestScore  {
            get => _highestscore > 0 ? _highestscore.ToString() : "";
            set {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _highestscore, i);
            } // add this bit of code so that the Reactive UI data bind will work
        }

        /* ==================================================================================================== */

        private bool _timer_flag = false;
        public bool Timer_Flag
        {
            get => _timer_flag;
            set => this.RaiseAndSetIfChanged(ref _timer_flag, value);
        }
        /* ======================================================= */



        public ReactiveCommand<Unit, Unit> commandStart;

        private IDisposable timerCounterDisposable = null;


        public void Resume()
        {
            if (timerCounterDisposable != null)
            {
                Timer_Flag = true;
                //this.RaisePropertyChanged("timer_flag");
            }
        }

        public void Stop()
        {
            if (timerCounterDisposable != null)
            {
                Timer_Flag = false;
                //this.RaisePropertyChanged("timer_flag");
            }
        }

        public void Dispose()
        {
            if (timerCounterDisposable != null)
                timerCounterDisposable.Dispose();
        }


        public ViewModel_Game()
        {
            GenerateHiddenWord();

            ButtonLetterInitializer();

            //Is_Game_Still_Running = false;

            InitializeReactiveCommand();

            SetupObservable();

            /* for debug purpose */
            //hidden_word = "heels";
            //ShowHiddenWord();
            //SetTimer();
        }



        /// <summary>
        /// function that return a random 5 letters hidden words
        /// </summary>
        private void GenerateHiddenWord() { hidden_word = WordsHelper.GetNextWord();
            // Debug
            Debug.WriteLine($"Hidden Word is {hidden_word}");
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

        private void InitializeReactiveCommand()
        {
            /* 
             * http://dotnetpattern.com/csharp-action-delegate 
             */

            // Debug
            //Is_Game_Still_Running = true;

            //void doStartButton()
            //{
            //    /* check whether any game is running*/
            //    if (!Is_Game_Still_Running)
            //    {
            //        //Is_Game_Still_Running = true;
            //    }
            //    else
            //    {
            //        //Is_Game_Still_Running = false;
  
            //    }

            //    //Restart_Timer = true;
            //    //this.RaisePropertyChanged("Restart_Timer");

            //    //// Debug
            //    //Debug.WriteLine($"Is_Game_Still_Running {Is_Game_Still_Running}");
            //    //Debug.WriteLine($"Restart_Timer {Restart_Timer}");
            //}

            Action doStartAction = new Action(startGame);
            //commandStart = ReactiveCommand.Create(doStartAction);

            //Action restartTimerAction = new Action(restartTimerCounter);
            commandStart = ReactiveCommand.Create(doStartAction);
        }

        /* ========= DEBUG ========================== */
        
        private void startGame()
        {
            Correct_Answer = 0;
            HighestScore = "0";
            Score = "0";
            restartTimerCounter();
        }
            
            
        /* create a Action to test the timer counter */

        private void restartTimerCounter()
        {
            //RunOnUiThread(() => ViewModel.TimerTick2(counter));
            /* Dispose if an obervable for the timer counter is running */
            if (timerCounterDisposable != null)
            {
                timerCounterDisposable.Dispose();
            }

            //_timer = MAX_COUNT;
            //this.RaisePropertyChanged("Timer");
            Timer_Flag = true;

            //if (timerCounterDisposable != null)
            //    _hangman_count = _hangman_count;

            if (_hangman_count != MAX_GUESS) _hangman_count--;

            /* 
             https://social.msdn.microsoft.com/Forums/en-US/c4acaf34-3136-4206-a6f9-ef5afba74b2b/interval-with-immediate-start?forum=rx
             */

            timerCounterDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                        .Take(MAX_TICK)
                        .Distinct()
                        //.Delay(TimeSpan.FromSeconds(1))
                        .Repeat()
                        .Subscribe(value =>
                        {
                            if (Timer_Flag)
                            {
                                CurrentActivity.RunOnUiThread(() =>
                                {
                                    var counter = MAX_TICK - (int)value;
                                        //_timer = counter;
                                        //this.RaisePropertyChanged("Timer");
                                        Timer = counter.ToString();
                                    Debug.WriteLine($"Timer counter : {_timer}");


                                });
                            }
                        });
        }

        private void SetupObservable()
        {
            /* 
                If the timer reach MAX_TICK, show the next hangman png
             */
            this.WhenAnyValue(x => x.Timer)
                .Select(value => (Timer == MAX_TICK.ToString() && Timer_Flag))
                .Subscribe((value) =>
                {
                    if(value)
                    {
                        //SetHangmanImage();
                        Debug.WriteLine($"MAX TICK : {value}");
                        GetNextHangmanImage();

                        //if(_hangman_count != 0)
                        //    Wrong_Answer++;
                        //Debug.WriteLine($"Wrong Answer : {Wrong_Answer}");
                    }
                        
                });

            /*
                When the screen display the last image of hangman,
                stop the timer, i.e dispose any current running observable.
                Also use this as the counter for number of wrong guess.
             */
            this.WhenAnyValue(x => x.Hangman_Image)
                .Select(value => (_hangman_count == MAX_GUESS))
                .Subscribe((value) => 
                {
                    if(value && timerCounterDisposable != null)
                    {
                        timerCounterDisposable.Dispose();
                        //Is_Game_Still_Running = false;
                        Timer_Flag = false;
                        Debug.WriteLine("Stop Timer Counter");
                        Debug.WriteLine("Game Over !!!");
                    }
                });

            //throw new NotImplementedException();

            /*
                If the user has click on a button, get letter from the button text,
                check against the hidden word. 
                if the letter is in hidden word, increase the correct guess count
                else change the hangman image ==> increase the wrong guess count
             */
            this.WhenAny(x => x.Btn_Text  , _ => string.Empty)
                .Subscribe( _ => {
                    /* get the letter on the button */
                    char letter = Btn_Text.ToLower()[0]; /* convert string to char */

                    /* Check whether the letter is in the hidden word
                        *   return -1 if not found
                        */
                    if (hidden_word.LastIndexOf(letter) != -1)
                    {
                        /* Count the number of time the letter appear in the hidden words*/
                        int count = CountLetter(letter);

                        /* add up the number of correct answer */
                        Correct_Answer += count;
                        
                        /* Calculate the score and update to total score*/
                        int score = WordsHelper.GetScore(letter);
                        _score += (_timer + score) * count;
                        this.RaisePropertyChanged("Score"); /* Trigger Property Changed */

                        //timer_flag = false;
                        //this.RaisePropertyChanged("timer_flag");
                        restartTimerCounter();

                    }
                    else
                    {
                        if(letter != '?')
                            GetNextHangmanImage();
                    }

                });

            /* 
                check the correct guess
             */
            this.WhenAnyValue(x => x.Correct_Answer)
                .Select(value =>(Correct_Answer == MAX_GUESS))
                .Subscribe((value) => {
                    if (value && timerCounterDisposable != null)
                    {
                        timerCounterDisposable.Dispose();
                        //Is_Game_Still_Running = false;
                        Timer_Flag = false;
                        Debug.WriteLine("Stop Timer Counter");
                        Debug.WriteLine("Game Won !!!");
                        
                        //this.RaisePropertyChanged("timer_flag");

                        _highestscore += _score;
                        this.RaisePropertyChanged("HighestScore"); /* Trigger Property Changed */
                    }
                });

        }

        /* 
            count the number of time, the letter appears in the hidden word.
        */
        /// <summary>
        /// Count the number of time, the input charactor, letter, 
        ///     appears in the string array, hidden_word
        /// Return the count
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        int CountLetter(char letter)
        {
            int count = 0;
            for (int i = 0; i < hidden_word.Length; i++)
                if (hidden_word[i] == letter)
                {
                    ShowHiddenWord(i + 1);
                    Correct_Answer++;
                    count++;
                }
            return count; 
        }

        /*
            if the user guess the correct letter, show the correct letter in the correct position in image slot
         */
        private void ShowHiddenWord(int i)
        {
            string getString(char ch) => ch.ToString();

            switch (i)
            {
                case 1: Slot01_Image = getString(hidden_word[0]); break;
                case 2: Slot02_Image = getString(hidden_word[1]); break;
                case 3: Slot03_Image = getString(hidden_word[2]); break;
                case 4: Slot04_Image = getString(hidden_word[3]); break;
                case 5: Slot05_Image = getString(hidden_word[4]); break;
            }
        }
        public void GetNextHangmanImage()
        {
            _hangman_count = ++_hangman_count % (MAX_GUESS+1);
            Hangman_Image = _hangman_count.ToString();
            //this.RaisePropertyChanged("Hangman_Image");
            Debug.WriteLine($"Hangman Image {_hangman_count}");
        }

    }
}