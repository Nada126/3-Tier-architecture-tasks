 
namespace App2.DAL.Configration
{
	public class DepartmentConfig : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.ToTable("Department").HasKey(d => d.Id);

			builder.Property(d => d.Name)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(d => d.Area)
				   .IsRequired()
				   .HasMaxLength(100);
 
		}
	}
}
