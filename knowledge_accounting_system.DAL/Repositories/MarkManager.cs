using knowledge_accounting_system.DAL.EF;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Repositories
{
    public class MarkManager : IMarkManager
    {
        public ApplicationContext Database { get; set; }
        public MarkManager(ApplicationContext db)
        {
            Database = db;
        }

        public IQueryable<ApplicationMark> Marks
        {
            get
            {
                return Database.MarkManager;
            }
        }

        public ApplicationMark FindMarkByNameAsync(string name) 
        {
            ApplicationMark mark = Database.MarkManager.FirstOrDefault(x => x.Name == name);
            return mark;
        }

        public async Task<ApplicationMark> FindMarkByIdAsync(int id) 
        {       
            ApplicationMark mark = await Database.MarkManager.FindAsync(id);
            return mark;
        }

        public async Task<bool> Delete(ApplicationMark item) 
        {
            var result = await Database.MarkManager.FindAsync(item.Id);
            if (result == null) { return false; }
            Database.MarkManager.Remove(item);
            Database.SaveChanges();
            return true;
        }

        public async Task<bool> Create(ApplicationMark item)
        {
            List<ApplicationMark> result = Database.MarkManager.Where(x=>x.Name == item.Name).ToList();
            if (!result.Any()) 
            {
                Database.MarkManager.Add(item);
                Database.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
