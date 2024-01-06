using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ADVADbContext : DbContext
    {
        public ADVADbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring precision for Salary in Employee
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            // Defining the relationship for Employee's Manager
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.NoAction); // Change to No Action

            // Configuring Department-Manager relationship
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithOne()
                .HasForeignKey<Department>(d => d.ManagerId)
                .OnDelete(DeleteBehavior.NoAction); // Change to No Action

            base.OnModelCreating(modelBuilder);
        }
    }
}
