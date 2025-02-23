namespace AppointmentBooking.Domain.SeedWork
{
	public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		IQueryable<TEntity> Table { get; }
		IQueryable<TEntity> TableNoTracking { get; }

		Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
	}
}
