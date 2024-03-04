using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Handlers;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7047"))
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

//? servi�o de armazenamento no navegador
builder.Services.AddBlazoredLocalStorage();

#region autoriza��o e aut�ntica��o

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

#endregion

//? registra os servi�os
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

//? adiciona o automaper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
