using AppointmentBooking.API;
using AppointmentBooking.API.Configuration;
using AppointmentBooking.Application.Configuration;
using AppointmentBooking.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	 .AddCustomSwagger()
	 .AddMediatR()
	 .AddInfrastructure(builder.Configuration)
	 .AddPresentation(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseCustomSwaggerUiExceptionHandler();
app.UseAuthorization();
app.MapControllers();

app.Run();
