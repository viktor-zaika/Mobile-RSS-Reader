using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_RSS_Reader.Actions;
using Mobile_RSS_Reader.Data.Models;
using Mobile_RSS_Reader.Parsers;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.UI.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IActionService _actionService;

        private List<FeedPresentationModel> _feeds;

        public List<FeedPresentationModel> Feeds {
            get { return _feeds; }
            set { RaiseAndSetIfChanged(ref _feeds, value); }
        }
        
        public HomePageViewModel(IActionService actionService, IObservable<IEnumerable<Feed>> feedListObservable)
        {
            _actionService = actionService;

            RefreshDataCommand = new Command(async () => await RefreshData()); // TODO remove it, use usual approach.

            feedListObservable.Subscribe(items =>
            {
                Feeds = items?.Select(item => new FeedPresentationModel(item)).ToList();
            });
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            try
            {
                await _actionService.UpdateFeedsAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                
            }
            IsBusy = false;
        }

        bool busy;

        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command) RefreshDataCommand).ChangeCanExecute();
            }
        }

        public class FeedPresentationModel
        {
            public string Text { get; set; }
            public string Detail { get; set; }
            public string Date { get; set; }

            public FeedPresentationModel(Feed feed)
            {
                Text = feed.Title;
                Detail = feed.Description;
                Date = feed.PubDate.ToString("dd MMM HH:mm");
            }

            public override string ToString() => Text;
        }
    }
}