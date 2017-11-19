using System;
using SQLite;

namespace Guess5.Lib.Model
{
    /// <summary>
    /// This data model/class store highest score information.
    ///   also use by the SQLite to retreive/store profile information to the database.
    /// </summary
    public class ScoreModel : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; } = 0;

        public override string ToString() {
            string score = Score.ToString();
            string text = (Name.Trim() == string.Empty) ? "" :
                            "".PadLeft(10-score.Length,'0') + score + " " +
                            Name.PadRight(10 - Name.Length, ' ') ;
            return text;
        }
    }
}
