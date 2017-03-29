using System;
using System.Linq;
using System.Threading.Tasks;
using Mobile_RSS_Reader.Data;
using System.Threading;

namespace Mobile_RSS_Reader.Actions
{
    public class ActionService : IActionService
    {   
        private readonly DataStorage _storage;
        private readonly FeedProvider _provider;
        private readonly ReactiveData _reactiveData;

        public ActionService(ReactiveData reactiveData, DataStorage storage, FeedProvider provider)
        {
            _reactiveData = reactiveData;
            _storage = storage;
            _provider = provider;
        }

        public async Task UpdateFeedsAsync(CancellationToken token)
        {
           var feeds = await _provider.GetFeedsAsync();
           await _storage.SaveFeedsAsync(feeds.ToList(), token);
            _reactiveData.FeedRefreshCompleted(true); // TODO Looks like try catch need here and real update result info.
        }

        public void OpenFeedDetailsAsync(Uri articleUri, CancellationToken token)
        {
            
        }
    }
}