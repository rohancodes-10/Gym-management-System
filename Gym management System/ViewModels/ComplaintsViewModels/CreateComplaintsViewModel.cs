using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.ComplaintsViewModels
{
    public class CreateComplaintsViewModel
    {
       public int GymId {  get; set; }


        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100, ErrorMessage = "Subject cannot exceed 100 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is Required")]
        [StringLength(100, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Message { get; set; }
    }
}
