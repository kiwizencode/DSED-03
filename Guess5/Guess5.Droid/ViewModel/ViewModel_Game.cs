using System;
using System.Collections.Generic;
using System.Diagnostics;  // used for debugging purpose
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

using ReactiveUI;

using Guess5.Lib.Helper;
using Guess5.Lib.DataAccessObject;
using Guess5.Lib.Model;

namespace Guess5.Droid.ViewModel
{
    /* Break up ViewModel_Game into 2 partial files :
       1. all coding related to the game logic + reactive-related
       2. all declarations related to GUI data-binding  */

    public partial class ViewModel_Game : ReactiveObject
    {
        /// <summary>
        /// point to the current Activity which is bind to this View Model
        /// for updating (android GUI) View purpose
        /// </summary>
        public ReactiveActivity CurrentActivity { get; set; }

        /* Max. no of guesses allowed by the user in a game.
         * This also correspondance to the number of hangman image to display 
         *    when user makes the wrong guess.*/
        private static int MAX_GUESS { get; set; } = 6;

        /* No of letters in a words. 
         * Even though I have fixed all words to be 5 letters.
         * Should be easy to change the code to handle words with difference length */
        private static int MAX_LETTER { get; set; } = 5;

        /* No of top scorers record to be held in the database. */
        private static int MAX_TOP_SCORES { get; set; } = 3;

        #region Related Properties that keep track of Scores
        /* Current Score */
        private int _score = 0;
        public string Score
        {
            get => _score.ToString();
            set
            {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _score, i);
            }
        }

