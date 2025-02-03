using ShopAPI.AuthResult;
using ShopAPI.Models;

namespace ShopAPI.Service
{
    public interface IAuth
    {
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<LoginResult> SignIn(LoginModel loginModel);

    }
}
