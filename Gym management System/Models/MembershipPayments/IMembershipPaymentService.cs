namespace Gym_management_System.Models.MembershipPayments
{
    public interface IMembershipPaymentService
    {
       MembershipPayment GetMembershipPaymentById(int id);
       IEnumerable<MembershipPayment>  GetMembershipPaymentByMemberId(int id);
        MembershipPayment AddPayment(int memberId,int planId);
    }
}
