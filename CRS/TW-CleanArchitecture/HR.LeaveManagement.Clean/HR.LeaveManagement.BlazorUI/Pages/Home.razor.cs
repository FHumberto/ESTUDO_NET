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

    //? ao inicializar a aplica��o aguarda a requisi��o para o servi�o de autentica��o
    protected async override Task OnInitializedAsync()
    {
        //? respectivamente se est� autenticado ou n�o
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