using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Vänligen fyll i ett användarnamn.")]
    [Display(Name = "Användarnamn:")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Vänligen fyll i ett lösenord.")]
    [Display(Name = "Lösenord:")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
  }
}