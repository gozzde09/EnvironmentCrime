namespace EnvironmentCrime.Models.AppDb
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
    void UpdateDepartment(int errandId, Department department);

    // Update employee - by manager
    void UpdateEmployee(int errandId, Employee employee);

    // Update investigator info - by manager 
    void UpdateInvestigatorInfo(int errandId, string investigatorInfo);

    // Add investigator info - by investigator
    void AddInvestigatorInfo(int errandId, string investigatorInfo);

    // Add investigator event - by investigator
    void AddInvestigatorEvent(int errandId, string investigatorAction);

    // Update errand status - by investigator
    void UpdateErrandStatus(int errandId, string statusId);

    // Insert file (sample or picture) associated with an errand - by investigator
    Task InsertFileAsync(string dirPath, int errandId, string uniqueFileName);
    // Get errands based on user role
    IQueryable<Case> GetErrands(string role);
    // Get errands for manager's employees
    public IQueryable<Case> GetManagerEmployeeList();
  }
}
