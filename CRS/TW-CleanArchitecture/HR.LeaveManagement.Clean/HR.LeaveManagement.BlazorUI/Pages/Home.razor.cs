using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Pages;
public partial class Home
{
    [Inject]
    private AuthenticationStateProvider ApiAuthenticationStateProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    //? ao inicializar a aplicação aguarda a requisição para o serviço de autenticação
    protected async override Task OnInitializedAsync()
    {
        //? respectivamente se está autenticado ou não
        await ((ApiAuthenticationStateProvider)
            ApiAuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    protected async void Logout()
    {
        await AuthenticationService.Logout();
    }

    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("Login/");
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }
}