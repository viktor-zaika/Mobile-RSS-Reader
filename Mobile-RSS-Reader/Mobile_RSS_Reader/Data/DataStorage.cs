using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using Akavache.Sqlite3;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    /// Represent platform independent storage which base on Sqlite persistent blob cache.
    /// </summary>
    public class DataStorage
    {
        /// <summary>
        /// Sqlite cache.
        /// </summary>
        private readonly SQLitePersistentBlobCache _cache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache">Sqlite persistance cache</param>
        public DataStorage(SQLitePersistentBlobCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Provide required observable using given getter.
        /// </summary>
        /// <param name="getter">Getter function</param>
        /// <returns></returns>
        private IObservable<T> GetObservableFromCache<T>(Func<SQLitePersistentBlobCache, IObservable<T>> getter)
        {
            return getter(_cache);
        }

        /// <summary>
        /// Clear all data async
        /// </summary>
        /// <returns>Clear operation task</returns>
        internal async Task ClearDataAsync()
        {
            await _cache.InvalidateAll().ToTask();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="keyFunc"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private async Task<IEnumerable<string>> SaveAllObjectsAsync<T>(IReadOnlyList<T> objects,
            Func<string, string> keyFunc, CancellationToken ctx) where T : BaseModel
        {
            if (!objects.Any())
            {
                return new string[0];
            }

            var objectDictionary = objects.ToDictionary(t => keyFunc(t.Id));
            await _cache.InsertObjects(objectDictionary).ToTask(ctx);
            return objectDictionary.Keys;
        }

        /// <summary>
        /// Save given object to storage with given key.
        /// </summary>
        /// <param name="key">Object key</param>
        /// <param name="obj">Object for saving</param>
        /// <param name="ctx">Object saving cancelation token</param>
        /// <returns></returns>
        private async Task<string> SaveObjectAsync<T>(string key, T obj, CancellationToken ctx)
        {
            await _cache.InsertObject(key, obj).ToTask(ctx);
            return key;
        }

        /// <summary>
        /// Saves object async.
        /// </summary>
        /// <param name="obj">Object for saving</param>
        /// <param name="keyFunc">Key func</param>
        /// <param name="ctx">Cancelation token</param>
        /// <returns>Task</returns>
        private Task<string> SaveObjectAsync<T>(T obj, Func<string, string> keyFunc, CancellationToken ctx)
            where T : BaseModel
        {
            return SaveObjectAsync(keyFunc(obj.Id), obj, ctx);
        }

        /// <summary>
        /// Prived feed key for given id.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Feed key</returns>
        private static string FeedId2Key(string id) => "feed:" + id;

        /// <summary>
        /// Provides all saved in storage feeds.
        /// </summary>
        /// <returns>List of stored feeds</returns>
        public IObservable<IEnumerable<Feed>> GetAllFeeds()
        {
            return GetObservableFromCache(cache => cache.GetAllObjects<Feed>());
        }

        /// <summary>
        /// Removes feeds async by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="ctx">Cancelation token</param>
        /// <returns></returns>
        public async Task DeleteFeedAsync(string id, CancellationToken ctx)
        {
            await _cache.InvalidateObject<Feed>(FeedId2Key(id)).ToTask(ctx);
        }

        /// <summary>
        /// Save given list of feeds to storage.
        /// </summary>
        /// <param name="feeds">Feeds for saving</param>
        /// <param name="ctx">Saving operation cancelation token</param>
        /// <returns>Saved keys</returns>
        public Task<IEnumerable<string>> SaveFeedsAsync(IReadOnlyList<Feed> feeds, CancellationToken ctx)
        {
            return SaveAllObjectsAsync(feeds, FeedId2Key, ctx);
        }

        /// <summary>
        /// Provides key for feed acrticle.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Key</returns>
        private static string ArticleId2Key(string id) => "Article:" + id;

        public IObservable<IEnumerable<FeedArticle>> GetAllFeedArticles()
        {
            return GetObservableFromCache(cache => cache.GetAllObjects<FeedArticle>());
        }

        /// <summary>
        /// Removes feed article by key
        /// </summary>
        /// <param name="id">Id for remove</param>
        /// <param name="ctx">Cancelation token</param>
        /// <returns>Task</returns>
        public async Task DeleteFeedArticleAsync(string id, CancellationToken ctx)
        {
            await _cache.InvalidateObject<FeedArticle>(ArticleId2Key(id)).ToTask(ctx);
        }

        /// <summary>
        /// Provides all saved in storage feeds.
        /// </summary>
        /// <returns>List of stored feeds</returns>
        public IObservable<FeedArticle> GetFeedArticle(string id)
        {
            return
                GetObservableFromCache(cache => cache.GetObject<FeedArticle>(ArticleId2Key(id)))
                    .Catch(Observable.Return((FeedArticle) null));
        }

        /// <summary>
        /// Save given list of feeds to storage.
        /// </summary>
        /// <param name="feeds">Feeds for saving</param>
        /// <param name="ctx">Saving operation cancelation token</param>
        /// <returns>Saved keys</returns>
        public Task SaveFeedArticleAsync(FeedArticle article, CancellationToken ctx)
        {
            return SaveObjectAsync(article, ArticleId2Key, ctx);
        }
    }
}