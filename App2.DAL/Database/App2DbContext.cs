

namespace App2.DAL.Database
{
    public class App2DbContext:DbContext
    {
		public App2DbContext(DbContextOptions<App2DbContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=ITI4MonthDay6;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true ");
        //}
        public DbSet <Employee> Employees { get; set; }
		public DbSet <Department> Departments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new EmployeeConfig());
			modelBuilder.ApplyConfiguration(new DepartmentConfig());
		}
	}
}
