using SQLite;
using System;

namespace HangmanApp.Shared.Data
{
    /// <summary>
    /// This data entity class store profile related information.
    ///   also use by the SQLite to retreive/store profile information to the database.
    /// </summary>
    public class Profile : IDataEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get ; set ; }
        public string Name { get; set; }
        public int Scores { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return Name + " " + Timestamp.ToUniversalTime();
        }
    }
}
