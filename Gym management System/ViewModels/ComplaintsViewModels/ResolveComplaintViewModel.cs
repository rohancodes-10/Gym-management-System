using System.ComponentModel.DataAnnotations;

namespace Gym_management_System.ViewModels.ComplaintsViewModels
{
    public class ResolveComplaintViewModel
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public int SubmittedById { get; set; }
        public string? SubmittedByName { get; set; }
        public string ?SubmittedByRole { get; set; }
        public string ?Subject { get; set; }
        public string ?Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please write a response before resolving")]
        [StringLength(1000)]
        public string OwnerResponse { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
