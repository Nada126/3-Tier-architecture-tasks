namespace App2.DAL.Configration
{
	public class EmployeeConfig : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
            builder.ToTable("Employees");

            //builder.Property(e => e.Name)
            //	.IsRequired()
            //	.HasMaxLength(100);

            //builder.Property(e => e.Age)
            //	.IsRequired();

            //builder.Property(e => e.Salary)
            //	.HasColumnType("decimal(8,2)");

            //builder.Property(e => e.Image)
            //	.HasMaxLength(255);

            //builder.HasOne(e => e.Department)
            //	   .WithMany(d => d.Employees)
            //	   .HasForeignKey(e => e.DepartmentId)
            //	   .OnDelete(DeleteBehavior.SetNull);
        }
	}
}
