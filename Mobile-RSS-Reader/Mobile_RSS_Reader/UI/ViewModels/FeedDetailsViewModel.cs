using System;

namespace Mobile_RSS_Reader.UI.ViewModels
{
    public class FeedDetailsViewModel : BaseViewModel
    {
        public string Article { get; }

        public FeedDetailsViewModel(string article)
        {
            Article = article;
        }
    }
}