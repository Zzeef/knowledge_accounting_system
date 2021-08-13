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
    public class PostTest
    {
        private PostService postService
        {
            get
            {
                var mock = new Mock<IdentityUnitOfWork>("TestDatabase");
                return new PostService(mock.Object);
            }
        }

        [TestMethod]
        public async Task Create()
        {
            await postService.DeletePost(1);
            var result = postService.AddPost(new PostDTO { Name = "user", Text = "312312312", Title = "dasdasd",Date = DateTime.Now});

            Assert.IsTrue(result.Result.Succedeed);
        }
        [TestMethod]
        public void GetPosts()
        {
            var mock = new Mock<IPostService>();
            mock.Setup(x => x.Posts).Returns(new List<PostDTO>());

            var result = mock.Object;
            Assert.IsTrue(result.Posts != null);
        }
    }
}
