using AppointmentBooking.Application.DTOs;
using AppointmentBooking.Infrastructure.Repositories.SalesManagers;
using AppointmentBooking.Infrastructure.Repositories.Slot;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppointmentBooking.Application.Queries.AppointmentSlotQueries
{
	public class GetAppointmentSlotQueryHandler : IRequestHandler<GetAppointmentSlotQuery, IEnumerable<AppointmentSlotDto>>
	{
		private readonly ISlotRepository _slotRepository;
		private readonly ISalesManagersRepository _salesManagersRepository;
		private readonly ILogger<GetAppointmentSlotQueryHandler> _logger;

		public GetAppointmentSlotQueryHandler(ISlotRepository slotRepository, ISalesManagersRepository salesManagersRepository, ILogger<GetAppointmentSlotQueryHandler> logger)
		{
			_slotRepository = slotRepository ?? throw new ArgumentNullException(nameof(slotRepository));
			_salesManagersRepository = salesManagersRepository ?? throw new ArgumentNullException(nameof(salesManagersRepository));
			_logger = logger;
		}
		public async Task<IEnumerable<AppointmentSlotDto>> Handle(GetAppointmentSlotQuery request, CancellationToken cancellationToken)
		{
			var managerIds = await _salesManagersRepository.SelectManagerIdsAsync(request.Products, request.Language, request.Rating, cancellationToken);
			if (!managerIds.Any())
				return Enumerable.Empty<AppointmentSlotDto>();

			var availableSlots = await _slotRepository.SelectAvailabelSlotsAsync(request.Date, managerIds, cancellationToken);
			if (!availableSlots.Any())
				return Enumerable.Empty<AppointmentSlotDto>();

			var result = availableSlots
				 .GroupBy(slot => new { slot.StartDate })
				 .Select(group => new AppointmentSlotDto
				 {
					 StartDate = group.Key.StartDate,
					 AvailableCount = group
							.Select(s => s.SalesManagerId)
							.Distinct()
							.Count()
				 })
				 .OrderBy(slot => slot.StartDate)
				 .ToList();

			return result;
		}
	}
}
