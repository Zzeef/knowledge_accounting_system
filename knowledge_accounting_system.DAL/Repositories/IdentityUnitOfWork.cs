using knowledge_accounting_system.DAL.EF;
using knowledge_accounting_system.DAL.Entities;
using knowledge_accounting_system.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using knowledge_accounting_system.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace knowledge_accounting_system.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext db;

        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly IMarkManager markManager;
        private readonly IUserMarkManager userMarkManager;
        private readonly IPostManager postManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            markManager = new MarkManager(db);
            userMarkManager = new UserMarkManager(db);
            postManager = new PostManager(db);
        }

        public IPostManager PostManager 
        {
            get { return postManager; }
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IMarkManager MarkManager
        {
            get { return markManager; }
        }

        public IUserMarkManager UserMarkManager 
        {
            get { return userMarkManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    markManager.Dispose();
                    userMarkManager.Dispose();
                    postManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
