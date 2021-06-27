
namespace FakeUserApi.Models
{
    public class FakeUserView
    {
        private readonly FakeUser _user;
        public long Id => _user.Id;
        public string Name => _user.Name;
        public string Lastname => _user.Lastname;
        public string Login => _user.Login;
        public string Email => _user.Email;

        public FakeUserView(FakeUser user)
        {
            _user = user;
        }
    }
}

