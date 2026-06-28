namespace Gym_management_System.Models.MembershipPayments
{
    public interface IMembershipPaymentService
    {
        IEnumerable<MembershipPayment> GetAllPayments();
        IEnumerable<MembershipPayment> GetAllPaymentsByGymId(int gymid);
        MembershipPayment GetMembershipPaymentById(int id);
       IEnumerable<MembershipPayment>  GetMembershipPaymentByMemberId(int id);
        IEnumerable<MembershipPayment> GetActivemembersByGymId(int gymId);
        IEnumerable<MembershipPayment> GetInActivemembersByGymId(int gymId);
        MembershipPayment? GetActivePaymentByMemberId(int memberid);
        Decimal GetRevenue();
        MembershipPayment AddPayment(MembershipPayment payment);
        void Delete(int id);
        void UpdateExpired();
    }
}
