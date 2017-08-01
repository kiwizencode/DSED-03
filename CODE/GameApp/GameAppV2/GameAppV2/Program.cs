using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppV2
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines;
            var list = new List<string>();
            var file_stream = new FileStream(@"5letters.txt", FileMode.Open, FileAccess.Read);
            using (var stream_reader = new StreamReader(file_stream, Encoding.UTF8))
            {
                string line;
                while ((line = stream_reader.ReadLine()) != null)
                {
                    list.Add(line);
                    Console.WriteLine($"{line}");
                }
                    
            }
            lines = list.ToArray();

            Console.WriteLine($"count : {lines.Length}");
            Console.ReadKey();
        }
    }
}
