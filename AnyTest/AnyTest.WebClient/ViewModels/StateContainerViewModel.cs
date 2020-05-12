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
using System.Collections;
using AnyTest.Model.ApiModels;

namespace AnyTest.WebClient.ViewModels
{
    public class StateContainerViewModel
    {
        private HttpClient _httpClient;

        public StateContainerViewModel(HttpClient httpClient) => _httpClient = httpClient;

        public Person Person { get; private set; } = new Person();
        public List<UserInfo> Users { get; private set; } = new List<UserInfo>();
        public List<Student> Students { get; private set; } = new List<Student>();

        public Dictionary<string, List<TestsTreeModel>> TestsTreeList { get; private set; } = new Dictionary<string, List<TestsTreeModel>>();

        public event EventHandler TestsUpdated;

        public IEnumerable<Subject> Subjects =>
            TestsTreeList.ContainsKey("subjects") ? TestsTreeList["subjects"].Select(tm => new Subject { Id = tm.Id, Name = tm.Name }) : new List<Subject>();

        public IEnumerable<Course> Courses =>
            TestsTreeList.ContainsKey("courses") ? TestsTreeList["courses"].Select(tm => new Course { Id = tm.Id, Name = tm.Name }) : new List<Course>();

        public IEnumerable<Test> Tests =>
            TestsTreeList.ContainsKey("tests") ? TestsTreeList["tests"].Select(tm => new Test { Id = tm.Id, Name = tm.Name }) : new List<Test>();

        public Person LoadingStub = new Person
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
                if(roles?.Contains("Tutor") ?? false) await SaveTutor(new Tutor());
                if(roles?.Contains("Student") ?? false) await SaveStudent(new Student());
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

        public async Task GetTestsList()
        {
            TestsTreeList = await _httpClient.GetJsonAsync<Dictionary<string, List<TestsTreeModel>>>("tests/list");
            TestsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveSubject(Subject subject)
        {
            if(subject.Id == 0)
            {
                await _httpClient.PostJsonAsync("subjects", subject);
            }
            else
            {
                await _httpClient.PutJsonAsync($"subjects/{subject.Id}", subject);
            }

            await GetTestsList();
        }

        public async Task SaveCourse(Course course)
        {
            if(course.Id == 0)
            {
                await _httpClient.PostJsonAsync("courses", course);
            }
            else
            {
                await _httpClient.PutJsonAsync($"courses/{course.Id}", course);
            }

            await GetTestsList();
        }

        public async Task SaveTest(Test test)
        {
            if(test.Id == 0)
            {
                if(Person.Id == 0) await GetPersonByAuthorizedUser();
                test.AuthorId = Person.Id;
                await _httpClient.PostJsonAsync("tests", test);
            }
            else
            {
                await _httpClient.PutJsonAsync($"tests/{test.Id}", test);
            }

            await GetTestsList();
        }

        public async Task GetStudents()
        {
            Students = await _httpClient.GetJsonAsync<List<Student>>("students");
        }
    }
}
