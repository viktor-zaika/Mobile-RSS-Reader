using System.Linq;

namespace Mobile_RSS_Reader.Parsers
{   
    /// <summary>
    /// Allow to extract article content from html page.
    /// </summary>
    public class HtmlArticleParser : IArticleParser
    {
        /// <summary>
        /// Extract article content which is surrounded by article tags.
        /// </summary>
        /// <param name="rowArticle">Raw article html text</param>
        /// <returns>Article conten.</returns>
        public string GetArticleFromPage(string rowArticle)
        {   
            System.Diagnostics.Debug.WriteLine("*** Input" + rowArticle);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(rowArticle);

            var mainText = doc.DocumentNode
                .Descendants("article")
                .First()
                .InnerText;
            return mainText;
        }
    }
}