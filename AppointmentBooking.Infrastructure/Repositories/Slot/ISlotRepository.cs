using AppointmentBooking.Domain.SeedWork;

namespace AppointmentBooking.Infrastructure.Repositories.Slot
{
	public interface ISlotRepository : IRepository<Domain.AggregatesModel.Slot>
	{
		Task<IEnumerable<Domain.AggregatesModel.Slot>> SelectAvailabelSlotsAsync(DateTime date, IEnumerable<long> managerIds, CancellationToken cancellationToken);
	}
}
