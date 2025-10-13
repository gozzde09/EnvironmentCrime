namespace EnvironmentCrime.Models
{
  public interface IEnvironmentCrimeRepository
  {
    // Queryable collections for Departments, Employees, ErrandStatuses, and Errands
    IQueryable<Department> Departments { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    IQueryable<Errand> Errands { get; }

    // Get details of a specific errand by its ID
    Task<Errand?> GetErrandDetails(string id);

  }
}
