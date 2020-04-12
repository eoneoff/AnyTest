using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AnyTest.Infrastructure
{
    /// <summary>
    /// \~english A class with extension methods for <c>HttpClient</c> class
    /// \~ukrainian Класс з методами розширення для классу <c>HttpClient</c>
    /// </summary>
    public static class HttpClientHelpers
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string requestUri)
        {
            var response = await client.GetAsync(requestUri);
            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode} {response.ReasonPhrase}");
            }

            var result = JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        /// <summary>
        /// \~english Extension metod to serialize an deserialize POST requests
        /// \~ukrainian Метод розширення, який серіалізує та десеріалізує POST запити
        /// </summary>
        /// <typeparam name="T">
        /// \~english A type of objext sent.
        /// \~ukrainian Тип об'єкту, який відправляється.
        /// </typeparam>
        /// <typeparam name="U">
        /// \~english A type of object received. Must be explicitely defined
        /// \~ukrainian Тип отримуваного об'єкту. Має бути явно визначений
        /// </typeparam>
        /// <param name="client">
        /// \~english <c>HttpClient</c> object. Method caller
        /// \~ukrainian Об'єкт <c>HttpClient</c>, який викликає метод
        /// </param>
        /// <param name="requestUri">
        /// \~english Reques URI
        /// \~ukrainian URI запиту
        /// </param>
        /// <param name="item">
        /// \~english The request payload
        /// \~ukrainian Тіло запиту
        /// </param>
        /// <returns>
        /// \~english A deseiralized object of <c>U</c> type
        /// \~ukrainian Десеріалізований об'єкт типу <c>U</c>
        /// </returns>
        public static async Task<U> PostAsJsonAsync<T, U> (this HttpClient client, string requestUri, T item)
        {
            var itemAsJson = JsonSerializer.Serialize(item);
            var response = await client.PostAsync(requestUri, new StringContent(itemAsJson, Encoding.UTF8, "application/json"));

            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode} {response.ReasonPhrase}");
            }

            var result = JsonSerializer.Deserialize<U>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        /// <summary>
        /// \~english Extension metod to serialize an deserialize PUT requests
        /// \~ukrainian Метод розширення, який серіалізує та десеріалізує PUT запити
        /// </summary>
        /// <typeparam name="T">
        /// \~english A type of objext sent.
        /// \~ukrainian Тип об'єкту, який відправляється.
        /// </typeparam>
        /// <typeparam name="U">
        /// \~english A type of object received. Must be explicitely defined
        /// \~ukrainian Тип отримуваного об'єкту. Має бути явно визначений
        /// </typeparam>
        /// <param name="client">
        /// \~english <c>HttpClient</c> object. Method caller
        /// \~ukrainian Об'єкт <c>HttpClient</c>, який викликає метод
        /// </param>
        /// <param name="requestUri">
        /// \~english Reques URI
        /// \~ukrainian URI запиту
        /// </param>
        /// <param name="item">
        /// \~english The request payload
        /// \~ukrainian Тіло запиту
        /// </param>
        /// <returns>
        /// \~english A deseiralized object of <c>U</c> type
        /// \~ukrainian Десеріалізований об'єкт типу <c>U</c>
        /// </returns>
        public static async Task<U> PutAsJsonAsync<T, U>(this HttpClient client, string requestUri, T item)
        {
            var itemAsJson = JsonSerializer.Serialize(item);
            var response = await client.PutAsync(requestUri, new StringContent(itemAsJson, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{response.StatusCode} {response.ReasonPhrase}");
            }

            var result = JsonSerializer.Deserialize<U>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
