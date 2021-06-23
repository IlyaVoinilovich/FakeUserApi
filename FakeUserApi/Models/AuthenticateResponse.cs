namespace FakeUserApi.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(FakeUser user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Lastname = user.Lastname;
            Login = user.Login;
            Email = user.Email;
            Token = token;
        }
    }
}
