namespace Mobile_RSS_Reader.Parsers
{   
    /// <summary>
    /// Describes requirement to article parser which extract article content from row article.
    /// </summary>
    public interface IArticleParser
    {   
        /// <summary>
        /// Extract article content from raw article.
        /// </summary>
        /// <param name="rowArticle"></param>
        /// <returns>Article content</returns>
        string GetArticleFromPage(string rowArticle);
    }
}