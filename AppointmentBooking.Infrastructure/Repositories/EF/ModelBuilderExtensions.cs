using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace AppointmentBooking.Infrastructure.Repositories.EF
{
	public static class ModelBuilderExtensions
	{

		public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
		{
			IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
				 .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

			foreach (Type type in types)
			{
				modelBuilder.Entity(type);
			}

			//modelBuilder.HasDefaultSchema("public");
		}

		public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
		{
			IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
				 .SelectMany(t => t.GetForeignKeys())
				 .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
			foreach (IMutableForeignKey fk in cascadeFKs)
				fk.DeleteBehavior = DeleteBehavior.Restrict;
		}

		public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
		{
			MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

			IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
				 .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

			foreach (Type type in types)
			{
				foreach (Type iface in type.GetInterfaces())
				{
					if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
					{
						MethodInfo applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
						applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
					}
				}
			}
		}
	}
}
