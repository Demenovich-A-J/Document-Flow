//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
        public string PositionsPass { get; set; }
    }
}