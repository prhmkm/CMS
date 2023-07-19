using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Page
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public int LanguageId { get; set; }
        public string Name { get; set; } = null!;
        public string CustomHTML { get; set; } = null!;
        public int ViewCounter { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