        /* Highest Score */
        private int _highestscore = 0;
        public string HighestScore
        {
            get => _highestscore > 0 ? _highestscore.ToString() : "";
            set
            {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _highestscore, i);
            }
        }

        /* Correct Answer */
        private int _correct_guess = 0;
        public int Correct_Answer
        {
            get => _correct_guess;
            set => this.RaiseAndSetIfChanged(ref _correct_guess, value);
        }
        #endregion

        #region Timer related properties and functions
        private bool _timer_flag = false;
        public bool Timer_Flag
        {
            get => _timer_flag;
            set => this.RaiseAndSetIfChanged(ref _timer_flag, value);
        }

        #region (A) The following code take care of the timer  when Game Activity is inactive or is finishing
        public void Set_Timer_Flag(bool flag)
        {
            if (timerDisposable != null)
            {
                Timer_Flag = flag;
            }
        }

        public void Dispose()
        {
            if (timerDisposable != null)
                timerDisposable.Dispose();

            UpdateTopScoresChart();

            /* Before Game Activity closed, update Top Score Chart to the database. */
            for (int i = 0; i < MAX_TOP_SCORES; i++)
                ScoreRepository.SaveProfile(score_chart[i]);
        }
        #endregion

        /* list of the letters that user has guessed. */
        //private List<char> user_guess = null;

        #region Declaration of Timer data-binding variable
        private static int MAX_TICK { get; set; } = 20;

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

        #endregion

        #region (F1) Function to enable/disable Game Play 
        /// <summary>
        /// When GUI (View) components, set flag to such that any update to GUI (view)
        ///   will not trigger any logic operation behind the sence
        /// </summary>
        /// <param name="flag"></param>
        private void StartGamePlay(bool flag = true)
        {
            //Is_Game_On = flag;
            Timer_Flag = flag;
        }
        #endregion
        #endregion

        private List<ScoreModel> score_chart;

        public ViewModel_Game(ReactiveActivity activity)
        {
            /* point to the Acitivity class*/
            this.CurrentActivity = activity;
            
            /* Create a Top Scorers chart*/
            RetreiveScoreChart();

            /* (G1) Reset all GUI (View) components */
            InstantiateNewGameGUI();
            HasGameGUI_Setup = true;

            /* (G2) Setup Start Game Command */
            SetupStartCommand();

            /* (Rx) Setup Observables */
            SetupObservable();

            /* (Rx.DB) Setup Observables related to database */
            SetupDbTableObservable();

        }
        #region Functions related to Score Chart
        /// <summary>
        /// Creat a Top Scorers chart from database.
        /// </summary>
        private void RetreiveScoreChart()
        {
            /* When Game Activity is loaded
               get a list/chart of Top Scorers */
            score_chart = new List<ScoreModel>();

            /* 1. Retreive from database */
            foreach (var score in ScoreRepository.GetProfiles().OrderBy(x => x.ID))
                score_chart.Add(score);

        }

        /// <summary>
        /// get the user highest score and update the top score charts.
        /// </summary>
        private void UpdateTopScoresChart()
        {
            /* Check the 'active' profile and retreive last highest score */

            int id = int.Parse(ProfileID);
            ProfileModel profile = ProfileRepository.GetProfile(id);

            ///*  If the current highest score is higher than the old score */
            //if (profile.Scores < _highestscore)
            //{
            //    profile.Scores = _highestscore; // update profile score.
            //    ProfileRepository.SaveProfile(profile);
            //    /* => The user profile always store the user highest score */
            //}

            int i;
            for (i = 0; i < score_chart.Count; i++)
            {
                ScoreModel top_score = score_chart[i];
                if (top_score.Score < _highestscore)
                {
                    ScoreModel score = new ScoreModel();
                    score.Name = profile.Name;
                    score.Score = _highestscore;
                    score_chart.Insert(i, score);
                    break;
                }
            }

            i = 0;
            foreach (var score in ScoreRepository.GetProfiles().OrderBy(x => x.ID))
            {
                score_chart[i].ID = score.ID;
                i++;
            }
        }

        private void UpdateUserProfile()
        {
            int id = int.Parse(ProfileID);
            ProfileModel profile = ProfileRepository.GetProfile(id);

            /*  If the current highest score is higher than the old score */
            if (profile.Scores < _highestscore)
            {
                profile.Scores = _highestscore; // update profile score.
                ProfileRepository.SaveProfile(profile);
                /* => The user profile always store the user highest score */
            }
        }
        #endregion

        #region (G) Setup functions related to GUI (View) Display
        #region (G1) Reset all GUI (View) components (for a new game)
        private bool HasGameGUI_Setup = false;
        /// <summary>
        /// The following code will reset the GUI (View) components 
        ///   for staring of a new games
        /// </summary>
        private void InstantiateNewGameGUI()
        {
            /* (F1) Set Game Play Status to false before GUI update */
            StartGamePlay(false);

            /* (G1.1) Reset ImageView in GUI (View) */
            ResetImageView();

            /* (G1.2) Generate a hidden word. */
            GenerateHiddenWord();

            /* (G1.3) Update 15 buttons GUI (View) */
            ButtonLetterInitializer();

            /* reset other GUI (view) components*/
            /* Score = "0"; // ==> start a brand new game */
            //HighestScore = "0";
        }

        #region (G1.1) Reset ImageView in GUI (View)
        /// <summary>
        /// Reset all ViewImages that holds letters for the hidden word to a '?' image.
        /// </summary>
        private void ResetImageView()
        {
            _slot01_Image = QuestionMarkFile;
            _slot02_Image = QuestionMarkFile;
            _slot03_Image = QuestionMarkFile;
            _slot04_Image = QuestionMarkFile;
            _slot05_Image = QuestionMarkFile;

            /* Refactor such a way => speed up the GUI updaing ??? */
            CurrentActivity.RunOnUiThread(() => {
                this.RaisePropertyChanged("Slot01_Image");
                this.RaisePropertyChanged("Slot02_Image");
                this.RaisePropertyChanged("Slot03_Image");
                this.RaisePropertyChanged("Slot04_Image");
                this.RaisePropertyChanged("Slot05_Image");
            });
        }
        #endregion

        #region (G1.2) Generate a hidden word.
        /// <summary>
        /// stores the hidden word, generated by WordsHelper.GetNextWord().
        /// Please refer to Guess5.Lib.Helper.WordsHelper.cs for detail.
        /// </summary>
        private string hidden_word { get; set; }
        /// <summary>
        /// return a random 5-letters words and save the value to a variable called hidden_word
        /// Please refer to Guess5.Lib.Helper.WordsHelper.cs for detail.
        /// </summary>
        private void GenerateHiddenWord() { hidden_word = WordsHelper.GetNextWord(); }
        #endregion

        #region (G1.3) Update 15 buttons GUI (View)
        /// <summary>
        /// Initialize all image on the 15 buttons to show random letter image.
        /// Letters from the hidden word will also be mixed in the 15 buttons' letter image.
        /// </summary>
        private void ButtonLetterInitializer()
        {
            /* Use the Helper class, WordsHelper.GenerateRandomLetter function
                 to generate a list with 15 random letters (in random sequence), 
                 include letters from the hidden word mixed into it.*/
            string letter_list = WordsHelper.GenerateRandomLetter(hidden_word);

            /* Iterate through the list and updates all 15 buttons individually
                 with a letter from the list.*/
            for (int i = 0; i < 15; i++)
            {
                string value = "" + letter_list[i];

                /* Refactor such a way => speed up the GUI updaing ??? */
                CurrentActivity.RunOnUiThread(() => {
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
                });

            }
        }
        #endregion        
        #endregion

        #region (G2) Setup Start Game Command
        public ReactiveCommand<Unit, Unit> commandStart;
        private void SetupStartCommand()
        {
            /* How to implement Action class
                http://dotnetpattern.com/csharp-action-delegate */
            /* Create a Action for the function StartGame */
            Action doStartAction = new Action(StartGame);
            commandStart = ReactiveCommand.Create(doStartAction);

            //Action doPauseAction = new Action(PauseGame);
            //commandPause = ReactiveCommand.Create(doPauseAction);
        }

        #region (G2.1) Start Game Function
        /// <summary>
        /// The function will be activated when user click the 'Start' button
        /// </summary>
        private void StartGame()
        {
            /* Check whether the Game GUI has been setup  
             * ==> user has won a game and is about to start another new game*/
            if (HasGameGUI_Setup != true)
                /* (G1) Reset all GUI (View) components (for a new game) */
                InstantiateNewGameGUI();

            /* (M0.1) Set Game Play Status to true ==> Game has started !!! */
            StartGamePlay();

            /* Note that all GUI Initialization has been done by InstantiateNewGameGUI(),
                  hence the following code will setup other related variable or function 
                  in order to start a new game */

            /* (G3) reset the hangman counter and display the starting hangmane image */
            ChangeHangmanImage(0);

            /* (Rx.1) Instantiate Time Counter Sequence */
            InstantiateTimeCounter();

            //user_guess = new List<char>();
            HighestScore = Score;
            Score = "0";
            

        }
        #endregion
        #endregion

        #region (G3) Change Hangman Image on GUI (View)
        /// <summary>
        /// Change hangman image to be display onto (GUI) View 
        /// </summary>
        private void ChangeHangmanImage(int index = -1)
        {
            /* Refactor such a way => speed up the GUI updaing ??? */
            CurrentActivity.RunOnUiThread(() =>
            {
                if (index == -1) // by default, alway increment the hangman count
                    _hangman_count++; 
                else
                    _hangman_count = index; //set to user-define hangmane index
                Hangman_Image = _hangman_count.ToString();
            });
            Debug.WriteLine($"Showing Hangman Image No [{_hangman_count}]");
        }
        #endregion

        #region (G4) Show the letter on respective (GUI) ImageView (view)
        /// <summary>
        /// Display the letter onto GUI (view)
        /// </summary>
        /// <param name="i"></param>
        private void ShowHiddenWord(int index)
        {
            string letter = hidden_word[index].ToString();
            switch (index)
            {
                case 0: Slot01_Image = letter; break;
                case 1: Slot02_Image = letter; break;
                case 2: Slot03_Image = letter; break;
                case 3: Slot04_Image = letter; break;
                case 4: Slot05_Image = letter; break;
            }
        }
        #endregion
        #endregion

        #region (Rx) Setup Observables
        private void SetupObservable()
        {
            /* (Rx.2) Trigger an observable when last hangman image is displayed. */
            Last_Hangman_Image_Observable();

            /* (Rx.3) Trigger an observable when user found all the letters in the hidden word. */
            HiddenWord_Found_Observable();

            /* (Rx.4) Trigger an observable when user click one of the 15 buttons. */
            Check_User_Input_Observable();

        }

        #region (Rx.1) Instantiate Time Counter Sequence
        private IDisposable timerDisposable = null;     // Timer Counter Disposable

        /* Define a variable to keep track whether timer counter has completed a round.
           If true, the timer counter can update the hangman image on GUI (view) */
        private bool after_first_round = false;
        
        /// <summary>
        /// Instantiate a new timer counter
        /// </summary>
        private void InstantiateTimeCounter()
        {
            after_first_round = false; // set flag to false

            if(timerDisposable != null)
                timerDisposable.Dispose();

            /* Create a sequence, generate an number every 1 second 
                from a range set by MAX_TICK and repeat the sequence. */

            timerDisposable = Observable
                .Interval(TimeSpan.FromSeconds(1)) // every 1 second
                .Take(MAX_TICK) // only generate MAX_TICK no of number 
                .Distinct()     // must be unique number
                .Repeat()       // keep repeat the sequence
                .Subscribe(value =>  
                {
                    /* Add a check for Timer_Flag due to following conditions :
                       false ==> 1. The game is not started yet or when Game Activity is temperory inactive.
                             ==> 2. Temporarily pause the timer until Timer_Flag is set to true again */
                    if (Timer_Flag) // Only execute when Timer_Flag is true
                    {
                        /* Must run in UI Thread, or else will not work !!! */
                        CurrentActivity.RunOnUiThread(() =>
                        {
                            int counter = MAX_TICK - (int)value;
                            Timer = counter.ToString();

                            // Debug 
                            Debug.WriteLine($"Timer counter : {counter}");
                            // Debug
                            Debug.WriteLine($"Hidden Word is {hidden_word}");

                            /* Let the timer counter do a complete turn before updating the hangman image */
                            if (value == MAX_TICK - 1)
                                after_first_round = true; // set flag to true

                            /* If counter reach MAX_TICK, (after timer has gone through one round)
                                    change the hangman image */
                            if (counter == MAX_TICK && after_first_round)
                            {
                                // Debug
                                Debug.WriteLine($"Reach MAX_TICK ==> change hangman image.");

                                /* (G3) Change hangman image on GUI (View)*/
                                // _hangman_count = 0; // reset the hangman counter
                                ChangeHangmanImage();
                            }
                       
                        });
                    }
                });
        }
        #endregion

        #region (Rx.2) Game Lose. Trigger an observable when last hangman image is displayed. 
        /// <summary>
        /// Trigger an Observable when last hangman image is displayed.
        /// Stop the timer counter.
        /// </summary>
        private void Last_Hangman_Image_Observable()
        {
            /*
                When the screen display the last image of hangman,
                stop the timer, i.e dispose any current running observable.
                Also use this as the counter for number of wrong guess.
             */
            this.WhenAnyValue(x => x.Hangman_Image)
                .Select(flag => (_hangman_count == MAX_GUESS))
                .Subscribe((flag) =>
                {
                    if (flag && timerDisposable != null)
                    {
                        timerDisposable.Dispose();
                        Debug.WriteLine("Timer Counter is Disposed !!!");
                        StartGamePlay(false);
                        //Timer_Flag = false;
                        //Is_Game_On = false;
                        Debug.WriteLine("Stop Timer Counter");
                        Debug.WriteLine("Game Over !!!");

                        /* update user highest score */
                        int total = _score + _highestscore;
                        HighestScore = total.ToString();

                        /* Show the answer to the user */
                        for (int i = 0; i < hidden_word.Length; i++)
                            /* (G4) Show the letter on respective (GUI) ImageView (view) */
                            ShowHiddenWord(i);

                        /* Before starting a brand new game, update Top Scores Chart*/
                        UpdateTopScoresChart();
                        UpdateUserProfile();

                        /* reset the score, start a brand new game. */
                        Score = "0";
                        HasGameGUI_Setup = false;
                        Correct_Answer = 0;
                    }
                });
        }
        #endregion

        #region (Rx.3) Game Won. Trigger an observable when user found all the letters in the hidden word.
        /// <summary>
        /// trigger an observable when user found all the letters in the hidden word,
        /// </summary>
        private void HiddenWord_Found_Observable()
        {
            this.WhenAnyValue(x => x.Correct_Answer)
                .Select(flag => (Correct_Answer == MAX_LETTER))
                .Subscribe((flag) =>
                {
                    if (flag & timerDisposable != null)
                    {
                        timerDisposable.Dispose();
                        Debug.WriteLine("Timer Counter is Disposed !!!");
                        StartGamePlay(false);
                        //Timer_Flag = false;
                        //Is_Game_On = false;
                        Debug.WriteLine("Stop Timer Counter");
                        Debug.WriteLine("Game Won !!!");

                        /* update user highest score */
                        int total = _score + _highestscore;
                        HighestScore = total.ToString();

                        /* Before starting a brand new game, update Top Scores Chart*/
                        UpdateUserProfile();

                        /* set score equal to highest score.
                           ==> ready to start anoher new game. */
                        Score = total.ToString();
                        HasGameGUI_Setup = false;
                        Correct_Answer = 0;
                    }
                });
        }
        #endregion

        #region (Rx.4) Trigger an observable when user click one of the 15 buttons.
        private void Check_User_Input_Observable()
        {
            /*  If the user has click on a button, get letter from the button text,
                check against the hidden word. 
                if the letter is in hidden word, increase the correct guess count
                else change the hangman image ==> increase the wrong guess count */

            this.WhenAny(x => x.Btn_Text, _ => string.Empty)
                .Select(flag => (Btn_Text.ToLower()[0] != '?'))
                .Subscribe((flag) =>
                {
                    if (flag)
                    {
                        /* Get the letter on the button. Always convert to lower-case */
                        char letter = Btn_Text.ToLower()[0]; /* convert string to char */

                        //user_guess.Add(letter);

                        /* Check whether the letter is one of the letters in the hidden word
                             return -1 if not found  */
                        if (hidden_word.LastIndexOf(letter) != -1)
                        {
                            /* (Rx.4.1) Count the number of time the letter appears in the hidden word. */
                            int count = CountLetter(letter);

                            /* Calculate the 'score' for the letter and add it to user total score*/
                            int score = WordsHelper.GetScore(letter);
                            int total = _score + (_timer + score) * count;
                            //this.RaisePropertyChanged("Score"); /* Trigger Property Changed */
                            Score = total.ToString();

                            /* add up the number of correct answer */
                            Correct_Answer += count;

                            /* Update the correct answer counter.
                               If the full (hidden) word is not found yet,
                               restart the timer again. */
                            if (Correct_Answer != MAX_LETTER)
                            {
                                /* Dispose Timer Counter Sequence
                                   There should be only one Timer Counter Sequence running at any time. */
                                timerDisposable.Dispose();
                                Debug.WriteLine("Timer Counter is Disposed !!!");

                                /* (Rx.1) re-Instantiate Time Counter Sequence */
                                //_hangman_count--;
                                InstantiateTimeCounter();
                                Debug.WriteLine("Timer restarted !!!");
                            }
                            else
                            {
                                /* */
                                //total += _highestscore;
                                //HighestScore = total.ToString();
                            }

                        }
                        /* If the letter choosed is not in hidden word. ==> wrong guess !!
                              display the next hangman image until the last hangman image is displayed, 
                              the game will halt. 
                              see (Rx.2) for detail. */
                        else 
                        {
                            /* (G3) Change the hangman image on GUI (view), */
                            ChangeHangmanImage();
                        }
                    }
                });
        }

        #region (Rx.4.1) Count the number of time the letter appears in the hidden word.
        /// <summary>
        /// return the number of time, the input charactor {letter} 
        ///     appears in the {hidden_word}. 
        ///     While do counting, also display the letter onto GUI (view)
        /// return the count
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        int CountLetter(char letter)
        {
            int count = 0;
            for (int i = 0; i < hidden_word.Length; i++)
                if (hidden_word[i] == letter)
                {
                    /* (G4) Show the letter on respective (GUI) ImageView (view) */
                    ShowHiddenWord(i);
                    /* increment the number of timer the letter is found*/
                    count++;
                }
            return count;
        }
        #endregion

        #endregion

        #endregion

        #region (Rx.DB.property) Properties related to database
        private string _profileID = string.Empty;
        public string ProfileID
        {
            get => _profileID;
            set => this.RaiseAndSetIfChanged(ref _profileID, value);
        }

        private string _profileName;
        public string ProfileName
        {
            get => _profileName;
            set => this.RaiseAndSetIfChanged(ref _profileName, value);
        }


        #endregion

        #region (Rx.DB) Setup Observables related to database
        private void SetupDbTableObservable()
        {
            /* (Rx.DB.1) Trigger an observables by Profile ID */
            RetreiveProfileObservable();

        }


        #region (Rx.DB.1) Trigger an observables by Profile ID
        /// <summary>
        /// Retrieve Profile Information by a valide Profile ID, i,e not string.Empty.
        /// </summary>
        private void RetreiveProfileObservable()
        {
            this.WhenAnyValue(x => x.ProfileID)
                .Select(flag => (ProfileID.Trim() != string.Empty))
                .Subscribe((flag) =>
                {
                    Debug.WriteLine($"Is profile present? {flag}");
                    if (flag) // true when user has selected a profile
                    {
                        /* When the user has seleted a profile, set profile to 'active' */
                        int id = int.Parse(ProfileID);
                        ProfileModel profile = ProfileRepository.GetProfile(id);
                        ProfileName = profile.Name;
                    }
                });
        }
        #endregion


        #endregion
    }
}