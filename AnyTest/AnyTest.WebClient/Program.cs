using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AnyTest.ClientAuthentication;
using AnyTest.ResourceLibrary;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace AnyTest.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticaionStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddLocalization();
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("uk")
            };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("uk");
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
            });

            await builder.Build().RunAsync();
        }
    }
}
