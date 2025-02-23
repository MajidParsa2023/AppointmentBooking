using AppointmentBooking.Domain.SeedWork;

namespace AppointmentBooking.Domain.AggregatesModel
{
	public class Slot : BaseEntity<long>, IAggregateRoot
	{
		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }
		public bool Booked { get; private set; }
		public long SalesManagerId { get; private set; }
		public SalesManager SalesManager { get; private set; }


		private Slot(long id, DateTime startDate, DateTime endDate, bool booked, long salesManagerId)
		{
			// Checking invariants
			if (id < 1)
				throw new ArgumentException("Id cannot be lower than or equal to zero.");
			if (salesManagerId < 1)
				throw new ArgumentException("SalesManagerId cannot be lower than or equal to zero.");

			// Each slot corresponds to a one-hour appointment
			TimeSpan duration = endDate - startDate;
			if (duration.TotalHours > 1)
				throw new ArgumentException("The slot duration cannot be more than one hour.");

			// Set entity
			Id = id;
			StartDate = startDate;
			EndDate = endDate;
			Booked = booked;
			SalesManagerId = salesManagerId;
		}
	}
}
