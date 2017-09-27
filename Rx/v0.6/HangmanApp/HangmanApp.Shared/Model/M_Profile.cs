using SQLite;
using System;

namespace HangmanApp.Shared.Model
{
    /// <summary>
    /// This data model/class store profile related information.
    ///   also use by the SQLite to retreive/store profile information to the database.
    /// </summary>
    public class Model_Profile : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } = 0;
        public string Name { get; set; }
        public int Scores { get; set; } = 0;
        public DateTime Timestamp { get; set; }

        public override string ToString() { return Name; }
        //public override string ToString()
        //{
        //    //return Name + " " + Timestamp.ToUniversalTime();
        //    string score_string = Scores.ToString();
        //    int len = 10 - Scores.ToString().Length;

        //    string name = Name.Trim();
        //    //return name.PadRight(10 - name.Length, ' ') + "\t" 
        //    //    + score_string.PadRight(10 - score_string.Length, '0')
        //    //    + "\t" + Timestamp.ToShortDateString() ;

        //    return  "".PadLeft(len, '0') + score_string + "\t" + name ;
        //}
    }
}
