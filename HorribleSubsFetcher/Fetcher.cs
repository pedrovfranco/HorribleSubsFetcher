using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace HorribleSubsFetcher
{
    class Fetcher
    {
        private HttpClient httpClient;

        private static readonly Uri searchLink = new Uri("https://horriblesubs.info/shows/");

        public Fetcher()
        {
            httpClient = new HttpClient();
        }

        public async Task Run()
        {
            var showId = await GetShowIdByName("vinland saga");

            var links = await GetMagnetLinks(showId);

            // Runs all magnet links found
            for (int i = 0; i < links.Length; i++)
            {
                System.Diagnostics.Process.Start(links[i]);
            }
        }


        public int GetShowIdByHtml(string html)
        {
            bool success = Int32.TryParse(html.MatchExtract(@"(?<=script.*var hs_showid = )[0-9]+(?=.*script)"), out int result);

            return success ? result : -1;
        }

        public async Task<int> GetShowIdByLink(string link)
        {
            try
            {
                var response = await httpClient.GetAsync(link);
                string content = await response.Content.ReadAsStringAsync();

                return GetShowIdByHtml(content);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> GetShowIdByName(string name)
        {
            try
            {
                var response = await httpClient.GetAsync(searchLink);
                string content = await response.Content.ReadAsStringAsync();

                var html = new HtmlDocument();
                html.LoadHtml(content);

                HtmlNode[] result = html.DocumentNode.QuerySelectorAll("div .ind-show").ToArray();

                // Searches for the title with the closest match to the input show name
                int leastDistance = Int32.MaxValue;
                int leastDistanceIndex = -1;
                for (int i = 0; i < result.Length; i++)
                {
                    string title = result[i].FirstChild.GetAttributeValue("title", null);
                    if (title != null)
                    {
                        int distance = DamerauLevenshtein(name, title);

                        if (distance < leastDistance)
                        {
                            leastDistance = distance;
                            leastDistanceIndex = i;
                        }

                        if (distance == 0)
                            break;
                    }
                }

                string refLink = result[leastDistanceIndex].FirstChild.GetAttributeValue("href", null);

                if (refLink == null)
                    return -1;

                Uri link = new Uri(searchLink, refLink);

                return await GetShowIdByLink(link.ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<HtmlNode[]> GetEpisodes(int showId)
        {
            var response = await httpClient.GetAsync($"https://horriblesubs.info/api.php?method=getshows&type=show&showid={showId}");
            string content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);

            HtmlNode[] result = html.DocumentNode.QuerySelectorAll(".rls-info-container").ToArray();

            return result.OrderBy(x => x.Id).ToArray();
        }

        public async Task<string[]> GetMagnetLinks(int showId, string[] resolutionPriority = null)
        {
            if (resolutionPriority == null)
                resolutionPriority = new string[] { "1080p", "720p", "480p" };

            var nodes = await GetEpisodes(showId);
            string[] result = new string[nodes.Length];
            string magnet = "";

            for (int i = 0; i < nodes.Length; i++)
            {
                int j;
                for (j = 0; j < resolutionPriority.Length; j++)
                {
                    var node = nodes[i];

                    var childNode = node.QuerySelector($".link-{resolutionPriority[j]} > .hs-magnet-link > a");

                    if (childNode == null)
                        continue;

                    magnet = childNode.GetAttributeValue("href", "");

                    if (magnet != null)
                        break;
                }

                if (j == resolutionPriority.Length)
                {
                    Console.WriteLine("Couldn't get any link with the provided resolution priority list");
                    return null;
                }

                result[i] = magnet;
            }

            return result;
        }

        
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
    }
}
