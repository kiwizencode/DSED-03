using System;
using System.IO;
using System.Collections.Generic;

using Guess5.Lib.Model;

namespace Guess5.Lib.DataAccessObject
{
    /*
     * This class using Repository Pattern as describe in sample project from the following uri:
     * https://developer.xamarin.com/samples/mobile/Tasky/
     */
    public class ScoreRepository
    { 
        SqliteDatabase _db = null;
        protected static string _location;
        protected static ScoreRepository _self;

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "myDB.db3";

    #if NETFX_CORE
				    var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
    #else

    #if SILVERLIGHT
					    // Windows Phone expects a local path, not absolute
					    var path = sqliteFilename;
    #else

    #if __ANDROID__
						    // Just use whatever directory SpecialFolder.Personal returns
						    string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
    #else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
    #endif
                var path = Path.Combine(libraryPath, sqliteFilename);

    #endif

    #endif
                return path;
            }
        }

        static ScoreRepository()
        {
            _self = new ScoreRepository();
        }
        protected ScoreRepository()
        {
            // set the db location
            _location = DatabaseFilePath;

            // instantiate the database	
            _db = new SqliteDatabase(_location);
        }
        public static ScoreModel GetProfile(int id)
        {
            return _self._db.GetItem<ScoreModel>(id);
        }

        public static IEnumerable<ScoreModel> GetProfiles()
        {
            return _self._db.GetItems<ScoreModel>();
        }

        public static int SaveProfile(ScoreModel item)
        {
            return _self._db.SaveItem<ScoreModel>(item);
        }

        public static int DeleteProfile(int id)
        {
            return _self._db.DeleteItem<ScoreModel>(id);
        }
    }
}
