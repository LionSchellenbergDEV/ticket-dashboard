using System.ComponentModel.DataAnnotations;

namespace ticket_dashboard.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Der Benutzername ist erforderlich.")]
        [Display(Name = "Benutzername")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Das Passwort ist erforderlich.")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }
    }
}
