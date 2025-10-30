using Microsoft.EntityFrameworkCore;

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

    public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

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
      return Task.Run(() => Errands.FirstOrDefault(e => e.ErrandId == id));
    }

    // Method to update the department of an errand
    public void UpdateDepartment(int errandId, string choosenDepartment)
    {
      // Find the errand by its ID
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errandDb != null)
      {
        // Update the DepartmentId of the errand
        errandDb.DepartmentId = choosenDepartment;
      }
      context.SaveChanges();
    }
    // Method to update the employee of an errand
    public void UpdateEmployee(int errandId, Employee employee)
    {
      // Find the errand by its ID
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);

      if (errandDb != null)
      {
        // Update the EmployeeId of the errand
        errandDb.EmployeeId = employee.EmployeeId;
        errandDb.StatusId = "S_A";
      }
      context.SaveChanges();
    }

    // Method to update investigator info of an errand - by manager
    public void UpdateInvestigatorInfo(int errandId, string investigatorInfo)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the InvestigatorInfo of the errand
        errandDb.InvestigatorInfo = errandDb.InvestigatorInfo + "\n" + investigatorInfo;
        errandDb.StatusId = "S_B";
        errandDb.EmployeeId = "";
      }
      context.SaveChanges();
    }
    // Method to add investigator info for an errand - by investigator
    public void AddInvestigatorInfo(int errandId, string investigatorInfo)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Set the InvestigatorInfo of the errandDb
        errandDb.InvestigatorInfo = errandDb.InvestigatorInfo + "\n" + investigatorInfo;
      }
      context.SaveChanges();
    }

    // Metod to change status for an errand - by investigator
    public void UpdateErrandStatus(int errandId, string statusId)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the StatusId of the errand
        errandDb.StatusId = statusId;
      }
      context.SaveChanges();
    }

    // Method to create an investigator event for an errand - by investigator
    public void CreateInvestigatorEvent(int errandId, string investigatorAction)
    {
      var errandDb = Errands.FirstOrDefault(er => er.ErrandId == errandId);
      if (errandDb != null)
      {
        // Update the InvestigatorAction of the errand
        errandDb.InvestigatorAction = errandDb.InvestigatorAction + "\n" + investigatorAction;
      }
      context.SaveChanges();
    }
    // Method to insert a file (sample or picture) associated with an errand - by investigator
    public async Task InsertFileAsync(string dirPath, int errandId, string uniqueFileName)
    {
      if (dirPath == "Samples")
      {
        var sample = new Sample
        {
          ErrandId = errandId,
          SampleName = uniqueFileName
        };
        await context.Samples.AddAsync(sample);
      }
      if (dirPath == "Pictures")
      {
        var picture = new Picture
        {
          ErrandId = errandId,
          PictureName = uniqueFileName
        };
        await context.Pictures.AddAsync(picture);
      }
      await context.SaveChangesAsync();
    }
  }
}

