using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyConsoleApp.Helpers
{
    public static class StringHelper
    {
        public static string FormatItemName(string itemName) 
        {
            string[] lowercaseWords = { "of", "the", "and", "a", "an" };

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            string[] words = itemName
                .Trim()
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (i == 0 || !lowercaseWords.Contains(words[i]))
                {
                    words[i] = textInfo.ToTitleCase(words[i]);
                }
            }

            return string.Join(" ", words);
        }
    }
    
}
