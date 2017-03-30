using System;

namespace Mobile_RSS_Reader.Data.Models
{   
    /// <summary>
    /// Represent storage model of each feed.
    /// </summary>
    public class Feed : BaseModel
    {   
        /// <summary>
        /// Feed title.
        /// </summary>
        public string Title;
        
        /// <summary>
        /// Feed description.
        /// </summary>
        public string Description;

        /// <summary>
        /// URI of feed details.
        /// </summary>
        public Uri FeedDetailUri;

        /// <summary>
        /// Feed publication date
        /// </summary>
        public DateTime PubDate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Feed id</param>
        public Feed(string id) : base(id)
        {
            // Nothing to do.
        }
    }
}