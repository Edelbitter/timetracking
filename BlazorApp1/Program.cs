using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("http://localhost:7119/") });
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}api/")});

await builder.Build().RunAsync();