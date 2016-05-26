namespace FunctionalProgramming
{
    public class UserProfile //state change
    {
        private User _user;
        private string _address;
        private User newUser;

        public UserProfile(User user, string address)
        {
            _user = user;
            _address = address;
        }

        public UserProfile UpdateUser(int userId, string name)
        {
            var newUser = new User(userId, name);
            return new UserProfile(newUser, _address);
        }
    }

    public class User // stateless
    {
        public int Id { get; }
        public string Name { get; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
