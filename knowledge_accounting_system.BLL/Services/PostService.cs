using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Services
{
    public class PostService : IPostService
    {
        IUnitOfWork Database { get; set; }

        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<PostDTO> Posts
        {
            get
            {
                List<PostDTO> result = new List<PostDTO>();
                foreach (var post in Database.PostManager.Posts)
                {
                    PostDTO postDTO = new PostDTO
                    {
                        Id = post.Id,
                        Name = post.Name,
                        Title = post.Title,
                        Text = post.Text,
                        Date = post.Date
                    };
                    result.Add(postDTO);
                }
                return result;
            }
        }
        /// <summary>
        /// Добавляет пост
        /// </summary>
        /// <param name="item">Объект Post</param>
        public async Task<OperationDetails> AddPost(PostDTO item) 
        {
            ApplicationPost newItem = new ApplicationPost
            {
                Id = item.Id,
                Name = item.Name,
                Title = item.Title,
                Text = item.Text,
                Date = item.Date
            };
            var result = await Database.PostManager.AddPost(newItem);
            if (result) 
            {               
                await Database.SaveAsync();
                return new OperationDetails(true, "Пост добавлен", "");
            }                       
            return new OperationDetails(false, "Пост не добавлен", "");
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="id">id поста</param>
        public async Task<OperationDetails> DeletePost(int id) 
        {
            var result = await Database.PostManager.DeletePost(id);
            if (result)
            {
                await Database.SaveAsync();
                return new OperationDetails(true, "Пост уалён", "");
            }
            return new OperationDetails(false, "Пост не удалён", "");
        }

        /// <summary>
        /// Ищет пост по id
        /// </summary>
        /// <param name="id">id поста</param>
        /// <returns>Объект поста</returns>
        public async Task<PostDTO> FindPostByIdAsync(int id) 
        {
            var item = await Database.PostManager.FindPostByIdAsync(id);
            PostDTO result = new PostDTO
            {
                Id = item.Id,
                Name = item.Name,
                Title = item.Title,
                Text = item.Text,
                Date = item.Date
            };
            return result;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
