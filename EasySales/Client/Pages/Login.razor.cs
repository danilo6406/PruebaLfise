using EasySales.Client.Services;
using EasySales.Shared;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EasySales.Client.Pages
{
    public partial class Login
    {
        private LoginModel loginModel = new LoginModel();

        [Inject]
        public IAuthService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }


        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(loginModel);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
