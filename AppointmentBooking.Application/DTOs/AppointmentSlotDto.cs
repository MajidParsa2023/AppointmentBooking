using System.Text.Json.Serialization;

namespace AppointmentBooking.Application.DTOs
{
	public record AppointmentSlotDto
	{
		[JsonPropertyName("start_date")]
		public DateTime StartDate { get; init; }

		[JsonPropertyName("available_count")]
		public int AvailableCount { get; init; }
	}
}
