using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddTransient<IAttachmentService,AttachmentService>();

            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<ApplicationUser>>();

            //builder.Services.AddIdentity<ApplicationUser,IdentityRole>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                // options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false; // Default True  {  #%$  }
                                                                 // options.Password.RequireUppercase = true;
                                                                 // options.Password.RequireLowercase = true;
                                                                 // options.Password.RequireDigit = true;
                                                                 // options.Password.RequiredUniqueChars = 5;





                //  options.User.RequireUniqueEmail = true;
                //  options.User.AllowedUserNameCharacters = "";
                //  options.Lockout.AllowedForNewUsers = true;
                //  options.Lockout.MaxFailedAccessAttempts = 3;
                //  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(900);
            })
                            .AddEntityFrameworkStores<ApplicationDbContext>();

         
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = ("/Account/SignIn");
            });
                            
            
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

            app.UseAuthentication(); 
            app.UseAuthorization(); 
            #endregion

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
	
}
