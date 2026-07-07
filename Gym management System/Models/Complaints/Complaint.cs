namespace Gym_management_System.Models.Complaints
{
    public class Complaint
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public int SubmittedById { get; set; }
        public string SubmittedByName { get; set; }
        public string SubmittedByRole { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public string? OwnerResponse { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
