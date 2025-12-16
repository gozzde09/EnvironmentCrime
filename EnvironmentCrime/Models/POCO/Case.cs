using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{
  // A list of attributes that does not exist in the database; it is a helper class used to pass attributes to the view.
  [Keyless]
  public class Case
  {   
    public string? EmployeeName { get; set; }
    public string? DepartmentName { get; set; }
    public string? RefNumber { get; set; }
    public int ErrandId { get; set; }
    public string? EmployeeId { get; set; }
    public string? StatusName { get; set; }
    public string? TypeOfCrime { get; set; }
    public DateTime DateOfObservation { get; set; }
  }
}
