using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Url_Detector
{
    internal class Program
    {
        IEnumerable<string> KnownTLDs = Properties.Resources.KnownTLDs
            .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => $".{x.Split('(').First().Trim()}");

        IEnumerable<string> KnownProtocols = Properties.Resources.KnownProtocols
            .Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim());

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Write("Enter a sentence: ");
                var input = Console.ReadLine();

                var replaced = RemoveUrlViaProtocol(input);
                replaced = RemoveUrlViaTLD(input);
                Console.WriteLine($"Result: {input}");
                if (replaced == input)
                {
                    Console.WriteLine("No URLs detected.");
                }

            }
        }
        private string RemoveUrlViaTLD(string input)
        {
            foreach (var pr in KnownTLDs)
            {
                int minIndex = input.IndexOf(pr, 0, StringComparison.OrdinalIgnoreCase);

                while (minIndex > 0)
                {
                    minIndex += pr.Length;

                    int start = minIndex,
                        charasRemoved = 0;

                    for (; minIndex > 0 && input[minIndex] != ' '; minIndex--)
                        charasRemoved++;

                    input = input.Remove(minIndex, charasRemoved + 1);

                    minIndex = input.IndexOf(pr, minIndex, StringComparison.OrdinalIgnoreCase);
                }
            }
            return input;
        }
        private string RemoveUrlViaProtocol(string input)
        {
            foreach (var pr in KnownProtocols)
            {
                int minIndex = input.IndexOf(pr, 0, StringComparison.OrdinalIgnoreCase);

                while (minIndex != -1)
                {
                    int start = minIndex, charasRemoved = 0;

                    for (; minIndex < input.Length && input[minIndex] != ' '; minIndex++)
                        charasRemoved++;

                    input = input.Remove(start, charasRemoved);

                    minIndex -= charasRemoved;
                    minIndex = input.IndexOf(pr, minIndex, StringComparison.OrdinalIgnoreCase);
                }
            }
            return input;
        }
        static void Main(string[] args) =>
            new Program().StartAsync().GetAwaiter().GetResult();
    }
}
