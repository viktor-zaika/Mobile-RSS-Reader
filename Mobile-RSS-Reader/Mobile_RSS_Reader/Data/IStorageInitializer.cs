using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mobile_RSS_Reader.Data
{
   public interface IStorageInitializer
    {
        Task StartInitializationAsync(CancellationToken ctx);
        
        void CompleteInitialization();
    }
}
