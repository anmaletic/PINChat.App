using System.ComponentModel.DataAnnotations;

namespace PINChat.App.Library.Models;

public class RegistrationModel
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
    
    [Required] [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
    public string? ConfirmPassword { get; set; }


    public void Reset()
    {
        UserName = null;
        Password = null;
        ConfirmPassword = null;
    }
}