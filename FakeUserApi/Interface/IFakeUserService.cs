using FakeUserApi.Models;

namespace FakeUserApi.Interface
{
    public  interface IFakeUserService
    {
       public AuthenticateUser Authenticate(AuthenticateCommand model);
    }
}