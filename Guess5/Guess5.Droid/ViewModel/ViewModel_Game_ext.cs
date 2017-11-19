﻿using System;
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
       1. all declarations related to GUI data-binding
       2. all coding related to the game logic + reactive-related
    */

    /* This file will contains all coding related to the game logic + reactive-related */

    public partial class ViewModel_Game : ReactiveObject
    {
        #region Declaration of constant properties
        /* Max. no of guesses allowed by the user in a game.
         * This also correspondance to the number of hangman image to display 
         *    when user makes the wrong guess.*/
        private static int MAX_GUESS { get; set; } = 6;

        /* No of letters in a words. 
         * Even though I have fixed all words to be 5 letters.
         * Should be easy to change the code to handle words with difference length */
        private static int MAX_LETTER { get; set; } = 5;

        #endregion


        private bool _timer_flag = false;
        public bool Timer_Flag
        {
            get => _timer_flag;
            set => this.RaiseAndSetIfChanged(ref _timer_flag, value);
        }

        /// <summary>
        /// This variable has 2 purpose :
        /// 1. true ==> a game has just started or
        ///             the user has just won a game and about to start another one.
        /// 2. false ==> a game has just ended or
        ///              start of the game activity screen
        /// </summary>
        private bool _game_on_flag = false;
        public bool Is_Game_On
        {
            get => _game_on_flag;
            set => this.RaiseAndSetIfChanged(ref _game_on_flag, value);
        }




        #region (S)upport Function



        #endregion





        /// <summary>
        /// This is part of the alphabet letter png filenames.
        /// The naming convention for the png file is "letter_?.png" where ? is the alphabet
        /// </summary>
        private static string LetterFile { get; set; } = "letter_";
        /// <summary>
        /// This is part of the hangman image png filenames.
        /// The naming convention for the png file is "hangman_0n.png" where n is a number
        /// </summary>
        private static string HangmanFile { get; set; } = "hangman";
        /// <summary>
        /// This is the name of the question mark png file name
        /// </summary>
        private static string QuestionMarkFile { get; set; } = "question_mark";



        #region Define Slot Image Variables
        /* ==== Declare variables to store hiddent letter image. I have called them Slot Image =================*/
        private string _slot01_Image = QuestionMarkFile;
        public string Slot01_Image
        {
            get => _slot01_Image;
            set => this.RaiseAndSetIfChanged(ref _slot01_Image, LetterFile + value);
        }

        private string _slot02_Image = QuestionMarkFile;
        public string Slot02_Image
        {
            get => _slot02_Image;
            set => this.RaiseAndSetIfChanged(ref _slot02_Image, LetterFile + value);
        }

        private string _slot03_Image = QuestionMarkFile;
        public string Slot03_Image
        {
            get => _slot03_Image;
            set => this.RaiseAndSetIfChanged(ref _slot03_Image, LetterFile + value);
        }

        private string _slot04_Image = QuestionMarkFile;
        public string Slot04_Image
        {
            get => _slot04_Image;
            set => this.RaiseAndSetIfChanged(ref _slot04_Image, LetterFile + value);
        }

        private string _slot05_Image = QuestionMarkFile;
        public string Slot05_Image
        {
            get => _slot05_Image;
            set => this.RaiseAndSetIfChanged(ref _slot05_Image, LetterFile + value);
        }

        /* ==== End of Slot Image Declaration ===================================================================*/
        #endregion

        private int _hangman_count { get; set; } = -1;
        /* ==== Declare variable to store hangman image  ========================================================*/
        private string _hangman_image = "hangman00"; // set the default image to load when activity start
        public string Hangman_Image
        {
            get => _hangman_image;
            set => this.RaiseAndSetIfChanged(ref _hangman_image, HangmanFile + value.PadLeft(2, '0'));
        }

        #region Define Buttons Variables
        /* ==== Declare variable to store letter for each button on the view/screen =============================*/
        /* There are total 15 buttons. Hence have to declare 15 variables to store letter for each button */

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









        /* Correct Answer */
        private int _correct_guess = 0;
        public int Correct_Answer
        {
            get => _correct_guess;
            set => this.RaiseAndSetIfChanged(ref _correct_guess, value);
        }

        /* Current Score */
        private int _score;
        public string Score
        {
            get => _score.ToString();
            set
            {
                if (int.TryParse(value, out int i))
                    this.RaiseAndSetIfChanged(ref _score, i);
            } // add this bit of code so that the Reactive UI data bind will work 
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
            } // add this bit of code so that the Reactive UI data bind will work
        }



        
        //public ReactiveCommand<Unit, Unit> commandPause;

        private IDisposable timerDisposable = null;     // Timer Counter Disposable


        #region variables related to User Profile
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

        private static int MAX_TOP_SCORES { get; set; } = 3;
        #endregion

        #region The following code take care of the timer when Game Activity is inactive or is finishing
        public void Resume()
        {
            if (timerDisposable != null)
            {
                Timer_Flag = true;
                //this.RaisePropertyChanged("timer_flag");
            }
        }

        public void Stop()
        {
            if (timerDisposable != null)
            {
                Timer_Flag = false;
                //this.RaisePropertyChanged("timer_flag");
            }
        }

        public void Dispose()
        {
            if (timerDisposable != null)
                timerDisposable.Dispose();
        }
        #endregion


    }
}