using AppointmentBooking.Domain.Enums;
using AppointmentBooking.Domain.SeedWork;

namespace AppointmentBooking.Infrastructure.Repositories.SalesManagers
{
	public interface ISalesManagersRepository : IRepository<Domain.AggregatesModel.SalesManager>
	{
		Task<IEnumerable<long>> SelectManagerIdsAsync(IEnumerable<Product> products, Language language, CustomerRating rating, CancellationToken cancellationToken);
	}
}
