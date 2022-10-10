using EmployeeManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagement.Data
{
    public class ApplicationDBContext: IdentityDbContext<Employee>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAttendance> EmpAttendances { get; set; }
        public DbSet<Justification> Justifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<EmployeeAttendance>()
                .HasKey(empAttendance => new { empAttendance.EmployeeId, empAttendance.CheckIn });

            {// Seeding 
                IdentityRole adminRole = new IdentityRole("Administrator");
                adminRole.NormalizedName = "ADMINISTRATOR";

                modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            }
        }
    }
}
