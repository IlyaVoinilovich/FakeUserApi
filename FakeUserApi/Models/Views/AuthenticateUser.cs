namespace FakeUserApi.Models
{
    public class AuthenticateUser
    {
        private readonly FakeUser _user;
        public long Id => _user.Id;
        public string Name => _user.Name;
        public string Lastname => _user.Lastname;
        public string Login => _user.Login;
        public string Email => _user.Email;
        public string Token { get;}

        public AuthenticateUser(FakeUser user, string token)
        {
            _user = user;
            Token = token;
        }
    }
}
