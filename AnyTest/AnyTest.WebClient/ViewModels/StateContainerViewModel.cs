using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AnyTest.Infrastructure;

namespace AnyTest.WebClient.ViewModels
{
    public class StateContainerViewModel
    {
        private HttpClient _httpClient;

        public StateContainerViewModel(HttpClient httpClient) => _httpClient = httpClient;

        public Person Person { get; private set; } = new Person();

        private Person LoadingStub = new Person
        {
            FirstName = "Loading...",
            FamilyName = "Loading...",
            Patronimic = "Loading...",
            Phone = "Loading...",
            Email = "Loading.."
        };

        public void ResetPerson() => Person = new Person();

        public async Task GetPersonByAuthorizedUser()
        {
            Person = LoadingStub;
            Person person = new Person();
            try
            {
                person = await _httpClient.GetJsonAsync<Person>("Accounts/Person");
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

        public Person GetCopyOfPerson()
        {
            return new Person
            {
                Id = Person.Id,
                FirstName = Person.FirstName,
                FamilyName = Person.FamilyName,
                Patronimic = Person.Patronimic,
                Phone = Person.Phone,
                Email = Person.Email
            };
        }

        /// <summary>
        /// \~english Saves a <c>Person</c> to database
        /// \~ukrainian Зберігає <c>Person</c> до бази даних
        /// </summary>
        /// <param name="person">
        /// \~english A <c>Person</c> object
        /// \~ukrainian Об'єкт <c>Person</c>
        /// </param>
        public async Task SavePerson(Person person) =>
            Person =  person.Id == 0 ? await  _httpClient.PostAsJsonAsync<Person, Person>("people", person) : await _httpClient.PutAsJsonAsync<Person, Person>($"people/{person.Id}", person);
    }
}
