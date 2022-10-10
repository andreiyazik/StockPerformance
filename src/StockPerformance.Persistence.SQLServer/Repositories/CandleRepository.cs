using EFCore.BulkExtensions;
using StockPerformance.Domain.Entities;
using StockPerformance.Persistence.Contracts.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockPerformance.Persistence.SQLServer.Repositories
{
    public class CandleRepository : RepositoryBase<Candle>, ICandleRepository
    {
        public CandleRepository( DataContext dataContext )
            : base( dataContext )
        {
        }

        public async Task BulkInsertOrUpdateAsync( IList<Candle> entities )
        {
            var bulkConfig = new BulkConfig { PreserveInsertOrder = true };
            await _dataContext.BulkInsertOrUpdateAsync( entities, bulkConfig );
        }
    }
}
