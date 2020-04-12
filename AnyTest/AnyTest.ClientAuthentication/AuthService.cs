using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http.Headers;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A service class for authenticating with JWT tokens
    /// \~ukrainian Сервісний клас для аутентифікації JWT токенів
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        /// <summary>
        /// \~english A list of valid roles
        /// \~ukrainian Перелік дійснийх ролей
        /// </summary>
        public static readonly List<string> Roles = new List<string>
        {
            "Administrator",
            "Tutor",
            "Student"
        };

        /// <summary>
        /// \~english Initalizes a new instance of <c>AuthService</c> class
        /// \~ukrainian Ініціалізує новий екземпляр класу <c>AuthService</c>
        /// </summary>
        /// <param name="httpClient">
        /// \~english An instance of <c>HttpClient</c> class. Depencency
        /// \~ukrainian Езкемплярр класу <c>HttpClient</c>. Залежність
        /// </param>
        /// <param name="authenticationStateProvider">
        /// \~english An instance of <c>AuthenticationStateProvider</c> class. Depencency
        /// \~ukrainian Езкемплярр класу <c>AuthenticationStateProvider</c>. Залежність
        /// </param>
        /// <param name="localStorage">
        /// \~english A class, implementing an <c>ILocalStorageService</c> interface. Dependency.
        /// \~ukrainian Клас. який наслідує інтерфейсу <c>ILocalStorageSerivce</c>. Залежність.
        /// </param>
        public AuthService(HttpClient httpClient,AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        ///<inheritdoc />
        public async Task<LoginResult> Login(LoginModel creds)
        {
            var loginAsJson = JsonSerializer.Serialize(creds);
            var response = await _httpClient.PostAsync("Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if(response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", loginResult.Token);
                (_authenticationStateProvider as ApiAuthenticaionStateProvider).MarkUserAsAuthenticated(creds.UserName);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            }

            return loginResult;
        }

        ///<inheritdoc />
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            (_authenticationStateProvider as ApiAuthenticaionStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        ///<inheritdoc />
        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostJsonAsync<RegisterResult>("api/accounts", registerModel);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> GetToken()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

            return true;
        }
    }
}
