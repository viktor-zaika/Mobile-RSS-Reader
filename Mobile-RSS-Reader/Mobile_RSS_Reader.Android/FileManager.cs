using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.Droid;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]

namespace Mobile_RSS_Reader.Droid
{
    public class FileManager : ILocalFileManager
    {
        private readonly string _localFileDirectory;

        public FileManager()
        {
            _localFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public ILocalFile GetLocalFile(string fileName)
        {
            return new LocalFile(_localFileDirectory, fileName);
        }

        public Task<IEnumerable<ILocalSearchedFile>> GetAllLocalFilesAsync()
        {
            IEnumerable<ILocalSearchedFile> allSearchedFiles =
                Directory.EnumerateFiles(_localFileDirectory).Select(f => new LocalFile(_localFileDirectory, f));
            return Task.FromResult(allSearchedFiles);
        }

        private class LocalFile : ILocalFile, ILocalSearchedFile
        {
            public LocalFile(string directory, string fileName)
            {
                FileNameOnly = fileName;
                FullFileName = Path.Combine(directory, fileName);
            }

            public string FullFileName { get; }

            public string FileNameOnly { get; }

            public Task<DateTime> GetLastModifiedUtcAsync()
            {
                var lastModified = File.GetLastWriteTime(FullFileName);
                return Task.FromResult(lastModified);
            }

            public Task DeleteAsync()
            {
                File.Delete(FullFileName);
                return Task.FromResult(0);
            }

            public Task<bool> DoesExistAsync()
            {
                return Task.FromResult(File.Exists(FullFileName));
            }

            public void DisableCloudBackup()
            {
                // nothing needed for Android, this is really mostly for iOS
            }
        }
    }
}

