namespace Mobile_RSS_Reader.Data.Models
{   
    /// <summary>
    /// Model of feed's article.
    /// </summary>
    public class FeedArticle : BaseModel
    {   
        /// <summary>
        /// Simplified content of article
        /// </summary>
        public readonly string Article;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"> Article id.</param>
        /// <param name="article">Article content.</param>
        public FeedArticle(string id, string article) : base(id)
        {
            Article = article;
        }
    }
}