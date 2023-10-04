
using EasySales.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EasySales.Client.Pages
{
    public partial class Logout
    {

        [Inject]
        public IAuthService authService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
