using Microsoft.OpenApi.Models;

namespace AppointmentBooking.API.Configuration
{
	public static class CustomSwaggerExtension
	{
		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			// Register the Swagger generator
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "AppointmentBooking.API",
					Version = "V1",
					Description = "Enpal Coding Challenge",
				});
			});

			return services;
		}
	}
}
