namespace Gym_management_System.Models.MembershipPayments
{
    public interface IMembershipPaymentService
    {
        IEnumerable<MembershipPayment> GetAllPayments();
       MembershipPayment GetMembershipPaymentById(int id);
       IEnumerable<MembershipPayment>  GetMembershipPaymentByMemberId(int id);
        MembershipPayment AddPayment(MembershipPayment payment);
    }
}
