using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentBooking.Infrastructure.Repositories.EF.EntityConfigs
{
	public class SalesManagerConfig : IEntityTypeConfiguration<Domain.AggregatesModel.SalesManager>
	{
		public void Configure(EntityTypeBuilder<Domain.AggregatesModel.SalesManager> builder)
		{
			builder.ToTable("sales_managers");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).IsRequired();
			builder.Property(c => c.Id).HasColumnName("id");
			builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(250);
			builder.Property(c => c.Languages).HasColumnName("languages").HasColumnType("varchar[]").HasMaxLength(100);
			builder.Property(c => c.Products).HasColumnName("products").HasColumnType("varchar[]").HasMaxLength(100);
			builder.Property(c => c.CustomerRatings).HasColumnName("customer_ratings").HasColumnType("varchar[]").HasMaxLength(100);
			//builder.Property(c => c.Slots).HasColumnName("slots");
		}
	}
}
