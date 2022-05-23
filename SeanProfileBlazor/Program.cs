global using Microsoft.AspNetCore.Components.Authorization;
global using SeanProfileBlazor.Services;
global using SeanProfileBlazor.Models;
using Blazored.LocalStorage;


using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SeanProfileBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:1989/") });

builder.Services.AddHttpClient<ITodoDataService, TodoService>(client => { client.BaseAddress = new Uri("https://localhost:1989/"); });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
