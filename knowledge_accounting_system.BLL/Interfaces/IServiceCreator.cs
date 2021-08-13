namespace knowledge_accounting_system.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
        IRoleService CreateRoleService(string connection);
        IMarkService CreateMarkService(string connection);
        IUserMarkService CreateUserMarkService(string connection);
        IPostService CreatePostService(string connection);
    }
}
