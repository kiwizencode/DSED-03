using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinAppV1.Models
{
    public class GuessWords
    {
        private const string _file_name = "XamarinAppV1.5letters.text";

        public static IList ReadFile()
        {
            var list = new System.Collections.Generic.List<string>();

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
