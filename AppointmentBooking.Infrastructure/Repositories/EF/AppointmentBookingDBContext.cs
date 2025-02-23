using AppointmentBooking.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppointmentBooking.Infrastructure.Repositories.EF
{
	public class AppointmentBookingDBContext : DbContext
	{
		public AppointmentBookingDBContext(DbContextOptions<AppointmentBookingDBContext> options)
			 : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var entitiesAssembly = typeof(IEntity).Assembly;
			modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
			modelBuilder.AddRestrictDeleteBehaviorConvention();
			modelBuilder.RegisterEntityTypeConfiguration(Assembly.GetExecutingAssembly());
		}
	}
}
