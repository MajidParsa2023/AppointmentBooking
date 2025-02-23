using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Application.Queries.AppointmentSlotQueries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppointmentBooking.API.Controllers
{
	[Route("calendar")]
	public class CalendarController : BaseApiController
	{
		private readonly ILogger<CalendarController> _logger;

		public CalendarController(ILogger<CalendarController> logger)
		{

			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpPost("query")]
		public async Task<ActionResult<AppointmentSlotDto>> GetAppointmentSlotsAsync([FromBody] GetAppointmentSlotQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Request => {JsonConvert.SerializeObject(query)}");

			var result = await Mediator.Send(query, cancellationToken);

			_logger.LogInformation($"Response => {JsonConvert.SerializeObject(result)}");

			return Ok(result);
		}
	}
}
