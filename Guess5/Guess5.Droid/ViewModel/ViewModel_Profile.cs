using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;  // used for debugging purpose
using System.Reactive;
using System.Reactive.Linq;

using ReactiveUI;

using Guess5.Lib.Model;
using Guess5.Lib.DataAccessObject;
using Android.Widget;

namespace Guess5.Droid.ViewModel
{
    public class ViewModel_Profile : ReactiveObject
    {
        /// <summary>
        /// point to the current Activity which is bind to this View Model
        /// for updating android GUI purpose
        /// </summary>
        public ReactiveActivity CurrentActivity { get; set; }

        //private ProfileModel _profile;
        //public ProfileModel Profile {
        //    get => _profile;
        //    set  {
        //        this.RaiseAndSetIfChanged(ref _profile, value);
        //    }
        //}

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

        // private ArrayAdapter<ProfileModel> _profile_Array;
        public ArrayAdapter<ProfileModel> Profile_Array {
            get {
                ProfileModel[] items = ProfileRepository.GetProfiles().OrderBy(x => x.Name.ToLower()).ToArray();
                return new ArrayAdapter<ProfileModel>(CurrentActivity, Android.Resource.Layout.SimpleListItem1, items);
            }
            set { }
        }

        public ArrayAdapter<ScoreModel> Score_Array
        {
            get
            {
                ScoreModel[] items = ScoreRepository.GetProfiles().OrderBy(x => x.Name.ToLower()).ToArray();
                return new ArrayAdapter<ScoreModel>(CurrentActivity, Android.Resource.Layout.SimpleListItem1, items);
            }
            set { }
        }

        public ReactiveCommand<Unit, Unit> CommandSave;
        public ReactiveCommand<Unit, Unit> CommandItemClicked;


        public ViewModel_Profile()
        {

            /* 
                http://dotnetpattern.com/csharp-action-delegate 
             */

            Action doCreateAction = new Action(CreateProfile);
            CommandSave = ReactiveCommand.Create(doCreateAction);


            Action doItemClickedAction = new Action(ItemClicked);
            CommandItemClicked = ReactiveCommand.Create(doItemClickedAction);


            SetupObservable(); 
        }

        private void ItemClicked()
        {
            //Debug
            Debug.WriteLine("Detected ItemClicked Event !!!");
        }

        private void CreateProfile()
        {
            if(ProfileName.Trim() != string.Empty)
            {
                /* Create a Game Profile */
                ProfileModel profile = new ProfileModel();
                profile.Name = ProfileName;

                DateTime theTime = DateTime.Now.ToLocalTime();
                profile.Timestamp = theTime;

                profile.ID = ProfileRepository.SaveProfile(profile);

                this.RaisePropertyChanged("Profile_Array");
            }
        }

        private void SetupObservable()
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
                        profile.Active = true;
                        ProfileRepository.SaveProfile(profile);

                        /* and the rest as 'inactive' */
                        foreach(var p in ProfileRepository.GetProfiles())
                        {
                            if( p.ID != id && p.Active)
                            {
                                p.Active = false;
                                ProfileRepository.SaveProfile(p);
                            }
                        }
                    }
                    /* 
                        flag return false under following conditions:
                        1. When the main activity first loaded, no active profile been selected
                           solution => load active profile => the last profile that user used.
                        2. When there is no profile (in the database).
                           solution => create a 'Guest' profile and set the profile to be 'active'
                     */
                    else
                    {
                        //List<ProfileModel> profiles = ProfileRepository.GetProfiles();
                        bool active_profile = false;
                        foreach (var profile in ProfileRepository.GetProfiles())
                        {
                            if (profile.Active)
                            {
                                active_profile = true;
                                ProfileID = profile.ID.ToString();
                                ProfileName = profile.Name;
                            }
                        }
                        if (!active_profile) // found active profile
                        {
                            /* Create a 'Guest 'Game Profile and set it to 'active' */
                            ProfileModel profile = new ProfileModel();
                            profile.Name = "Guest";

                            DateTime theTime = DateTime.Now.ToLocalTime();
                            profile.Timestamp = theTime;

                            profile.Active = true;
                            profile.ID = ProfileRepository.SaveProfile(profile);

                            ProfileID = profile.ID.ToString();
                            ProfileName = profile.Name;
                        }
                    }
                });
        }


    }
}