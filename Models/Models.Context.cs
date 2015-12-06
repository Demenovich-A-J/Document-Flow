namespace EntityModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }
    
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<DocumentPath> DocumentPaths { get; set; }
    }
}
