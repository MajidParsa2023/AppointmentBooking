using AppointmentBooking.Domain.SeedWork;
using AppointmentBooking.Infrastructure.Repositories.EF;
using AppointmentBooking.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Repositories
{
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		protected readonly AppointmentBookingDBContext DbContext;		
		public IQueryable<TEntity> Table => Entities;
		public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
		private DbSet<TEntity> Entities { get; }

		public BaseRepository(AppointmentBookingDBContext dbContext)
		{
			DbContext = dbContext;
			Entities = DbContext.Set<TEntity>();
		}

		public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
		{
			Assert.NotNull(entity, nameof(entity));
			await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
			if (saveNow)
				await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}

		public virtual async Task<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
		{
			var tEntity = await Entities.FindAsync(ids, cancellationToken);
			return tEntity;
		}

		public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
		{
			Assert.NotNull(entity, nameof(entity));
			Entities.Update(entity);
			if (saveNow)
				await DbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
