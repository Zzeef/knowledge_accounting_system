using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.BLL.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using knowledge_accounting_system.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using knowledge_accounting_system.DAL.Interfaces;
using knowledge_accounting_system.DAL.Repositories;

namespace UnitTest.Tests
{
    [TestClass]
    public class UserTest
    {
        private RoleService roleService
        {
            get
            {
                var mock = new Mock<IdentityUnitOfWork>("TestDatabase");
                return new RoleService(mock.Object);
            }
        }

        private UserService userService
        {
            get
            {
                var mock = new Mock<IdentityUnitOfWork>("TestDatabase");
                return new UserService(mock.Object);
            }
        }

        private UserDTO testUser => new UserDTO() { Name = "testuserg", Password = "ASDf1234", Email = "testuserg@gmail.com", Role = "user" };


        [TestMethod]
        public void FindUserByIdAsync()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.FindUserByIdAsync("1")).ReturnsAsync(new UserDTO() { Id = "1", Name = "123123", Password = "ASDf1234" });

            var result  = mock.Object.FindUserByIdAsync("1");
            Assert.IsTrue(result.Result.Id != null);
        }

        [TestMethod]
        public void FindUserByNameAsync() 
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.FindUserByNameAsync("123123")).ReturnsAsync(new UserDTO() { Id = "1", Name = "123123", Password = "ASDf1234" });
           
            var result = mock.Object.FindUserByNameAsync("123123");
            Assert.IsTrue(result.Result.Id != null);
        }

        [TestMethod]
        public void GetUsers() 
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.Users).Returns(new List<IdentityUser>());

            var result = mock.Object;
            Assert.IsTrue(result.Users != null);
        }

        [TestMethod]
        public void Create()
        {
            userService.DeleteUserAsync(testUser);
            roleService.Create("user");
            var result = userService.Create(testUser);

            Assert.IsTrue(result.Result.Succedeed);
        }
        /**/
        [TestMethod]
        public void Delete() 
        {
            userService.Create(testUser);
            var result = userService.DeleteUserAsync(testUser);
            Assert.IsTrue(result.Result.Succedeed);
        }
    }
}
