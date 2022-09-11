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

builder.Services.AddHttpClient<ITodoService, TodoService>(client => { client.BaseAddress = new Uri("https://localhost:1989/"); });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
