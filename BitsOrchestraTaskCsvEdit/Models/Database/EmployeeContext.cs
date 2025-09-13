using Microsoft.EntityFrameworkCore;

namespace BitsOrchestraTaskCsvEdit.Models.Database
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }

    }
}
