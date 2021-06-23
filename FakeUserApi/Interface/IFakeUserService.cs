using FakeUserApi.Models;

namespace FakeUserApi.Interface
{
    public  interface IFakeUserService
    {
       public AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}