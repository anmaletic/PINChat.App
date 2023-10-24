using System.ComponentModel.DataAnnotations;

namespace PINChat.App.Library.Models;

public class AuthenticationUserModel
{
    [Required(ErrorMessage = "Nije upisano korisničko ime!")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Nije upisana lozinka!")]
    public string? Password { get; set; }
}