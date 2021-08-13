using knowledge_accounting_system.DAL.EF;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Repositories
{
    public class UserMarkManager : IUserMarkManager
    {
        public ApplicationContext Database { get; set; }
        public UserMarkManager(ApplicationContext db)
        {
            Database = db;
        }

        public IQueryable<ApplicationUserMark> UserMarks
        {
            get
            {
                return Database.UserMarkManager;
            }
        }

        public async Task<bool> Create(ApplicationUserMark item) 
        {
            if (!await FindUserMarkAsync(item)) 
            {
                Database.UserMarkManager.Add(item);
                Database.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(ApplicationUserMark item)
        {
            var newItem = await Database.UserMarkManager.FindAsync(item.UserId, item.MarkId);
            if (newItem != null) 
            {
                Database.UserMarkManager.Remove(newItem);
                Database.SaveChanges();
                return true; 
            }
            return false;
        }

        public async Task<bool> FindUserMarkAsync(ApplicationUserMark item) 
        {
            var result = await Database.UserMarkManager.FindAsync(item.UserId,item.MarkId);
            if (result == null) { return false; }
            return true;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
