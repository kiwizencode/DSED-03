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
        [PrimaryKey]
        public int ID { get ; set ; }
        public string Name { get; set; }
        public int Score { get; set; } = 0;

        public override string ToString() {
            string score = Score.ToString();
            string text = (Name.Trim() == string.Empty) ? "" :
                            score.PadLeft(10-score.Length,'0') + 
                            Name.PadRight(10 - Name.Length, ' ') ;
            return text;
        }
    }
}
