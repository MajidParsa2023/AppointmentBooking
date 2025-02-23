using AppointmentBooking.Domain.SeedWork;
using AppointmentBooking.Infrastructure.Repositories;
using AppointmentBooking.Infrastructure.Repositories.EF;
using AppointmentBooking.Infrastructure.Repositories.SalesManagers;
using AppointmentBooking.Infrastructure.Repositories.Slot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			SetServiceType(services);
			ConfigureDatabase(services, configuration);

			return services;
		}

		private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
		{
			//EF: Postgres
			services.AddDbContext<AppointmentBookingDBContext>((serviceProvider, options) =>
			{
				var connectionString = configuration.GetConnectionString(nameof(AppointmentBookingDBContext)); // Type-safe connection string
				var migrationsAssembly = typeof(AppointmentBookingDBContext).Assembly.GetName().Name;

				options.UseNpgsql(connectionString, npgsqlOptions =>
				{
					npgsqlOptions.MigrationsAssembly(migrationsAssembly);
				});
			});
		}

		private static void SetServiceType(IServiceCollection services)
		{
			services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
			services.AddTransient(typeof(ISlotRepository), typeof(SlotRepository));
			services.AddTransient(typeof(ISalesManagersRepository), typeof(SalesManagersRepository));
		}
	}
}
