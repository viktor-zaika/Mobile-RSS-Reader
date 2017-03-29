namespace Mobile_RSS_Reader.Data.Models
{
    public class FeedArticle : BaseModel
    {   
        private readonly string Article;

        public FeedArticle(string id) : base(id)
        {
            // Nothing to do.
        }
    }
}