using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HorribleSubsFetcher
{
    /// <summary>
    /// Class for adding methods that utilize regex to the String class
    /// </summary>
    public static class StringRegex
    {
        /// <summary>
        /// Whether matchingField matches anywhere in the string
        /// </summary>
        /// <param name="str">The string being searched</param>
        /// <param name="matchingField">The regex string</param>
        /// <returns>Whether the match was successful</returns>
        public static bool Matches(this string str, string matchingField)
        {
            Regex regex = new Regex(matchingField);
            Match match = regex.Match(str);
            return match.Success;
        }

        /// <summary>
        /// The index of the first match of regex string matchingField
        /// </summary>
        /// <param name="str">The string being searched</param>
        /// <param name="matchingField">The regex string</param>
        /// <returns>The index of the first match or -1 if no match</returns>
        public static int MatchIndex(this string str, string matchingField)
        {
            Regex regex = new Regex(matchingField);
            Match match = regex.Match(str);
            for (int i = 0; i < match.Captures.Count;)
            {
                Capture cap = match.Captures[i];
                return cap.Index;
            }

            return -1;
        }

        /// <summary>
        /// Extracts the first match of matchingField from a string a returns it.
        /// </summary>
        /// <param name="str">The string being searched</param>
        /// <param name="matchingField">The regex string</param>
        /// <returns>The match or null if not match</returns>
        public static string MatchExtract(this string str, string matchingField)
        {
            Regex regex = new Regex(matchingField);
            Match match = regex.Match(str);
            for (int i = 0; i < match.Captures.Count;)
            {
                Capture cap = match.Captures[i];
                return cap.Value;
            }

            return null;
        }


        public static List<int> IndexesOf(this string str, string match)
        {
            int index = 0;
            List<int> result = new List<int>();
            for (int i = 0; ; i++)
            {
                index = str.IndexOf(match, index);

                if (index == -1)
                    break;

                result.Add(index);

                index++;
            }

            return result;
        }
    }
}
