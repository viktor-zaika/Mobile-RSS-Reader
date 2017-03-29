using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Mobile_RSS_Reader.Data.Models;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    /// Represent reactive data entity which provides abilities to storage access
    /// and notify all subscribers about data changes using Rx approach. 
    /// </summary>
    public class ReactiveData // TODO may be add disposable pattern
    {   
        /// <summary>
        /// Refresh state observable.
        /// </summary>
        private readonly BehaviorSubject<bool> _refreshingState = new BehaviorSubject<bool>(false);
        
        /// <summary>
        /// Refresh succeded observable. 
        /// </summary>
        private readonly Subject<Unit> _refreshSucceeded = new Subject<Unit>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataStorage">Storage instance.</param>
        public ReactiveData(DataStorage dataStorage)
        {
            RefreshingStateObservable = _refreshingState.DistinctUntilChanged();

            var rootObservable =
                Observable.Return(Unit.Default)
                    .Concat(_refreshSucceeded);

            FeedListObservable =
                rootObservable
                    .Select(t => dataStorage.GetAllFeeds())
                    .Concat()
                    .Replay(1)
                    .RefCount();
        }

        /// <summary>
        /// Refreshing state observable.
        /// </summary>
        public IObservable<bool> RefreshingStateObservable { get; }

        /// <summary>
        /// Feed list observable.
        /// </summary>
        public IObservable<IEnumerable<Feed>> FeedListObservable { get; }

        /// <summary>
        /// Notify all required observables about refresh operation completion.
        /// </summary>
        /// <param name="isSuccess">true if refresh operation was complited succesfully otherwise false.</param>
        public void FeedRefreshCompleted(bool isSuccess)
        {
            _refreshingState.OnNext(isSuccess);

            if (isSuccess)
            {
                Device.BeginInvokeOnMainThread(() => _refreshSucceeded.OnNext(Unit.Default));
            }
        }
    }
}