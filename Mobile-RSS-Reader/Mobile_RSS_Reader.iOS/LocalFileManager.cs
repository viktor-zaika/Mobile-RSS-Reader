using System;
using System.IO;
using Mobile_RSS_Reader.Data;
using Mobile_RSS_Reader.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(LocalFileManager))]

namespace Mobile_RSS_Reader.iOS
{
    /// <summary>
    /// Ios local file manager implementation.
    /// </summary>
    public class LocalFileManager : ILocalFileManager
    {
        /// <summary>
        /// Local files directory.
        /// </summary>
        private readonly string _localFileDirectory;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalFileManager()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            _localFileDirectory = Path.Combine(documentsPath, "..", "Library"); // Library folder
        }

        /// <summary>
        /// Privides local file by file name.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Local file</returns>
        public ILocalFile GetLocalFile(string fileName)
        {
            return new LocalFile(_localFileDirectory, fileName);
        }

        /// <summary>
        /// Ios local file.
        /// </summary>
        private class LocalFile : ILocalFile
        {
            /// <summary>
            /// Full file name
            /// </summary>
            public string FullFileName { get; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="directory">Directory</param>
            /// <param name="fileName">File name</param>
            public LocalFile(string directory, string fileName)
            {
                FullFileName = Path.Combine(directory, fileName);
            }
        }
    }
}

