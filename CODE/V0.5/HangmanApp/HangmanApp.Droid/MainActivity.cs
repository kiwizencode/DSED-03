using Android.App;
using Android.Widget;
using Android.OS;

using System;
using System.Linq;

using HangmanApp.Shared.Data;
using HangmanApp.Shared.DataAccessObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace HangmanApp.Droid
{
    [Activity(Label = "Hangman App", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView listViewProfile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.profile_layout_test);

            SetupData();
        }

        private void SetupData()
        {
            List<Profile> items = ProfileRepository.GetTasks().ToList();

            listViewProfile = FindViewById<ListView>(Resource.Id.listViewProfile);

            /*
            Profile user = new Profile { ID = 1, Name = "Guest", Timestamp = System.DateTime.Now };

            items.Add(user);
            */
            /* http://www.ezzylearning.com/tutorial/binding-android-listview-with-custom-objects-using-arrayadapter */
            var adapter = new ArrayAdapter<Profile>(this,
                                          Android.Resource.Layout.SimpleListItem1,
                                          items);

            listViewProfile.Adapter = adapter;      
        }
    }
}

