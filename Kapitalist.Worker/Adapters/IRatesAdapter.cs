using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Common.ApiModels;

namespace Kapitalist.Worker.Adapters
{
    public interface IRatesAdapter
    {
        string Name { get; }

        Task<RatesSnapshot> Get(CancellationToken cancellationToken);
    }
}