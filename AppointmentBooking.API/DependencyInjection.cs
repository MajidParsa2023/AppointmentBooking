using AppointmentBooking.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AppointmentBooking.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

			services.Configure<JsonOptions>(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
			});

			return services;
		}
	}
}
