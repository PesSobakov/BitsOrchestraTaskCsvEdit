using BitsOrchestraTaskCsvEdit.Models.Database;
using BitsOrchestraTaskCsvEdit.Services;
using Microsoft.EntityFrameworkCore;

namespace BitsOrchestraTaskCsvEdit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EmployeeContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration["DATABASE_CONNECTION_STRING"], options2 =>
                {
                    options2.CommandTimeout(300);
                    options2.EnableRetryOnFailure();
                });
            });
            builder.Services.AddTransient<IMainService, MainService>();
            builder.Services.AddTransient<ICsvService, CsvService>();
            builder.Services.AddTransient<IDatabaseService, DatabaseService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
