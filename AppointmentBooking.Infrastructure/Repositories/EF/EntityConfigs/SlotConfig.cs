using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentBooking.Infrastructure.Repositories.EF.EntityConfigs
{
	public class SlotConfig : IEntityTypeConfiguration<Domain.AggregatesModel.Slot>
	{
		public void Configure(EntityTypeBuilder<Domain.AggregatesModel.Slot> builder)
		{
			builder.ToTable("slots");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).IsRequired();
			builder.Property(c => c.Id).HasColumnName("id");
			builder.Property(c => c.StartDate).HasColumnName("start_date").HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
			builder.Property(c => c.EndDate).HasColumnName("end_date").HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
			builder.Property(c => c.Booked).HasColumnName("booked");
			builder.Property(c => c.SalesManagerId).HasColumnName("sales_manager_id");
			//builder.HasOne(c => c.SalesManager).WithMany().HasForeignKey(c => c.SalesManagerId);
		}
	}
}
