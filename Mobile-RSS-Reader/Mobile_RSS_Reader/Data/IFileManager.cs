

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    /// Describes requirement to planform depended file manager
    /// </summary>
    public interface ILocalFileManager
    {
        /// <summary>
        /// Provides file by given name.
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Local file</returns>
        ILocalFile GetLocalFile(string fileName);
    }

    /// <summary>
    /// Describes requirements to local file
    /// </summary>
    public interface ILocalFile
    {
        /// <summary>
        /// Full file name
        /// </summary>
        string FullFileName { get; }
    }
}
