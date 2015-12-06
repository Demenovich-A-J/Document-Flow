namespace EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class DocumentTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public string PositionsPath { get; set; }
    }
}
