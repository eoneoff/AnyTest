using AnyTest.Model;
using AnyTest.Model.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface ITestRepository : IRepository<Test>
    {
        /// <summary>
        /// \~english A method to gett a structured list of subjects, courses and tests
        /// \~ukrainian Метод для отримання структурованого списку предметів, курсів та тестів
        /// </summary>
        /// <returns>
        /// \~english A list of subjects, courses and tests, organized as a tree
        /// \~ukrainian Список предметів, курсів та тестів деревоподібної структури
        /// </returns>
        public Task<Dictionary<string, List<TestsTreeModel>>> GetTestsList();
        public Task<TestSubject> AddSubject(long testId, long subjectId);
        public Task<TestSubject> RemoveSubject(long testId, long subjectId);
        public Task<TestCourse> AddCourse(long testId, long courseId);
        public Task<TestCourse> RemoveCourse(long testId, long courseId);
    }
}
