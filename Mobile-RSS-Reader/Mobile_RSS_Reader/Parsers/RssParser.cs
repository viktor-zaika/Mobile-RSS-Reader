using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Mobile_RSS_Reader.Converters;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Parsers
{   
    /// <summary>
    /// Represent feed parser which provided in rss format.
    /// </summary>
    public class RssParser : FeedParser
    {   
        /// <summary>
        /// Html to plain text converter
        /// </summary>
        private readonly IHtmlToPlainTextConverter _htmlToPlainTextConverter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="htmlToPlainTextConverter">Html to plain text converter</param>
        public RssParser(IHtmlToPlainTextConverter htmlToPlainTextConverter)
        {
            _htmlToPlainTextConverter = htmlToPlainTextConverter;
        }

        /// <inheritdoc />
        public IEnumerable<Feed> Parse(string rowFeeds)
        {
            var rowFeedsXml = XDocument.Parse(rowFeeds);

            var feeds = Enumerable.Empty<Feed>();
            
            try
            {
                feeds = from item in rowFeedsXml.Descendants("item")
                    select new Feed(item.Element("guid").Value) //TODO use rss id.
                    {
                        Title = item.Element("title").Value,
                        Description = _htmlToPlainTextConverter.ConvertToPlainText(item.Element("description").Value),
                        FeedDetailUri = new Uri(item.Element("link").Value),
                        PubDate = DateTime.Parse(item.Element("pubDate").Value)
                    };
            }
            catch (Exception ex)
            {
                // nothing to do  
            }

            return feeds;
        }
    }
}