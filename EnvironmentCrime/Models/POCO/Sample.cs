using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models.POCO
{
  public class Sample
  {
    public int SampleId { get; set; }
    public string? SampleName { get; set; }
    public int ErrandId { get; set; }
  }
}
