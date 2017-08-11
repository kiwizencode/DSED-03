using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamarinAppV1.Models
{
    public class GuessWords
    {
        private const string _file_name = "5letters.txt";

        public static string [] ReadFile()
        {
            var list = new List<string>();

            var assembly = typeof(GuessWords).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("XamarinAppV1."+_file_name);

            using (StreamReader stream_reader = new StreamReader(stream))
            {
                string line;
                while ((line = stream_reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            return list.ToArray();
        }
    }
}
