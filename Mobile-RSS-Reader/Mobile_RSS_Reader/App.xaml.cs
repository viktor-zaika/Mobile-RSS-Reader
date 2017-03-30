using System.Reactive.Subjects;
using System.Threading;
using Mobile_RSS_Reader.Actions;
using Mobile_RSS_Reader.Converters;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.Data.Models;
using Mobile_RSS_Reader.Parsers;
using Mobile_RSS_Reader.UI.Styles;
using Mobile_RSS_Reader.UI.ViewModels;
using Mobile_RSS_Reader.UI.Views;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Mobile_RSS_Reader
{
    /// <summary>
    /// Application
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Storage file name.
        /// </summary>
        private readonly string StorageFileName = "FeedReader";

        /// <summary>
        /// Connectivity observable
        /// </summary>
        private readonly BehaviorSubject<bool> _isConnectivityAvailable;

        /// <summary>
        /// Action service instace.
        /// </summary>
        private readonly ActionService _actionService;

        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Apply styles.
            if (Resources == null)
            {
                Resources = new ResourceDictionary();
            }

            FeedCardStyling.Apply(Resources);

            // Open and initialize storage.
            var tuple = StorageFactory.OpenStorage(StorageFileName, 1, CancellationToken.None);

            if (tuple.Item2 != null)
            {
                tuple.Item2.StartInitializationAsync(CancellationToken.None);
                tuple.Item2.CompleteInitialization();
            }

            // Subscribe for connectivity changing events.
            _isConnectivityAvailable = new BehaviorSubject<bool>(CrossConnectivity.Current.IsConnected);

            CrossConnectivity.Current.ConnectivityChanged +=
                (sender, args) => { _isConnectivityAvailable.OnNext(args.IsConnected); };

            var reactiveData = new ReactiveData(tuple.Item1);
            var feedProvider = new FeedProvider(new RssParser(new HtmlToTextConverter()), tuple.Item1);

            _actionService = new ActionService(reactiveData,
                tuple.Item1, feedProvider,
                _isConnectivityAvailable,
                OpenDetailPage,
                ShowConnectivityErrorDialog);

            var mainPage = new HomePage(new HomePageViewModel(_actionService, reactiveData.FeedListObservable));

            // Set main page.
            MainPage = new NavigationPage(mainPage);
        }

        /// <summary>
        /// Opens details page for given article.
        /// </summary>
        /// <param name="article">Full feed article</param>
        private void OpenDetailPage(FeedArticle article)
        {
            var model = new FeedDetailsViewModel(article);
            MainPage.Navigation.PushAsync(new FeedDetailsPage(model));
        }

        /// <summary>
        /// Shows alert view with connectivity error message.
        /// </summary>
        private void ShowConnectivityErrorDialog()
        {
            MainPage.DisplayAlert("Connectivity Error", "Please check your internet connection.", "OK");
        }

        // <inheritdoc /> 
        protected override void OnStart()
        {
            _actionService.UpdateFeedsAsync(CancellationToken.None);
        }

        // <inheritdoc /> 
        protected override void OnSleep()
        {
            // Nothing to do.
        }

        // <inheritdoc /> 
        protected override void OnResume()
        {
            // Nothing to do.
        }
    }
}
