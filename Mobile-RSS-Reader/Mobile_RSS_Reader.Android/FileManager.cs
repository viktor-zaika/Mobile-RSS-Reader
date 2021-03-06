using System.IO;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.Droid;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(FileManager))]

namespace Mobile_RSS_Reader.Droid
{   
    /// <summary>
    /// Simple file manager implementation
    /// </summary>
    public class FileManager : ILocalFileManager
    {   
        /// <summary>
        /// Local directory
        /// </summary>
        private readonly string _localFileDirectory;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileManager()
        {
            _localFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        /// <summary>
        /// Provides local file by name.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Local file</returns>
        public ILocalFile GetLocalFile(string fileName)
        {
            return new LocalFile(_localFileDirectory, fileName);
        }

        /// <summary>
        /// Simple implementation of local file.
        /// </summary>
        private class LocalFile : ILocalFile
        {

            /// <summary>
            /// Containe full file name
            /// </summary>
            public string FullFileName { get; }

            /// <summary>
            /// Local file implementation.
            /// </summary>
            /// <param name="directory">File directory.</param>
            /// <param name="fileName">File name.</param>
            public LocalFile(string directory, string fileName)
            {
                FullFileName = Path.Combine(directory, fileName);
            }

        }
    }
}

