
using System;
using System.IO;

using HangmanApp.Shared.Data;
using System.Collections.Generic;

namespace HangmanApp.Shared.DataAccessObject
{
	public class ProfileRepository
	{
		ProfileDatabase _db = null;
		protected static string _location;
		protected static ProfileRepository _self;

		public static string DatabaseFilePath
		{
			get
			{
				var sqliteFilename = "ProfileDB.db3";

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
						string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
						string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
					 #endif
					var path = Path.Combine(libraryPath, sqliteFilename);

			#endif

				#endif
				return path;
			}
		}

		static ProfileRepository()
		{
			_self = new ProfileRepository();
		}
		protected ProfileRepository()
		{
			// set the db location
			_location = DatabaseFilePath;

			// instantiate the database	
			_db = new ProfileDatabase(_location);
		}
		public static Model_Profile GetProfile(int id)
		{
			return _self._db.GetItem<Model_Profile>(id);
		}

		public static IEnumerable<Model_Profile> GetProfiles()
		{
			return _self._db.GetItems<Model_Profile>();
		}

		public static int SaveProfile(Model_Profile item)
		{
			return _self._db.SaveItem<Model_Profile>(item);
		}

		public static int DeleteProfile(int id)
		{
			return _self._db.DeleteItem<Model_Profile>(id);
		}
	}
}
