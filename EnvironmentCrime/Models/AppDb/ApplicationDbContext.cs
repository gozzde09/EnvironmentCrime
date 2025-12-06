using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models.AppDb
{
  public class ApplicationDbContext :DbContext
  {
    // Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // DbSets for each entity
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Errand> Errands { get; set; }
    public DbSet<ErrandStatus> ErrandStatuses { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Sample> Samples { get; set; }
    public DbSet<Sequence> Sequences { get; set; }
    // DTO/query-objekt
    public DbSet<Case> Cases { get; set; }
  }
}
