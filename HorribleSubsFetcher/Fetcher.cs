using System;
using System.Collections.Generic;
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
                        int distance = FuzzyMatching.DamerauLevenshtein(name, title);

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

        public async Task<List<string>> GetMagnetLinks(int showId, string[] resolutionPriority = null, List<int> episodeList = null)
        {
            if (resolutionPriority == null)
                resolutionPriority = new string[] { "1080p", "720p", "480p" };

            var nodes = await GetAllEpisodes(showId);
            List<string> result = new List<string>();
            string magnet = "";

            if (episodeList == null) // Get all episodes
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    magnet = GetSingleEpisode(nodes[i], resolutionPriority);
                    result.Add(magnet);
                }
            }
            else
            {
                for (int i = 0; i < episodeList.Count; i++)
                {
                    if (episodeList[i] >= 1 && episodeList[i] <= nodes.Length)
                    {
                        magnet = GetSingleEpisode(nodes[episodeList[i] - 1], resolutionPriority);
                        result.Add(magnet);
                    }
                }
            }

            return result;
        }

        public string GetSingleEpisode(HtmlNode node, string[] resolutionPriority)
        {
            int j;
            string magnet = null;
            for (j = 0; j < resolutionPriority.Length; j++)
            {
                var childNode = node.QuerySelector($".link-{resolutionPriority[j]} > .hs-magnet-link > a");

                if (childNode == null)
                    continue;

                magnet = childNode.GetAttributeValue("href", null);

                if (magnet != null)
                    break;
            }

            if (j == resolutionPriority.Length)
                Console.WriteLine("Couldn't get any link with the provided resolution priority list");

            return magnet;
        }

        public async Task<HtmlNode[]> GetAllEpisodes(int showId)
        {
            var response = await httpClient.GetAsync($"https://horriblesubs.info/api.php?method=getshows&type=show&showid={showId}");
            string content = await response.Content.ReadAsStringAsync();

            var html = new HtmlDocument();
            html.LoadHtml(content);

            HtmlNode[] result = html.DocumentNode.QuerySelectorAll(".rls-info-container").ToArray();

            return result.OrderBy(x => x.Id).ToArray();
        }

    }
}
