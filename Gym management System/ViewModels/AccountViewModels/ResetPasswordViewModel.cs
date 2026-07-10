using Gym_management_System.Models.Users;

namespace Gym_management_System.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        public int UserId { get; set; }
        public string UserName {  get; set; }


        public string NewPasswordHash { get; set; }
        public string ConfirmPasswordHash { get; set; }
    }
}
