using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Akavache.Sqlite3;

namespace Mobile_RSS_Reader.Data
{
    public class StorageInitializer : IStorageInitializer
    {
        private readonly SQLitePersistentBlobCache _cache;
        private readonly int _targetDbVersion;

        public StorageInitializer(SQLitePersistentBlobCache cache, int targetDbVersion)
        {
            _cache = cache;
            _targetDbVersion = targetDbVersion;
        }

        public Task StartInitializationAsync(CancellationToken ctx)
        {
            return _cache.InvalidateAll().ToTask(ctx);
        }

        public void CompleteInitialization()
        {
            SetDbVersion(_cache, _targetDbVersion);
        }

        public static int? TryGetDbVersion(SQLitePersistentBlobCache cache)
        {
            try
            {
                return cache.Connection.ExecuteScalar<int>("PRAGMA user_version;");
            }
            catch
            {
                return null;
            }
        }

        private static void SetDbVersion(SQLitePersistentBlobCache cache, int dbVersion)
        {
            cache.Connection.Execute("PRAGMA user_version = " + dbVersion);
        }
    }
}