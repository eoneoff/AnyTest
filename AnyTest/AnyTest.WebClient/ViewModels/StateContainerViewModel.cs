using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyTest.WebClient.ViewModels
{
    public class StateContainerViewModel
    {
        private HttpClient _httpClient;

        public StateContainerViewModel(HttpClient httpClient) => _httpClient = httpClient;

        public Person Person { get; set; }

        public async Task GetPersonByAuthorizedUser()
        {
            var response = await _httpClient.GetAsync("Accounts/Person");
            Person person = new Person();
            try
            {
                person = JsonSerializer.Deserialize<Person>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Person = person;
            }
        }
    }
}
