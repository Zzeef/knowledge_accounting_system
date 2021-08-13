using knowledge_accounting_system.BLL.DTO;
using knowledge_accounting_system.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IPostService : IDisposable
    {
        List<PostDTO> Posts { get; }
        Task<OperationDetails> AddPost(PostDTO item);
        Task<OperationDetails> DeletePost(int id);
        Task<PostDTO> FindPostByIdAsync(int id);
    }
}
