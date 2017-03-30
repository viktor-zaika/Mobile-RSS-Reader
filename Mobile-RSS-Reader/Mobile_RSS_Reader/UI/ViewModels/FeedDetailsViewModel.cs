using Mobile_RSS_Reader.Data.Models;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.ViewModels
{   
    /// <summary>
    /// Feed details view model
    /// </summary>
    public class FeedDetailsViewModel : BaseViewModel
    {   
        /// <summary>
        /// Article content.
        /// </summary>
        public HtmlWebViewSource Article { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="article">Full feed article instance.</param>
        public FeedDetailsViewModel(FeedArticle article)
        {
            Article = new HtmlWebViewSource
            {
                Html = article.Article
            }; 
        }
    }
}