using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPrototype
{
    public static class Util
    {

        /// <summary>
        /// Acceptable number of string operations to get a search term
        /// </summary>
        public static int SearchTermTarget = 3;

        public static bool UseSearchTerm(string searchTerm, string comparable, bool emptyIsDefault = true, bool caseSensitive = false, int target = -1)
        {
            if (target == -1)
                target = SearchTermTarget;

            if (emptyIsDefault && searchTerm == "")
                return true;

            if (!caseSensitive)
            {
                searchTerm = searchTerm.ToLower();
                comparable = comparable.ToLower();
            }

            if (comparable.Contains(searchTerm))
                return true;

            return ComputeStringDistance(searchTerm, comparable) < target;
        }

        /// <summary>
        /// Levenshtein distance algorithem
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int ComputeStringDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
