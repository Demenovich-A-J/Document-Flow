namespace BL.Models
{
    public class DocumentTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }
        public string Text { get; set; }
        public string PositionsPath { get; set; }
    }
}