namespace EnvironmentCrime.Models
{
  public class EFEnvironmentCrimeRepository : IEnvironmentCrimeRepository
  {
    private ApplicationDbContext context;

    // Constructor that accepts the database context via dependency injection
    public EFEnvironmentCrimeRepository(ApplicationDbContext ctx)
    {
      context = ctx;
    }
    // IQueryable properties to access each entity set
    public IQueryable<Department> Departments => context.Departments;

    public IQueryable<Employee> Employees => context.Employees;

    public IQueryable<Errand> Errands => context.Errands;

    public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;

    public IQueryable<Picture> Pictures => context.Pictures;

    public IQueryable<Sample> Samples => context.Samples;

    public IQueryable<Sequence> Sequences => context.Sequences;

    // Method to save a new errand to the database
    public void SaveErrand(Errand newErrand)
    {
      {
        if (newErrand.ErrandId == 0)
        {
          var sequence = Sequences.FirstOrDefault(s => s.Id == 1);
          if (sequence != null)
          {
            // Generate a reference number using the current year and sequence value
            newErrand.RefNumber = DateTime.Now.Year + "-45-" + sequence.CurrentValue;
            newErrand.StatusId = "S_A";
            sequence.CurrentValue++;
            context.Errands.Add(newErrand);
          }
        }
        context.SaveChanges();
      }
    }

    // Method to get errand details by ID
    public Task<Errand?> GetErrandDetails(int id)
    {
      return Task.Run(() =>
      {
        // Query the Errands DbSet to find the errand with the specified ID
        var errandDetail = Errands.Where(er => er.ErrandId == id).FirstOrDefault();
        return errandDetail;
      });
    }

    // Method to update the department of an errand
    public void UpdateDepartment(int errandId, string choosenDepartment)
    {
      // Find the errand by its ID
      var errand = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errand != null)
      {
        // Update the DepartmentId of the errand
        errand.DepartmentId = choosenDepartment;
      }
      context.SaveChanges();
    }
    // Method to update the employee of an errand
    public void UpdateEmployee(int errandId, Employee employee)
    {
      // Find the errand by its ID
      var errand = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errand != null)
      {
        // Update the EmployeeId of the errand
        errand.EmployeeId = employee.EmployeeId;
      }
      context.SaveChanges();
    }
  }
}

