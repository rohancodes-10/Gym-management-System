namespace Gym_management_System.Models.MembershipPayments
{
    public interface IMembershipPaymentService
    {
        IEnumerable<MembershipPayment> GetAllPayments();
        IEnumerable<MembershipPayment> GetAllPaymentsByGymId(int gymid);
        MembershipPayment GetMembershipPaymentById(int id);
       IEnumerable<MembershipPayment>  GetMembershipPaymentByMemberId(int id);
        MembershipPayment? GetActivePaymentByMemberId(int memberid);
        MembershipPayment AddPayment(MembershipPayment payment);
        void UpdateExpired();
    }
}
