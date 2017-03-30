using System.Threading;
using System.Threading.Tasks;

namespace Mobile_RSS_Reader.Data
{
    /// <summary>
    /// Desribes features of storage initializer.
    /// </summary>
   public interface IStorageInitializer
    {   
        /// <summary>
        /// Starts storage initialization.
        /// </summary>
        /// <param name="ctx">Cancelation token</param>
        /// <returns>Task</returns>
        Task StartInitializationAsync(CancellationToken ctx);
        
        /// <summary>
        /// Completes initialization.
        /// </summary>
        void CompleteInitialization();
    }
}
