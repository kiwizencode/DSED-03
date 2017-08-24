using ReactiveUI;
//using ReactiveUI.Legacy;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;

using Xamarin.Forms;

using HangmanApp.Shared.Data;
using HangmanApp.Shared.DataAccessObject;

namespace HangmanApp.Droid.ViewModel
{
    public class ViewModel_MainScreen : ReactiveObject
    {
        private string _activity_text ;
        public string ActivityName
        {
            get => _activity_text;
            set => this.RaiseAndSetIfChanged(ref _activity_text, value);
        }

        public string txtStartGame;
        public ReactiveCommand cmdStartGame;

        public string txtScores;
        public ReactiveCommand cmdScores;

        public string txtProfile;
        public ReactiveCommand cmdProfile;

        public string txtCredits;
        public ReactiveCommand cmdCredits;

        public ReactiveCommand cmdActivity { get; }        

        private string _msg = string.Empty;
        public string Toast {
            get => _msg;
            set { this.RaiseAndSetIfChanged(ref _msg, value); }
        }

        /*
         * The Model_Profile object store user profile information
         */
        Model_Profile _profile;
        private Model_Profile Profile  {
            get => _profile;
            set { this.RaiseAndSetIfChanged(ref _profile, value); }
        }

        public string Profile_ID
        {
            get => _profile.ID.ToString();
            set
            {
                /*
                 * Whether profile is a new and old profile by checking the ID
                 */
                if(value=="0")
                {
                    _profile = new Model_Profile();
                }
                else
                {
                    int id;
                    if(int.TryParse(value,out id))
                    {
                        Model_Profile temp = ProfileRepository.GetProfile(id);
                        if( temp != null)
                            this.RaiseAndSetIfChanged(ref _profile, temp);
                    }
                    
                }
            }
        }

        public string Profile_Name { get => _profile.Name; }

        public ViewModel_MainScreen()
        {
            cmdActivity = ReactiveCommand.Create<string>((x) => Toast = x );

            cmdStartGame = ReactiveCommand.Create(() => Toast = txtStartGame);
            cmdScores = ReactiveCommand.Create(() => Toast = txtScores);
            cmdProfile = ReactiveCommand.Create(() => Toast = txtProfile);
            cmdCredits = ReactiveCommand.Create(() => Toast = txtCredits);
        }

    }
}