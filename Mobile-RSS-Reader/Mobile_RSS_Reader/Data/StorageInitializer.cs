using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Akavache.Sqlite3;

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    /// Sqlite storage initializer.
    /// </summary>
    public class StorageInitializer : IStorageInitializer
    {
        private readonly SQLitePersistentBlobCache _cache;
        private readonly int _targetDbVersion;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache">Cache</param>
        /// <param name="targetDbVersion">Target db version</param>
        public StorageInitializer(SQLitePersistentBlobCache cache, int targetDbVersion)
        {
            _cache = cache;
            _targetDbVersion = targetDbVersion;
        }

        /// <inheritdoc />
        public Task StartInitializationAsync(CancellationToken ctx)
        {
            return _cache.InvalidateAll().ToTask(ctx);
        }

        /// <inheritdoc />
        public void CompleteInitialization()
        {
            SetDbVersion(_cache, _targetDbVersion);
        }

        /// <summary>
        /// Gets storage version
        /// </summary>
        /// <param name="cache">Cache</param>
        /// <returns>Storage version</returns>
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

        /// <summary>
        /// Sets storage version.
        /// </summary>
        /// <param name="cache">SQLite persistent cache</param>
        /// <param name="dbVersion">Db version</param>
        private static void SetDbVersion(SQLitePersistentBlobCache cache, int dbVersion)
        {
            cache.Connection.Execute("PRAGMA user_version = " + dbVersion);
        }
    }
}