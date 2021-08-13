using knowledge_accounting_system.DAL.EF;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Repositories
{
    public class PostManager : IPostManager
    {
        public ApplicationContext Database { get; set; }
        public PostManager(ApplicationContext db)
        {
            Database = db;
        }

        public IQueryable<ApplicationPost> Posts
        {
            get
            {
                return Database.PostManager;
            }
        }

        public async Task<ApplicationPost> FindPostByIdAsync(int id) 
        {
            return await Database.PostManager.FindAsync(id);
        }

        public async Task<bool> AddPost(ApplicationPost item) 
        {
            if (await FindPostByIdAsync(item.Id) != null) { return false; }
            Database.PostManager.Add(item);
            Database.SaveChanges();
            return true;
        }

        public async Task<bool> DeletePost(int id) 
        {
            var item = await FindPostByIdAsync(id);
            if (item == null) { return false; }
            Database.PostManager.Remove(item);
            Database.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
