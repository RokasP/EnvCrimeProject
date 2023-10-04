using EnvCrime.Models.poco;
using Microsoft.EntityFrameworkCore;

namespace EnvCrime.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Errand> Errands { get; set; }

        public DbSet<ErrandStatus> ErrandStatus1 { get; set; } // hellre ErrandStatusES?

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Sample> Samples { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Sequence> Sequences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
