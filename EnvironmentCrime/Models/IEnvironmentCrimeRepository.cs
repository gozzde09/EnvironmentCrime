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

    // Update department - by coordinator
    void UpdateDepartment(int errandId, string choosenDepartment);

    // Update employee - by manager
    void UpdateEmployee(int errandId, Employee employee);

    // Update investigator info - by manager 
    void UpdateInvestigatorInfo(int errandId, string investigatorInfo);

    // Add investigator info - by investigator
    void AddInvestigatorInfo(int errandId, string investigatorInfo);

    // Add investigator event - by investigator
    void AddInvestigatorEvent(int errandId, string investigatorAction);

    // Update errand status
    void UpdateErrandStatus(int errandId, string statusId);

    // Insert file (sample or picture) associated with an errand
    Task InsertFileAsync(string dirPath, int errandId, string uniqueFileName);

  }
}
