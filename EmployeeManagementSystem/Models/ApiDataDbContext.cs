using System.Data.Entity;

namespace EmployeeManagementSystem.Models
{
    public class ApiDataDbContext : DbContext
    {
 
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }

    }
}
