using CrudDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudDemo.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
