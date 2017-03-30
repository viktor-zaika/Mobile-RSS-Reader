using System.Collections.Generic;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Parsers
{   
    /// <summary>
    /// Discribes requirements to feed parser
    /// </summary>
    public interface IFeedParser
    {
        /// <summary>
        /// Parse feed.
        /// </summary>
        /// <param name="rowFeeds">Row feed</param>
        /// <returns></returns>
        IEnumerable<Feed> Parse(string rowFeeds);
    
    }
}