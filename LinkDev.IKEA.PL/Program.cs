using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
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

            // builder.Services.AddScoped<ApplicationDbContext>();
            // builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>(ServerProvider =>
            // {
            //     
            //     
            //     var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // 
            //     optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            // 
            // 
            //     //var scope = ServerProvider.CreateScope();
            // 
            //     //var department = scope.ServiceProvider.GetService<IDepartmentRepository>();
            //    
            // 
            //     return optionsBuilder.Options;
            // 
            //     
            // });

              builder.Services.AddDbContext<ApplicationDbContext>(ServerProvider =>
              {
                  ServerProvider.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
              });
                //ByDefault Scoped
                // contextLifetime:ServiceLifetime.Scoped,
                // optionsLifetime:ServiceLifetime.Scoped,
                // optionsAction:(optionBuilder) =>
                // {
                //     optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                // }
                // );

            
                
                
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
