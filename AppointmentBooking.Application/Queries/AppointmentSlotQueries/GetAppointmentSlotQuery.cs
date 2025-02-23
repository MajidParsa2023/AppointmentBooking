using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Domain.Enums;
using MediatR;

namespace AppointmentBooking.Application.Queries.AppointmentSlotQueries
{
	public class GetAppointmentSlotQuery : IRequest<IEnumerable<AppointmentSlotDto>>
	{
		public required DateTime Date { get; init; }
		public required IReadOnlyList<Product> Products { get; init; }
		public required Language Language { get; init; }
		public required CustomerRating Rating { get; init; }
	}
}
