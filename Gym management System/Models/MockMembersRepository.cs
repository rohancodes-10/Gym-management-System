namespace Gym_management_System.Models
{
    public class MockMembersRepository: IMemberService
    {
        private List<Members> _membersList;
        public MockMembersRepository()
        {
            _membersList = new List<Members>()
            {
                new Members{Id=1, Address="changunarayan-8-Bhaktapur",MemberName="Ram",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                 new Members{Id=2, Address="changunarayan-8-Bhaktapur",MemberName="ham",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                  new Members{Id=3, Address="changunarayan-8-Bhaktapur",MemberName="shyam",Phone="9876543210",Gender="Male",city="Bhaktapur"},
                   new Members{Id=4, Address="changunarayan-8-Bhaktapur",MemberName="gitam",Phone="9876543210",Gender="Male",city="Bhaktapur"}
            };
        }
        public IEnumerable<Members> GetAllMembers()
        {
            return (_membersList);
        }
        public Members ? GetMembers(int id)
        {
            return _membersList.FirstOrDefault(e => e.Id == id);
        }
    }
}
