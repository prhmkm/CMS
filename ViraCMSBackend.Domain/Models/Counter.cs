using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Counter
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
