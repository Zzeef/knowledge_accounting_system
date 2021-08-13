using System;

namespace knowledge_accounting_system.DAL.Entities
{
    public class ApplicationPost
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}
