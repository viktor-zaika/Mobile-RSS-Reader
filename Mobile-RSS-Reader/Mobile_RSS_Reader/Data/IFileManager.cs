using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_RSS_Reader.Data
{
    public interface ILocalFileManager
    {
        ILocalFile GetLocalFile(string fileName);

        Task<IEnumerable<ILocalSearchedFile>> GetAllLocalFilesAsync();
    }

    public interface ILocalFile
    {
        string FullFileName { get; }

        Task DeleteAsync();

        Task<bool> DoesExistAsync();

        void DisableCloudBackup();
    }

    public interface ILocalSearchedFile
    {
        string FileNameOnly { get; }

        Task<DateTime> GetLastModifiedUtcAsync();

        Task DeleteAsync();
    }
}
