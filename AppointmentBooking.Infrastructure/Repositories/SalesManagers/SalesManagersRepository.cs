using AppointmentBooking.Domain.AggregatesModel;
using AppointmentBooking.Domain.Enums;
using AppointmentBooking.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppointmentBooking.Infrastructure.Repositories.SalesManagers
{
	public class SalesManagersRepository : BaseRepository<SalesManager>, ISalesManagersRepository
	{
		private readonly ILogger<SalesManagersRepository> _logger;

		public SalesManagersRepository(AppointmentBookingDBContext dbContext, ILogger<SalesManagersRepository> logger) : base(dbContext)
		{
			_logger = logger;
		}

		public async Task<IEnumerable<long>> SelectManagerIdsAsync(IEnumerable<Product> products, Language language, CustomerRating rating, CancellationToken cancellationToken)
		{
			// TODO: Paging
			// TODO: To increase performance, We can use the ProjectTo<> method of Automapper instead of the Including a command.

			var productNames = products.Select(p => p.ToString()).ToList();
			var languageString = language.ToString();
			var ratingString = rating.ToString();

			var result = await TableNoTracking
								.Where(s => s.Languages.Contains(languageString) &&
												s.CustomerRatings.Contains(ratingString) &&
												productNames.All(p => s.Products.Contains(p)))
								.Select(s => s.Id)
								.ToListAsync(cancellationToken);

			return result;
		}
	}
}
