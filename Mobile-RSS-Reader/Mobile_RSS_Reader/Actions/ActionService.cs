using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Mobile_RSS_Reader.Data;
using System.Threading;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Actions
{
    /// <summary>
    /// Represent action engine implementation which provides abilities for action performing
    /// </summary>
    public class ActionService : IActionService
    {
        private readonly DataStorage _storage;
        private readonly FeedProvider _provider;
        private readonly ReactiveData _reactiveData;
        private readonly Action<FeedArticle> _openDetailPage;
        private readonly Action _showConnectivityErrorDialog;

        private bool _isConnectivityAvailable;
        private CancellationTokenSource _tokenSource;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reactiveData"> Reactive data instance</param>
        /// <param name="storage"> Application storage</param>
        /// <param name="provider"> Feed provider</param>
        /// <param name="isConnectivityAvailable"> Is connectivity available observable</param>
        /// <param name="openDetailPageAction"> Open feed detail action</param>
        /// <param name="showConnectivityErrorDialog"> Show connectivity error alert dialog</param>
        public ActionService(ReactiveData reactiveData,
            DataStorage storage,
            FeedProvider provider,
            BehaviorSubject<bool> isConnectivityAvailable,
            Action<FeedArticle> openDetailPageAction,
            Action showConnectivityErrorDialog)
        {
            _reactiveData = reactiveData;
            _storage = storage;
            _provider = provider;
            _openDetailPage = openDetailPageAction;
            _tokenSource = new CancellationTokenSource();
            _showConnectivityErrorDialog = showConnectivityErrorDialog;

            isConnectivityAvailable.Subscribe(async isAvailable =>
            {
                _tokenSource.Cancel();
                _tokenSource = new CancellationTokenSource();
                _isConnectivityAvailable = isAvailable;
                if (isAvailable)
                    await UpdateFeedsAsync(CancellationToken.None);
            });
        }

        /// <inheritdoc /> 
        public async Task UpdateFeedsAsync(CancellationToken token)
        {
            if (!_isConnectivityAvailable)
            {
                _showConnectivityErrorDialog();
                return;
            }

            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(token, _tokenSource.Token).Token;

            var feeds = await _provider.GetFeedsAsync(linkedToken);
            await _storage.SaveFeedsAsync(feeds.ToList(), linkedToken);

            _reactiveData.FeedRefreshCompleted(true);
        }

        /// <inheritdoc /> 
        public async Task OpenFeedDetailsAsync(Uri articleUri, CancellationToken token)
        {
            var article = await _provider.GetFullFeedOfflineAsync(articleUri);

            if (article == null && _isConnectivityAvailable)
            {
                var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(token, _tokenSource.Token).Token;
                article = await _provider.GetFullFeedOnlineAsync(articleUri, linkedToken);
            }
            else if (article == null)
            {
                _showConnectivityErrorDialog();
                return;
            }

            if (article != null)
                _openDetailPage(article);
        }
    }
}