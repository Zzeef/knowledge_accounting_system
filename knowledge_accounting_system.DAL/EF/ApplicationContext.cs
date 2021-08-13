using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using knowledge_accounting_system.DAL.Entities;

namespace knowledge_accounting_system.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString) { }

        public DbSet<ApplicationMark> MarkManager { get; set; }

        public DbSet<ApplicationUserMark> UserMarkManager { get; set; }

        public DbSet<ApplicationPost> PostManager { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationMark>().ToTable("AppMark");

            modelBuilder.Entity<ApplicationPost>().ToTable("AppPost");

            modelBuilder.Entity<ApplicationUserMark>().HasKey(c => new { c.UserId, c.MarkId });
        }
    }
}
