global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
global using MudBlazor.Services;
global using MudBlazor;
global using WASM_JUDOPLAT;
using Blazored.LocalStorage;
using JUDOPLAT.WASM_JUDOPLAT.Provider;
using System.IdentityModel.Tokens.Jwt;
using Tewr.Blazor.FileReader;
using Microsoft.AspNetCore.Http;
using Toolbelt.Blazor.Extensions.DependencyInjection;
namespace WASM_JUDOPLAT;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBlazoredLocalStorage();
        //builder.Services.AddScoped<MemoryStorageUtility>(); //Memory storage
        builder.Services.AddSpeechSynthesis();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 2000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
  builder.Services.AddScoped<BlazorTransitionableRoute.IRouteTransitionInvoker, BlazorTransitionableRoute.DefaultRouteTransitionInvoker>();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<DialogService>();
        builder.Services.AddFileReaderService();
        builder.Services.AddScoped<ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<ApiAuthenticationStateProvider>());
        builder.Services.AddScoped<JwtSecurityTokenHandler>();
        builder.Services.AddTransient<IPdfRepo, PdfRepo>();

        await builder.Build().RunAsync();
    }
}

