using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SentimentAnalyzer
{
    public static class Utilities
    {
        public static int CountSubstring(this string text, string value)
        {
            int count = 0, 
                minIndex = text.IndexOf(value, 0);

            while (minIndex != -1)
            {
                minIndex = text.IndexOf(value, minIndex + value.Length);
                count++;
            }
            return count;
        }
    }
}
