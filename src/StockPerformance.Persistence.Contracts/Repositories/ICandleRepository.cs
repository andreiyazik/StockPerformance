using StockPerformance.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockPerformance.Persistence.Contracts.Repositories
{
    public interface ICandleRepository : IRepositoryBase<Candle>
    {
        Task BulkInsertOrUpdateAsync( IList<Candle> entities );
    }
}