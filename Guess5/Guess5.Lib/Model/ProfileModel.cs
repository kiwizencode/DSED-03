﻿using SQLite;
using System;

namespace Guess5.Lib.Model
{
    /// <summary>
    /// This data model/class store profile related information.
    ///   also use by the SQLite to retreive/store profile information to the database.
    /// </summary>
    public class ProfileModel : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } = 0;
        public string Name { get; set; }
        public int Scores { get; set; } = 0;
        public DateTime Timestamp { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            //return Name;
            string score = Scores.ToString();
            string text = Name + "".PadRight(10 - Name.Length, ' ') +
                            "".PadLeft(10 - score.Length, '0') + score + " " ;
            return text;
        }
    }
}
