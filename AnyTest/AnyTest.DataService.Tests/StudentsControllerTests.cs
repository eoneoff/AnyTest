using AnyTest.DataService.Controllers;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AnyTest.DataService.Tests
{
    [TestClass]
    public class StudentsControllerTests
    {
        private Mock<IStudentsRepository> repository = new Mock<IStudentsRepository>();
        private Mock<IPersonRepository> people = new Mock<IPersonRepository>();
        private StudentsController controller;

        public StudentsControllerTests()
        {
            controller = new StudentsController(repository.Object, people.Object);
        }

        [TestMethod]
        public void GetReturnsListOfStudents()
        {
            //Arrange
            repository.Setup(r => r.Get()).ReturnsAsync(new List<Student>() as IEnumerable<Student>);

            //Act
            var result = controller.Get().Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Student>));
        }

        [TestMethod]
        public void GetIdReturnsStudent()
        {
            //Arrange
            repository.Setup(r => r.Get(It.IsAny<object[]>())).ReturnsAsync((object[] id) =>
            {
                return new Student
                {
                    Id = (long)id[0]
                };
            });
            long studentId = 1;

            //Act
            var result = controller.Get(studentId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Student));
            Assert.AreEqual(studentId, result?.Id);
        }

        [TestMethod]
        public void PostReturnsStudent()
        {
            //Arrange
            repository.Setup(r => r.Post(It.IsAny<Student>())).ReturnsAsync((Student s) => { return s; });

            //Act
            var result = (controller.Post(new Student()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Student));
        }

        [TestMethod]
        public void PutReturnsCaseHistory()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            controller.ControllerContext = new ControllerContext { HttpContext = context.Object };

            var identity = new Mock<ClaimsIdentity>();
            context.SetupGet(c => c.User.Identity).Returns(identity.Object);

            string email = "test@test.com";
            identity.Setup(i => i.FindFirst(ClaimTypes.Email)).Returns(new Claim("email", email));
            people.Setup(p => p.Get(It.IsAny<long>())).ReturnsAsync(new Person { Email = email });

            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            repository.Setup(r => r.Put(It.IsAny<Student>(), It.IsAny<object[]>())).ReturnsAsync((Student s, object[] key) => { return s; });

            //Act
            var result = (controller.Put(1, new Student()).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Student));
        }

        [TestMethod]
        public void DeleteReturnsStudent()
        {
            //Arrange
            var context = new Mock<HttpContext>();
            controller.ControllerContext = new ControllerContext { HttpContext = context.Object };

            var identity = new Mock<ClaimsIdentity>();
            context.SetupGet(c => c.User.Identity).Returns(identity.Object);

            string email = "test@test.com";
            identity.Setup(i => i.FindFirst(ClaimTypes.Email)).Returns(new Claim("email", email));
            people.Setup(p => p.Get(It.IsAny<long>())).ReturnsAsync(new Person { Email = email });

            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);
            repository.Setup(r => r.Delete(It.IsAny<object[]>())).ReturnsAsync(new Student());

            //Act
            var result = (controller.Delete(1).Result as OkObjectResult).Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Student));
        }

        [TestMethod]
        public void GetStudensPageReturnsListOfStudents()
        {
            //Arrange
            repository.Setup(r => r.GetStudentPage(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Student>() as IEnumerable<Student>);

            //Act
            var result = controller.GetStudentsPage(0, 15).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Student>));
        }

        [TestMethod]
        public void AddStudentToCourseReturnsStudenttCourse()
        {
            //Arrange
            repository.Setup(r => r.AddToCourse(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(new StudentCourse());
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);

            //Act
            var result = (controller.AddStudentToCourse(1, 1).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(StudentCourse));
        }

        [TestMethod]
        public void RemoveStudentFromCourseReturnsStdentCourse()
        {
            //Arrange
            repository.Setup(r => r.RemoveFromCourse(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(new StudentCourse());
            repository.Setup(r => r.Exists(It.IsAny<object[]>())).ReturnsAsync(true);

            //Act
            var result = (controller.RemoveStudentFromCourse(1, 1).Result as OkObjectResult)?.Value;

            //Assert
            Assert.IsInstanceOfType(result, typeof(StudentCourse));
        }
    }
}
