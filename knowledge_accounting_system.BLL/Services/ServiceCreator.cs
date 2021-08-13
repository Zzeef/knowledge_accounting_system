using knowledge_accounting_system.BLL.Interfaces;
using knowledge_accounting_system.DAL.Repositories;

namespace knowledge_accounting_system.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        public IRoleService CreateRoleService(string connection) 
        {
            return new RoleService(new IdentityUnitOfWork(connection));
        }

        public IMarkService CreateMarkService(string connection) 
        {
            return new MarkService(new IdentityUnitOfWork(connection));
        }

        public IUserMarkService CreateUserMarkService(string connection) 
        {
            return new UserMarkService(new IdentityUnitOfWork(connection));
        }

        public IPostService CreatePostService(string connection) 
        {
            return new PostService(new IdentityUnitOfWork(connection));
        }
    }
}
