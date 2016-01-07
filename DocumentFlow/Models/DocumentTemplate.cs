using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Models
{
    public class DocumentTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public string PositionsPath { get; set; }
    }
}