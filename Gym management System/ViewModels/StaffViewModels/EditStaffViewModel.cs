namespace Gym_management_System.ViewModels.StaffViewModels
{
    public class EditStaffViewModel:CreateStaffViewModel
    {
        public int id { get; set; }
        public string? ExistingPhotoUrl { get; set; }
    }
}
