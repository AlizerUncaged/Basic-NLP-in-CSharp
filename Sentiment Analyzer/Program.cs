using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SentimentAnalyzer
{
    internal class Program
    {
        private List<string> GoodWords = new List<string>();
        private List<string> BadWords = new List<string>();

        public async Task StartAsync()
        {
            /// Load bad words.
            BadWords.AddRange(
                    Properties.Resources.BadWords
                    .Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim().ToLower())
                );

            /// Load good words.
            GoodWords.AddRange(
                 Properties.Resources.GoodWords
                 .Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(x => x.Trim().ToLower())
             );

            /// Clean.
            BadWords = BadWords.Distinct().ToList();
            GoodWords = GoodWords.Distinct().ToList();

            while (true)
            {
                Console.Write("Enter a sentence: ");
                var input = Console.ReadLine().ToLower();

                var result = GoodWords.Sum(x => input.CountSubstring(x)) - BadWords.Sum(x => input.CountSubstring(x));

                Console.WriteLine($"Result {result}: {(result > 0 ? "Positive!".Pastel(System.Drawing.Color.Green) : result < 0 ? "Negative...".Pastel(System.Drawing.Color.Red) : "Neutral".Pastel(System.Drawing.Color.DarkGray))}");
            }
        }

        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();
    }
}
