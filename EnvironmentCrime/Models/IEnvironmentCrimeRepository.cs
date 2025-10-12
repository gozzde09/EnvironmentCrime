namespace EnvironmentCrime.Models
{
  public interface IEnvironmentCrimeRepository
  {
    IQueryable<Department> Departments { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    IQueryable<Errand> Errands { get; }

  }
}
