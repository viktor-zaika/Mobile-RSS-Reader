using System;
using System.Threading;
using Akavache.Sqlite3;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    ///  Storage factory
    /// </summary>
    public class StorageFactory
    {
        /// <summary>
        /// Opens storage
        /// </summary>
        /// <param name="storageFileName">Sqlite storage file name</param>
        /// <param name="version">version</param>
        /// <param name="ctx">Cancelation token</param>
        /// <returns>Storage and initializer pair.</returns>
        public static Tuple<DataStorage, IStorageInitializer> OpenStorage(string storageFileName, int version,
            CancellationToken ctx)
        {
            var fileManager = DependencyService.Get<ILocalFileManager>();
            var fileName = storageFileName;
            var localFile = fileManager.GetLocalFile(fileName);

            var cacheTuple = OpenCache(localFile, version);

            return new Tuple<DataStorage, IStorageInitializer>(new DataStorage(cacheTuple.Item1), cacheTuple.Item2);
        }

        /// <summary>
        /// Opens cache.
        /// </summary>
        /// <param name="file">Storage file name</param>
        /// <param name="dbVersion">Storage version</param>
        /// <returns>Return cache and storage initializer pair</returns>
        private static Tuple<SQLitePersistentBlobCache, IStorageInitializer> OpenCache(ILocalFile file, int dbVersion)
        {
            var fullDbFileName = file.FullFileName;

            var cache = new SQLitePersistentBlobCache(fullDbFileName);
            var actualDbVersion = StorageInitializer.TryGetDbVersion(cache);
            IStorageInitializer dataStorageInitializer = null;
            if (actualDbVersion.HasValue == false || actualDbVersion.Value != dbVersion)
            {
                dataStorageInitializer = new StorageInitializer(cache, dbVersion);
            }

            return new Tuple<SQLitePersistentBlobCache, IStorageInitializer>(cache, dataStorageInitializer);
        }
    }
}