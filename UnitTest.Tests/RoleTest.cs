using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.BLL.Services;
using knowledge_accounting_system.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Tests
{
    [TestClass]
    public class RoleTest
    {
        private RoleService roleService
        {
            get
            {
                var mock = new Mock<IdentityUnitOfWork>("TestDatabase");
                return new RoleService(mock.Object);
            }
        }

        [TestMethod]
        public async Task Create() 
        {
            await roleService.DeleteRoleAsync(new RoleDTO { Id = "1", Name = "user" });
            var result = roleService.Create("user");

            Assert.IsTrue(result.Result.Succedeed);
        }

        [TestMethod]
        public void FindRoleByNameAsync() 
        {
            var mock = new Mock<IRoleService>();
            mock.Setup(x => x.FindRoleByIdAsync("user")).ReturnsAsync(new RoleDTO { Id = "1", Name = "user"});

            var result = mock.Object.FindRoleByIdAsync("user");
            Assert.IsTrue(result.Result.Id != null);
        }

        [TestMethod]
        public async Task Delete()
        {
            await roleService.Create("manager");
            var result = roleService.DeleteRoleAsync(new RoleDTO { Id = "2", Name = "manager" });
            Assert.IsTrue(result.Result.Succedeed);
        }
    }
}
