using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            builder.Services.AddControllersWithViews();

            
            /// builder.Services.AddScoped<ApplicationDbContext>();
            /// builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>(ServerProvider =>
            /// {
            ///     
            ///     
            ///     var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            /// 
            ///     optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
              
            //     //var scope = ServerProvider.CreateScope();
            //     //var department = scope.ServiceProvider.GetService<IDepartmentRepository>();   
              
            //     return optionsBuilder.Options;
            // 
            //     
            // });

              builder.Services.AddDbContext<ApplicationDbContext>(optionBuilder =>
               {
                   optionBuilder
                   .UseLazyLoadingProxies()
                   .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
              });
            //ByDefault Scoped
            // contextLifetime:ServiceLifetime.Scoped,
            // optionsLifetime:ServiceLifetime.Scoped,
            // optionsAction:(optionBuilder) =>
            // {
            //     optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            // }
            // );


            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Kestrel MiddleWares
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); 
            #endregion

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
