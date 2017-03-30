using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_RSS_Reader.Actions;
using Mobile_RSS_Reader.Data.Models;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.ViewModels
{
    /// <summary>
    /// Home page view model
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        /// <summary>
        /// Action service.
        /// </summary>
        private readonly IActionService _actionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="actionService">Action service</param>
        /// <param name="feedListObservable">Feeds list observable</param>
        public HomePageViewModel(IActionService actionService, IObservable<IEnumerable<Feed>> feedListObservable)
        {
            _actionService = actionService;

            RefreshDataCommand = new Command(async () => await RefreshData()); // TODO remove it, use usual approach.

            feedListObservable.Subscribe(items =>
            {
                Feeds = items
                    ?.OrderByDescending(item => item.PubDate)
                    .Select(item => new FeedPresentationModel(item))
                    .ToList();

                IsActionExecuting = !Feeds.Any();
            });

            IsActionExecuting = true;
        }

        public ICommand RefreshDataCommand { get; }

        /// <summary>
        /// Handles feed item selected event.
        /// </summary>
        /// <param name="presentationModel"> Feed presentation model of selected item</param>
        /// <returns></returns>
        public async Task HandleItemSelectedAsync(FeedPresentationModel presentationModel)
        {
            IsActionExecuting = true;
            await _actionService.OpenFeedDetailsAsync(presentationModel.Link, CancellationToken.None);
            IsActionExecuting = false;
        }

        /// <summary>
        /// True if any not related to data update actions is executing.
        /// </summary>
        private bool _isActionExecuting;

        public bool IsActionExecuting
        {
            get { return _isActionExecuting; }
            set { RaiseAndSetIfChanged(ref _isActionExecuting, value); }
        }

        /// <summary>
        /// List of feed presentation models
        /// </summary>
        private List<FeedPresentationModel> _feeds;

        public List<FeedPresentationModel> Feeds
        {
            get { return _feeds; }
            set { RaiseAndSetIfChanged(ref _feeds, value); }
        }

        /// <summary>
        /// Perform data refresh.
        /// </summary>
        /// <returns>Task</returns>
        private async Task RefreshData()
        {
            IsBusy = true;

            await _actionService.UpdateFeedsAsync(CancellationToken.None);

            IsBusy = false;
        }

        /// <summary>
        /// True if refresh operation or other operation is running, otherwise false.
        /// </summary>
        private bool _busy;

        public bool IsBusy
        {
            get { return _busy; }
            set
            {
                _busy = value;
                OnPropertyChanged();
                ((Command) RefreshDataCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Feed presentation model
        /// </summary>
        public class FeedPresentationModel
        {
            public string FeedId { get; }
            public Uri Link { get; }
            public string Text { get; set; }
            public string Detail { get; set; }
            public string Date { get; set; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="feed">Feed</param>
            public FeedPresentationModel(Feed feed)
            {
                FeedId = feed.Id;
                Link = feed.FeedDetailUri;
                Text = feed.Title;
                Detail = feed.Description;
                Date = feed.PubDate.ToString("dd MMM HH:mm");
            }
        }
    }
}