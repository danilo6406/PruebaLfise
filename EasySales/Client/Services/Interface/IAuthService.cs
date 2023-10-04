using EasySales.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasySales.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();

        Task LoginCookie(string name);
        //Task<RegisterResult> Register(RegisterModel registerModel);
    }
}