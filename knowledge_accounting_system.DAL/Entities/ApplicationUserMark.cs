
namespace knowledge_accounting_system.DAL.Entities
{
    public class ApplicationUserMark
    {
        public string UserId { get; set; }

        public int MarkId { get; set; }

        public int Score { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationMark Mark { get; set; }
    }
}
