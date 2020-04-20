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
using AnyTest.Model.Api;

namespace AnyTest.WebClient.ViewModels
{
    public class StateContainerViewModel
    {
        private HttpClient _httpClient;

        public StateContainerViewModel(HttpClient httpClient) => _httpClient = httpClient;

        public Person Person { get; private set; } = new Person();
        public List<UserInfo> Users { get; private set; } = new List<UserInfo>();
        public List<Subject> Subjects { get; private set; } = new List<Subject>();
        public List<Course> Courses { get; private set; } = new List<Course>();
        public List<Test> Tests { get; private set; } = new List<Test>();

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

        public async Task GetTests()
        {
            GetDummyTests();
        }

        private void GetDummyTests()
        {
            var testNames = new Dictionary<string, List<string>>
            {
                { "Математика", new List<string>
                {
                    "Алгебра",
                    "Математичний аналіз"
                } },
                {"Розробка програмного забезпечення", new List<string>
                {
                    "C#",
                    "Python"
                } },
                {"Фізика", new List<string>
                {
                    "Електромагнетизм",
                    "Ядерна фізика"
                } },
                {"-", new List<string>
                {
                    "Конституційне право"
                } }
            };

            Subjects = new List<Subject>();
            Courses = new List<Course>();
            Tests = new List<Test>();
            long subjectId = 1;
            long courseId = 1;
            long testId = 1;
            var rnd = new Random();

            foreach(var subj in testNames)
            {
                Subject subject = null;

                if(subj.Key != "-")
                {
                    subject = new Subject
                    {
                        Id = subjectId++,
                        Name = subj.Key,
                        Courses = new List<Course>(),
                        Tests = new List<TestSubject>()
                    };

                    Subjects.Add(subject);

                    AddTestsToSubject(subject, null, rnd, ref testId);
                }

                foreach(var crs in subj.Value)
                {
                    var course = new Course
                    {
                        Id = courseId++,
                        Name = crs,
                        SubjectId = subject?.Id,
                        Tests = new List<TestCourse>()
                    };

                    if (subject != null) subject.Courses.Add(course);
                    Courses.Add(course);
                    AddTestsToSubject(subject, course, rnd, ref testId);
                }
            }

            AddTestsToSubject(null, null, rnd, ref testId);
        }

        private void AddTestsToSubject(Subject subject, Course course, Random rnd, ref long testId )
        {
            int count = rnd.Next() % 3;

            if (course != null) count++;

            for (int i = 0; i < count; i++, testId++)
            {
                var test = new Test
                {
                    Id = testId,
                    Name = $"Test {testId}",
                    Subjects = new List<TestSubject>(),
                    Courses = new List<TestCourse>()
                };

                if(subject != null)
                {
                    var testSubject = new TestSubject { TestId = testId, SubjectId = subject.Id, Test = test };
                    test.Subjects.Add(testSubject);
                    subject.Tests.Add(testSubject);
                }

                if(course != null)
                {
                    var testCourse = new TestCourse { TestId = testId, CourseId = course.Id, Test = test };
                    test.Courses.Add(testCourse);
                    course.Tests.Add(testCourse);
                }

                Tests.Add(test);
            }
        }
    }
}
