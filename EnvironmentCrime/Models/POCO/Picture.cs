using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models.POCO
{
  public class Picture
  {
    public int PictureId { get; set; }
    public string? PictureName { get; set; }
    public int ErrandId { get; set; }
  }
}
