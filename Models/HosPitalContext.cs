using Microsoft.EntityFrameworkCore;


namespace CareNet_System.Models
{
    public class HosPitalContext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bills> Bills { get; set; }


        public HosPitalContext() : base() { }
        public HosPitalContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hospital;Integrated Security=True;Encrypt=False");

        }
    }
}


//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


//namespace CareNet_System.Models
//{
//    public class HosPitalContext : IdentityDbContext<IdentityUser>
//    {
//        public DbSet<Staff> Staff { get; set; }
//        public DbSet<Department> Departments { get; set; }
//        public DbSet<Patient> Patients { get; set; }
//        public DbSet<Bills> Bills { get; set; }

//        public HosPitalContext() : base() { }

//        public HosPitalContext(DbContextOptions<HosPitalContext> options)
//            : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hospital;Integrated Security=True;Encrypt=False");
//        }
//    }
//}
