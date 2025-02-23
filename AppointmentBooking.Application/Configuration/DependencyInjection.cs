using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace AppointmentBooking.Application.Configuration
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMediatR(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
