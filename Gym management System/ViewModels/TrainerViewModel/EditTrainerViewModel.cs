namespace Gym_management_System.ViewModels.TrainerViewModel
{
    public class EditTrainerViewModel:AddTrainerViewModel
    {
        public int id { get; set; }
        public string? ExistingPhotoUrl { get; set; }
    }
}
