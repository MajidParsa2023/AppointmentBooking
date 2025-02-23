using AppointmentBooking.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppointmentBooking.Infrastructure.Repositories.Slot
{
	public class SlotRepository : BaseRepository<Domain.AggregatesModel.Slot>, ISlotRepository
	{
		private readonly ILogger<SlotRepository> _logger;

		public SlotRepository(AppointmentBookingDBContext dbContext, ILogger<SlotRepository> logger) : base(dbContext)
		{
			_logger = logger;
		}

		public async Task<IEnumerable<Domain.AggregatesModel.Slot>> SelectAvailabelSlotsAsync(DateTime date, IEnumerable<long> managerIds, CancellationToken cancellationToken)
		{
			// TODO: Paging
			// TODO: To increase performance, We can use the ProjectTo<> method of Automapper instead of the Including a command.

			var dateUtc = DateTime.SpecifyKind(date, DateTimeKind.Utc);

			var bookedSlotsQuery = SelectBookedSlotsQuery(dateUtc, managerIds);

			var result = await TableNoTracking
					.Where(s => s.StartDate >= dateUtc.Date &&
									s.StartDate < dateUtc.Date.AddDays(1) &&
									!s.Booked &&
									managerIds.Contains(s.SalesManagerId) &&
									!bookedSlotsQuery.Any(b => b == s.Id)
									)
					.OrderBy(s => s.StartDate)
					.ToListAsync(cancellationToken);

			return result;
		}

		// We should not pass a Queryable object to other layers. It will cause a performance issue. So we should set the method as private.
		private IQueryable<long> SelectBookedSlotsQuery(DateTime dateUtc, IEnumerable<long> managerIds)
		{
			// Get all booked slots
			var bookedSlots = TableNoTracking
				 .Where(s => s.Booked &&
								 s.StartDate >= dateUtc.Date &&
								 s.StartDate < dateUtc.Date.AddDays(1) &&
								 managerIds.Contains(s.SalesManagerId));

			// Get the IDs of booked slots and their adjacent slots (1 hour before and after)
			var bookedAndAdjacentSlots = bookedSlots
				 .SelectMany(bookedSlot => TableNoTracking
					  .Where(s => s.SalesManagerId == bookedSlot.SalesManagerId &&
									(
										(s.StartDate >= bookedSlot.StartDate && s.StartDate < bookedSlot.StartDate.AddHours(1)) ||
										(s.StartDate < bookedSlot.StartDate && s.EndDate > bookedSlot.StartDate)
									))
				 .Select(s => s.Id)
				 .Distinct());

			return bookedAndAdjacentSlots;

		}

	}
}
