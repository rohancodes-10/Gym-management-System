using Gym_management_System.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }
        public string UserName {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
