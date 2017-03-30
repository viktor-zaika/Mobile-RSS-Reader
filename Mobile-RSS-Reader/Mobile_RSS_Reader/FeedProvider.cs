using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mobile_RSS_Reader.Parsers;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader
{
    /// <summary>
    /// Provides feeds and feed detail for application in online and offline way.
    /// </summary>
    public class FeedProvider
    {
        private IFeedParser _parser;
        private DataStorage _storage;
        private HttpClient _client;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="feedParser">Feed parser</param>
        /// <param name="storage"> Storage instance</param>
        public FeedProvider(IFeedParser feedParser, DataStorage storage)
        {
            _parser = feedParser;
            _storage = storage;
            _client = new HttpClient();
        }

        /// <summary>
        /// Provides all available feeds async.
        /// </summary>
        /// <param name="token">Cancelation token</param>
        /// <returns>Feed's or empty list in case of fail.</returns>
        public async Task<IEnumerable<Feed>> GetFeedsAsync(CancellationToken token)
        {
            IEnumerable<Feed> feeds = new List<Feed>();

            try
            {   
                var rowFeeds = await _client.GetStringAsync(new Uri("https://news.microsoft.com/feed/"));
                feeds = _parser.Parse(rowFeeds);
                await _storage.SaveFeedsAsync(new ReadOnlyCollection<Feed>(feeds.ToList()), CancellationToken.None);
                var allFeeds = await _storage.GetAllFeeds().FirstOrDefaultAsync();

                var oldNews = allFeeds.Where(l2 => feeds.All(l1 => l1.Id != l2.Id));
                oldNews.Select(async item =>
                {
                    // Remove old feeds.
                    await _storage.DeleteFeedAsync(item.Id, token);
                    await _storage.DeleteFeedArticleAsync(item.Id, token);
                }).ToList();
            }
            catch
            {
                // nothing to do here.
            }

            return feeds;
        }

        /// <summary>
        /// Provides full feed in offline way (From storage).
        /// </summary>
        /// <param name="link">Article link</param>
        /// <returns>Full feed article or null if it not found.</returns>
        public async Task<FeedArticle> GetFullFeedOfflineAsync(Uri link)
        {
            return await _storage.GetFeedArticle(link.ToString()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Provides full feed article from network
        /// </summary>
        /// <param name="link">Article link</param>
        /// <param name="token">Cancelation token</param>
        /// <returns>Full feed article or null in case of fail or cancelation.</returns>
        public async Task<FeedArticle> GetFullFeedOnlineAsync(Uri link, CancellationToken token)
        {
            FeedArticle result = null;
            try
            {
                // Article missing in storage and should be downloaded
                var rowArticle = await _client.GetStringAsync(link);
                var resultArticle = new HtmlArticleParser().GetArticleFromPage(rowArticle);
                result = new FeedArticle(link.ToString(), resultArticle);
                await _storage.SaveFeedArticleAsync(result, token);
            }
            catch
            {
                // nothing to do here.
            }

            return result;
        }
    }
}