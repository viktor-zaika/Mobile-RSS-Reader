using System;
using Newtonsoft.Json;
using System.Threading;
using Akavache.Sqlite3;
using Xamarin.Forms;

namespace Mobile_RSS_Reader.Data
{
    public class StorageFactory
    {
        public static Tuple<DataStorage, IStorageInitializer> OpenStorage(string storageFileName, int version,
            CancellationToken ctx)
        {
            var fileManager = DependencyService.Get<ILocalFileManager>();
            var fileName = storageFileName;
            var localFile = fileManager.GetLocalFile(fileName);

            var cacheTuple = OpenCache(localFile, version);
          
            return new Tuple<DataStorage, IStorageInitializer>(new DataStorage(cacheTuple.Item1), cacheTuple.Item2);
        }

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

            file.DisableCloudBackup();
            return new Tuple<SQLitePersistentBlobCache, IStorageInitializer>(cache, dataStorageInitializer);
        }

    }
}