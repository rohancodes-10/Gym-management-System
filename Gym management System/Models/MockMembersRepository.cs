namespace Gym_management_System.Models
{
    public class MockMembersRepository 
    {
        private List<Member> _membersList;
        public MockMembersRepository()
        {
            _membersList = new List<Member>()
            {
                new Member{Id=1, Address="changunarayan-8-Bhaktapur",MemberName="Ram",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                 new Member{Id=2, Address="changunarayan-8-Bhaktapur",MemberName="ham",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                  new Member{Id=3, Address="changunarayan-8-Bhaktapur",MemberName="shyam",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                   new Member{Id=4, Address="changunarayan-8-Bhaktapur",MemberName="gitam",Phone="9876543210",Gender="Male",city="Bhaktapur"}
            };
        }
        public IEnumerable<Member> GetAllMembers()
        {
            return (_membersList);
        }
        public Member ? GetMembers(int id)
        {
            return _membersList.FirstOrDefault(e => e.Id == id);
        }
    }
}
