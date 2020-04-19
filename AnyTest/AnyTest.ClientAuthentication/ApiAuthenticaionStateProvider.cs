using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A provider for authentication state in blazor components
    /// \~ukrainian Клас, який постачає стан аутентифікації компотенту blazor
    /// </summary>
    public class ApiAuthenticaionStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        /// <summary>
        /// \~english Initializes a new instance of <c>ApiAuthenticationStateProvider</c> class
        /// \~ukrainian Ініціалізує новий екземпляр класу <c>ApiAuthenticationStateProvider</c>
        /// </summary>
        /// <param name="httpClient">
        /// \~english An instance of <c>HttpClient</c> class. Depencency
        /// \~ukrainian Езкемплярр класу <c>HttpClient</c>. Залежність
        /// </param>
        /// <param name="localStorage">
        /// \~english A class, implementing an <c>ILocalStorageService</c> interface. Dependency.
        /// \~ukrainian Клас. який наслідує інтерфейсу <c>ILocalStorageSerivce</c>. Залежність.
        /// </param>
        public ApiAuthenticaionStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        /// <summary>
        /// \~english Method called by blazor AuthorizeView component to define the authentication state
        /// \~ukrainian Метод, який викликає компонет blazor щоб визначити стан аутентифікації
        /// </summary>
        /// <returns>An <c>AuthenticationState</c> object, containing the jwt token</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //Get a token from a local storage
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");

            if(string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            if(TokenExprired(savedToken))
            {
                await _localStorage.RemoveItemAsync("authToken");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt")));
        }

        /// <summary>
        /// \~english Marks a user as authenticated for an <c>AuthorizeView</c> component
        /// \~ukrainian Позначає користувача аутентивікованим для компонента <c>AuthorizeView</c> 
        /// </summary>
        /// <param name="token">
        /// \~ JWT token
        /// \~ JWT токен
        /// </param>
        public void MarkUserAsAuthenticated(string token)
        {

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// \~english Marks user as logged out for an <c>AuthorizeView</c> component
        /// \~ukrainian Позначає, що коритсувач вийшов для компонента <c>AuthorizeView</c>
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// \~english Defines if current token is expired
        /// \~ukrainian Визначає, чи не сплив термін дії збереженого токену
        /// </summary>
        /// <param name="jwt">
        /// \~english JWT token
        /// \~ukrainian JWT токен
        /// </param>
        /// <returns></returns>
        private bool TokenExprired (string jwt) =>
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(long.Parse(ParseClaimsFromJwt(jwt).FirstOrDefault(c => c.Type == "exp")?.Value)) < DateTime.Now;

        /// <summary>
        ///\~english Parses JWT token into collection of security Claims
        ///\~ukrainian Парсить JWT токен у колекцію Клеймів безпеки
        /// </summary>
        /// <param name="jwt">
        /// \~english JWT token
        /// \~ukrainian JWT токет
        /// </param>
        /// <returns>
        /// \~english A collection of Claimes, containint data from JWT token
        /// \~ukrainian Колекція Клеймів, які містять дані з JWT токена
        /// </returns>
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];//Gets paload from jwt token
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize <Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if(roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach(var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        /// <summary>
        /// \~english Generates a base 64 byte sequence from the JWT token payload
        /// \~ukrainian Генерує послідовність байтів із базою 64 біта із даних JWT токена
        /// </summary>
        /// <param name="base64">JWT tokey payload string</param>
        /// <returns>byte sequence</returns>
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch(base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
