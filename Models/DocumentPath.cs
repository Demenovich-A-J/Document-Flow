namespace EntityModels
{
    public class DocumentPath
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public bool? Status { get; set; }
    }
}