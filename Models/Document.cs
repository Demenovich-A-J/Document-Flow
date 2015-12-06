namespace EntityModels
{
    public class Document
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}