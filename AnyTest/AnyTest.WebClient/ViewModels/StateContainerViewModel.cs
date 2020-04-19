using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AnyTest.ClientAuthentication;
using System.Net;

namespace AnyTest.WebClient.ViewModels
{
    public class StateContainerViewModel
    {
        private HttpClient _httpClient;

        public StateContainerViewModel(HttpClient httpClient) => _httpClient = httpClient;

        public Person Person { get; private set; } = new Person();
        public List<UserInfo> Users = new List<UserInfo>();

        private Person LoadingStub = new Person
        {
            FirstName = "Loading...",
            FamilyName = "Loading...",
            Patronimic = "Loading...",
            Phone = "Loading...",
            Email = "Loading..."
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
            catch (HttpRequestException ex)
            {
                if(ex.InnerException is WebException {Status:WebExceptionStatus.ProtocolError, Response:HttpWebResponse {StatusCode: HttpStatusCode.Unauthorized } } ) throw;
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
        /// \~english Saves a <c>Person</c> to database. Creates <c>Tutor</c> or <c>Student</c> if user is tutor or student
        /// \~ukrainian Зберігає <c>Person</c> до бази даних. Створює викладача або студета якщо користувач є викладачем або студентом
        /// </summary>
        /// <param name="person">
        /// \~english A <c>Person</c> object
        /// \~ukrainian Об'єкт <c>Person</c>
        /// </param>
        /// <param name="roles">
        /// \~english Contains a list of person's roles
        /// \~ukrainian Список ролей користувача
        /// <param>
        public async Task SavePerson(Person person, IEnumerable<string> roles = null)
        {
            if(person.Id == 0)
            {
                Person = await _httpClient.PostJsonAsync<Person>("people", person);
                roles ??= new List<string>();
                if(roles.Contains("Tutor")) await SaveTutor(new Tutor());
                if(roles.Contains("Student")) await SaveStudent(new Student());
            }

            else Person = await _httpClient.PutJsonAsync<Person>($"people/{person.Id}", person);
        }

            

        /// <summary>
        /// \~english Gets a complete list of users from server
        /// \~ukrainian Отримує з сервера повний список користувачів
        /// </summary>
        public async Task GetUsers()
        {
            Users = await _httpClient.GetJsonAsync<List<UserInfo>>("accounts/users");
        }

        /// <summary>
        /// \~english Saves a <c>Tutor</c> to database
        /// \~ukrainian Зберігає <c>Tutor</c> до бази даних
        /// </summary>
        /// <param name="tutor">
        /// \~english A <c>Tutor</c> object
        /// \~ukrainian Об'єкт <cTutor</c>
        /// </param>
        public async Task SaveTutor(Tutor tutor)
        {
            if (tutor.Id == 0)
            {
                tutor.Id = Person.Id;
                Person.Tutor = await _httpClient.PostJsonAsync<Tutor>("tutors", tutor);
            }
            else Person.Tutor = await _httpClient.PutJsonAsync<Tutor>("tutors", tutor);
        }

        /// <summary>
        /// \~english Saves a <c>Student</c> to database
        /// \~ukrainian Зберігає <c>Student</c> до бази даних
        /// </summary>
        /// <param name="student">
        /// \~english A <c>Student</c> object
        /// \~ukrainian Об'єкт <c>Student</c>
        /// </param>
        public async Task SaveStudent(Student student)
        {
            if (student.Id == 0)
            {
                student.Id = Person.Id;
                Person.Student = await _httpClient.PostJsonAsync<Student>("students", student);
            }
            else Person.Student = await _httpClient.PutJsonAsync<Student>("students", student);
        }

        private List<UserInfo> GenerateStubUsers(int count = 18)
        {
            var result = new List<UserInfo> { new UserInfo { User = "user1", Email = "email1@email.com", Name = "name1", Roles = new List<string> { "Administrator", "Tutor" } } };
            var random = new Random();
            for(int i = 2; i<=count;i++)
            {
                var userInfo = new UserInfo
                {
                    User = $"user{i}",
                    Email = $"email{1}@email.com",
                    Name = $"name{i}"
                };
                var roles = new List<string>();
                if (random.Next() % 5 == 0)
                {
                    if (random.Next() % 10 == 0) roles.Add("Administrator");
                    roles.Add("Tutor");
                }
                else
                    roles.Add("Student");
                userInfo.Roles = roles;
                result.Add(userInfo);
            }
            return result;
        }
    }
}
