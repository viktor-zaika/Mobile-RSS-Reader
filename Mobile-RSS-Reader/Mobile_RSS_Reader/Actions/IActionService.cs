using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mobile_RSS_Reader.Actions
{
    /// <summary>
    /// Describes requirement to application action
    ///  service and describes main abilities of it.
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        /// Updates feeds async.
        /// </summary>
        /// <param name="token"> Operation cancelation token</param>
        /// <returns> Task </returns>
        Task UpdateFeedsAsync(CancellationToken token);

        /// <summary>
        /// Open feed details for given feed URI.
        /// </summary>
        /// <param name="articleUri"> Feed URI</param>
        /// <param name="token"> Operation cancelation token</param>
        /// <returns></returns>
        Task OpenFeedDetailsAsync(Uri articleUri, CancellationToken token);
    }
}