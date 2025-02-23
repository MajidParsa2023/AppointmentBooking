using AppointmentBooking.Domain.SeedWork;

namespace AppointmentBooking.Domain.AggregatesModel
{
	public class SalesManager : BaseEntity<long>, IAggregateRoot
	{
		public string Name { get; private set; }
		public List<string> Languages { get; private set; }
		public IReadOnlyList<string> Products { get; private set; }
		public IReadOnlyList<string> CustomerRatings { get; private set; }
		public ICollection<Slot> Slots { get; private set; }


		private SalesManager(long id, string name, List<string> languages, IReadOnlyList<string> products, IReadOnlyList<string> customerRatings)
		{
			// Checking invariants
			if (id < 1)
				throw new ArgumentException("Id cannot be lower than or equal to zero.");
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("The Name field must not be empty.");

			// Set entity
			Id = id;
			Name = name;
			Languages = languages;
			Products = products;
			CustomerRatings = customerRatings;
			Slots = new List<Slot>();
		}
	}
}
