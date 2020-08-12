using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillLibrary
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetterOfEachWord(this string sentence)
        {
            var capitalizedWordList = new StringBuilder("");

            var individualWords = sentence.Split(' ');
            var wordList = individualWords.Where(i => !string.IsNullOrWhiteSpace(i));
            
            foreach (var word in wordList)
            {
                if (word.Length == 0)
                    continue;
                else if (word.Length == 1)
                    capitalizedWordList = capitalizedWordList.Append($"{word.ToUpper()} ");
                else
                    capitalizedWordList = capitalizedWordList.Append($"{char.ToUpper(word[0])}{word.Substring(1)} ");
            }

            return capitalizedWordList.ToString().TrimEnd();
        }
    }
}
