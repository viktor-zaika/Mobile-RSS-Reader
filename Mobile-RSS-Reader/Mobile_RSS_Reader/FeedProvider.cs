using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Mobile_RSS_Reader.Parsers;
using System.Net.Http;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader
{
    public class FeedProvider
    {
        private FeedParser _parser;

        public FeedProvider(FeedParser feedParser)
        {
            _parser = feedParser;
        }

        public async Task<IEnumerable<Feed>> GetFeedsAsync()
        {
            var client = new HttpClient();

            var rowFeeds = await client.GetStringAsync(new Uri("https://news.microsoft.com/feed/"));

            return _parser.Parse(rowFeeds);

        }

        public async Task<string> feedDetails

    }
}