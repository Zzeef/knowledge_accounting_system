using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.BLL.Services;
using knowledge_accounting_system.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledge_accounting_system.WEB
{
    [TestClass]
    public class MarksTest
    {
        private MarkService markService
        {
            get
            {
                var mock = new Mock<IdentityUnitOfWork>("TestDatabase");
                return new MarkService(mock.Object);
            }
        }

        [TestMethod]
        public void Create()
        {
            markService.DeleteMarkAsync(new MarkDTO { Name = "Math" });
            var result = markService.Create("Math");

            Assert.IsTrue(result.Result.Succedeed);
        }

        [TestMethod]
        public void FindRoleByNameAsync()
        {
            var mock = new Mock<IMarkService>();
            mock.Setup(x => x.FindMarkByIdAsync(1)).ReturnsAsync(new MarkDTO { Id = 1, Name = "Math" });

            var result = mock.Object.FindMarkByIdAsync(1);
            Assert.IsTrue(result.Result.Id != null);
        }

        [TestMethod]
        public void Delete()
        {
            markService.Create("user");
            var result = markService.DeleteMarkAsync(new MarkDTO { Name = "user" });
            Assert.IsTrue(result.Result.Succedeed);
        }
    }
}