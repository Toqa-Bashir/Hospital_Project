using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CareNet_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // ✅ Register custom authorization filter (if needed)
            builder.Services.AddScoped<AuthorizeFilter>();

            // ✅ Configure cookie authentication to use custom login page
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // ✅ Custom login URL
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            // ✅ Configure database context
            builder.Services.AddDbContext<HosPitalContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            // ✅ Configure ASP.NET Identity (customized)
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<HosPitalContext>()
            .AddDefaultTokenProviders();

            // ✅ Register repositories
            builder.Services.AddScoped<IRepository<Staff>, StaffRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // ✅ الاحتفاظ بمستودع الأقسام أيضًا

            var app = builder.Build();

            // ✅ Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ✅ Order is important: Auth before endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            // ✅ Configure routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}