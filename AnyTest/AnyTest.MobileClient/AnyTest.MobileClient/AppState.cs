using AnyTest.ClientAuthentication;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyTest.MobileClient
{
    public static class AppState
    {
        private static readonly string tokenKey = "authToken";

        public static HttpClient HttpClient = new HttpClient(
            new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            }, false
            ) { BaseAddress = new Uri(CrossSettings.Current.GetValueOrDefault("baseAddress", "https://192.168.0.115:44358/api/")) };

        public static bool IsLoggedIn =>
            App.Current.Properties.ContainsKey(tokenKey)
            && App.Current.Properties[tokenKey] is string token
            && !string.IsNullOrWhiteSpace(token)
            && !ApiAuthenticaionStateProvider.TokenExpired(token);
        public static async Task<LoginResult> Login(LoginModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model as AnyTest.ClientAuthentication.LoginModel);
            var response = await HttpClient.PostAsync("Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if(response.IsSuccessStatusCode)
            {
                App.Current.Properties[tokenKey] = loginResult.Token;
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            }

            return loginResult;
        }

        public static void Logout() => App.Current.Properties.Remove(tokenKey);
    }
}
