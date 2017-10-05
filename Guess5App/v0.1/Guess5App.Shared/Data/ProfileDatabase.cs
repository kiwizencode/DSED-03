using System.Linq;
using System.Collections.Generic;
using SQLite;

using Guess5App.Shared.Model;


namespace Guess5App.Shared.DataAccessObject
{
    public class ProfileDatabase : SQLiteConnection
    {
        static object locker = new object();
        /// <summary> Initializes a new instance of the <see cref="HangmanApp.Shared.DataAccessObject.ProfileDB"/> TaskDatabase.
        /// if the database doesn't exist, it will create the database and the all tables. </summary>
        /// <param name="path">Path.</param>
        public ProfileDatabase(string path) : base(path)
        {
            // create the tables
            CreateTable<Model_Profile>();
        }

        /// <summary>return list of profile</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetItems<T>() where T : IModel, new()
        {
            lock (locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }

        /// <summary> retreive profile by providing the id.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">profile ID</param>
        /// <returns></returns>
        public T GetItem<T>(int id) where T : IModel, new()
        {
            lock (locker)
            {
                return Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        /// <summary> save profile data. If ID is 0, create a new record, else update record by ID</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int SaveItem<T>(T item) where T : IModel
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    Update(item);
                    return item.ID;
                }
                else
                {
                    return Insert(item);
                }
            }
        }

        /// <summary>delete a record by providing the profile ID</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteItem<T>(int id) where T : IModel, new()
        {
            lock (locker)
            {
                return Delete(new T() { ID = id });
            }
        }
    }
}
