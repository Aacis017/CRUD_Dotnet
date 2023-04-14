using CRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class CURDDemoDbContext : DbContext
    {
        public CURDDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
