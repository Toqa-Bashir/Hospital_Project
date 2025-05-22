using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareNet_System.Models
{
    public class HosPitalContext : IdentityDbContext<IdentityUser>
    {
        public HosPitalContext(DbContextOptions<HosPitalContext> options)
            : base(options)
        {
        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bills> Bills { get; set; }


    }
}