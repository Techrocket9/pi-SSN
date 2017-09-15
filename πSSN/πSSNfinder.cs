using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace πSSN
{
    class πSSNfinder
    {

        static readonly int REPORT_INTERVAL = 100_000;

        static void Main(string[] args)
        {
            var srcFilePath = @"C:\Users\Techr\Downloads\y-cruncher v0.7.3.9474\Binaries\Pi - Dec - Chudnovsky.txt"; // Get some digits of π from a file. This version of the program was written for and tested against 2.5 billion digits generated with y-cruncher
            using (var fs = File.Open(srcFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var stream = new StreamReader(fs))
            {
                int foundSSNs = 0;
                uint digitOffset = 1;
                var ssnOffsets = new uint[1_000_000_000];
                var ringBuffer = new StringBuilder(9);

                //Discard 3 (the . will be dropped in the first pass through the main loop)
                stream.Read();

                // Load ringbuffer with starter data
                for (int i = 0; i < 9; ++i)
                {
                    ringBuffer.Append((char)stream.Read());
                }

                DateTime lastFound = DateTime.MinValue;
                // Actually do the work
                for (; ; )
                {
                    var nextVal = stream.Read();
                    if (-1 == nextVal)
                    {
                        Console.WriteLine($"Exhaused available digits of π. Found {foundSSNs} social security numbers.");
                        break;
                    }
                    ++digitOffset;
                    ringBuffer.Remove(0, 1); // Pop off last character
                    ringBuffer.Append((char)nextVal); // Push on next character
                    var ssn = int.Parse(ringBuffer.ToString());

                    if (0 == ssnOffsets[ssn])
                    {

                        ssnOffsets[ssn] = digitOffset;
                        ++foundSSNs;
                        if (0 == foundSSNs % REPORT_INTERVAL)
                        {
                            var oldTime = lastFound;
                            lastFound = DateTime.Now;
                            var findtime = lastFound - oldTime;

                            Console.WriteLine($"Found {FormatOrdinal(foundSSNs)} social security number in π at offset {digitOffset.ToString("N0")}. Finding the last {REPORT_INTERVAL.ToString("N0")} SSNs took {findtime}");
                        }
                    }



                    if (foundSSNs >= 1_000_000_000)
                    {
                        Console.WriteLine($"Found all SSNs in π");
                        break;
                    }
                }
                //TODO Output the indexes of the SSNs
            }
        }

        // Taken from https://stackoverflow.com/a/20175/
        public static string FormatOrdinal(int num)
        {
            if (num <= 0) return num.ToString("N0").ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString("N0") + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString("N0") + "st";
                case 2:
                    return num.ToString("N0") + "nd";
                case 3:
                    return num.ToString("N0") + "rd";
                default:
                    return num.ToString("N0") + "th";
            }

        }
    }
}
