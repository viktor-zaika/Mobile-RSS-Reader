using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mobile_RSS_Reader.Data.Models;

namespace Mobile_RSS_Reader.Actions
{
    public interface IActionService
    {
       Task UpdateFeedsAsync(CancellationToken token);

       void OpenFeedDetailsAsync();

    }
}