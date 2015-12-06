namespace EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentPath
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
