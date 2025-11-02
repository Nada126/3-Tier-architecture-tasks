
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
 
namespace App2.DAL.Database
{
    public class App2DbContext : IdentityDbContext<Employee, IdentityRole, string>
    {
        public App2DbContext(DbContextOptions<App2DbContext> options)
            : base(options) { }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DepartmentConfig());
             
            modelBuilder.Entity<Employee>().ToTable("AspNetUsers");
        }
    }
}
