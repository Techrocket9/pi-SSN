using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace πSSN
{
    class Program
    {
        static void Main(string[] args)
        {
            var srcFilePath = @"C:\Users\Techr\Downloads\y-cruncher v0.7.3.9474\Binaries\Pi - Dec - Chudnovsky.txt";
            using (var fs = File.Open(srcFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var stream = new StreamReader(fs))
            {
                char firstChar = (char) stream.Read();
                char secondChar = (char)stream.Read();
                char thirdChar = (char)stream.Read();

                Console.WriteLine($"{firstChar}, {secondChar}, {thirdChar}");
            }
        }
    }
}
