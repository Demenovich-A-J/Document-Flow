using System;

namespace DocumentFlow.Models
{
    public class Document
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool? Status { get; set; }
        public DocumentType Type { get; set; }
        public DateTime DateTime { get; set; }
    }
}