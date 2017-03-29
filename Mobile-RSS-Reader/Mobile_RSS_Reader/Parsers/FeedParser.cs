using System.Collections.Generic;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Parsers
{
    public interface FeedParser
    {
        IEnumerable<Feed> Parse(string rowFeeds);
    }
}