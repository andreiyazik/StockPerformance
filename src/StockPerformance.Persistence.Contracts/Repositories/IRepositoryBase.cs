using StockPerformance.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace StockPerformance.Persistence.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : IEntity
	{
		Task CreateAsync( TEntity entity );
		Task SaveChangesAsync();
	}
}