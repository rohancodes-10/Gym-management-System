using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
