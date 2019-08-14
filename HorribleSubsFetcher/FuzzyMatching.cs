using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    static class FuzzyMatching
    {
        public static int Levenshtein(string a, string b, bool caseSensitive = false)
        {
            // Declare auxiliary matrix
            int[][] aux = new int[a.Length + 1][];
            for (int i = 0; i < aux.Length; i++)
                aux[i] = new int[b.Length + 1];

            // Initialize all values to 0
            for (int i = 0; i < aux.Length; i++)
            {
                for (int j = 0; j < aux[i].Length; j++)
                    aux[i][j] = 0;
            }

            for (int i = 1; i <= a.Length; i++)
                aux[i][0] = i;

            for (int j = 1; j <= b.Length; j++)
                aux[0][j] = j;

            for (int j = 1; j <= b.Length; j++)
            {
                for (int i = 1; i <= a.Length; i++)
                {
                    int cost;
                    if (caseSensitive)
                        cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    else
                        cost = (Char.ToLower(a[i - 1]) == Char.ToLower(b[j - 1])) ? 0 : 1;

                    int min = aux[i - 1][j] + 1; // Deletion
                    min = Math.Min(min, aux[i][j - 1] + 1); // Insertion
                    min = Math.Min(min, aux[i - 1][j - 1] + cost); // Modification

                    aux[i][j] = min;
                }
            }

            return aux[a.Length][b.Length];
        }

        // Not working properly. Do not use
        public static int DamerauLevenshtein(string a, string b, bool caseSensitive = false)
        {
            // Declare auxiliary matrix
            int[][] aux = new int[a.Length + 1][];
            for (int i = 0; i < aux.Length; i++)
                aux[i] = new int[b.Length + 1];

            // Initialize all values to 0
            for (int i = 0; i < aux.Length; i++)
            {
                for (int j = 0; j < aux[i].Length; j++)
                    aux[i][j] = 0;
            }

            for (int i = 1; i <= a.Length; i++)
                aux[i][0] = i;

            for (int j = 1; j <= b.Length; j++)
                aux[0][j] = j;

            for (int j = 1; j <= b.Length; j++)
            {
                for (int i = 1; i <= a.Length; i++)
                {
                    int cost;
                    if (caseSensitive)
                        cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    else
                        cost = (Char.ToLower(a[i - 1]) == Char.ToLower(b[j - 1])) ? 0 : 1;

                    int min = aux[i - 1][j] + 1; // Deletion
                    min = Math.Min(min, aux[i][j - 1] + 1); // Insertion
                    min = Math.Min(min, aux[i - 1][j - 1] + cost); // Modification

                    if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1]) // Transposition
                        min = Math.Min(min, aux[i - 2][j - 2] + cost);

                    aux[i][j] = min;
                }
            }

            return aux[a.Length][b.Length];
        }

        public static double DamerauLevenshteinPercentage(string a, string b)
        {
            return 1.0 - ((double)DamerauLevenshtein(a, b) / Math.Max(a.Length, b.Length));
        }
    }
}
