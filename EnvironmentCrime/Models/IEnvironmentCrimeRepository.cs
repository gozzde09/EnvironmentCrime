using EnvironmentCrime.Models;

namespace EnvironmentCrime.Models
{
  public interface IEnvironmentCrimeRepository
  {
    // Properties to access different entities 
    IQueryable<Department> Departments { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<ErrandStatus> ErrandStatuses { get; }
    IQueryable<Errand> Errands { get; }
    IQueryable<Picture> Pictures { get; }
    IQueryable<Sample> Samples { get; }
    IQueryable<Sequence> Sequences { get; }

    // Save a new errand 
    void SaveErrand(Errand newErrand);

    // Get details of a specific errand by its ID
    Task<Errand?> GetErrandDetails(int id);

  }
}
